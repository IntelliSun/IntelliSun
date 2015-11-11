#Requires -Version 3

$ScriptPath = $MyInvocation.MyCommand.Definition

$Script:UseWriteHost = $true
function _WriteDebug($msg) {
    if($Script:UseWriteHost) {
        try {
            Write-Debug $msg
        } catch {
            $Script:UseWriteHost = $false
            _WriteDebug $msg
        }
    }
}

function _WriteOut {
    param(
        [Parameter(Mandatory=$false, Position=0, ValueFromPipeline=$true)][string]$msg,
        [Parameter(Mandatory=$false)][ConsoleColor]$ForegroundColor,
        [Parameter(Mandatory=$false)][ConsoleColor]$BackgroundColor,
        [Parameter(Mandatory=$false)][switch]$NoNewLine)

    if($__TestWriteTo) {
        $cur = Get-Variable -Name $__TestWriteTo -ValueOnly -Scope Global -ErrorAction SilentlyContinue
        $val = $cur + "$msg"
        if(!$NoNewLine) {
            $val += [Environment]::NewLine
        }
        Set-Variable -Name $__TestWriteTo -Value $val -Scope Global -Force
        return
    }

    if(!$Script:UseWriteHost) {
        if(!$msg) {
            $msg = ""
        }
        if($NoNewLine) {
            [Console]::Write($msg)
        } else {
            [Console]::WriteLine($msg)
        }
    }
    else {
        try {
            if(!$ForegroundColor) {
                $ForegroundColor = $host.UI.RawUI.ForegroundColor
            }
            if(!$BackgroundColor) {
                $BackgroundColor = $host.UI.RawUI.BackgroundColor
            }

            Write-Host $msg -ForegroundColor:$ForegroundColor -BackgroundColor:$BackgroundColor -NoNewLine:$NoNewLine
        } catch {
            $Script:UseWriteHost = $false
            _WriteOut $msg
        }
    }
}

function _MakeToken()
{
    param(
        [string]$tokenName
    )

    New-Item ".\$tokenName" -ItemType "File" | %{$_.Attributes = "hidden"}
}

Set-Variable -Option Constant "CommandName" ([IO.Path]::GetFileNameWithoutExtension($ScriptPath))
Set-Variable -Option Constant "CommandPrefix" "tools-"

Set-Variable -Option Constant "ToolsDirectory" ".\Tools"
Set-Variable -Option Constant "InstallToken" "~TOOLS"

if(!$ColorScheme) {
    $ColorScheme = @{
        "Banner"=[ConsoleColor]::Cyan
        "RuntimeName"=[ConsoleColor]::Yellow
        "Help_Header"=[ConsoleColor]::Yellow
        "Help_Switch"=[ConsoleColor]::Green
        "Help_Argument"=[ConsoleColor]::Cyan
        "Help_Optional"=[ConsoleColor]::Gray
        "Help_Command"=[ConsoleColor]::DarkYellow
        "Help_Executable"=[ConsoleColor]::DarkYellow
        "Feed_Name"=[ConsoleColor]::Cyan
        "Warning" = [ConsoleColor]::Yellow
    }
}

_WriteDebug "=== Running $CommandName ==="

Function _FindTool {
    param(
        [string]$toolName
    )

    $tools = @(get-command $toolName -ea SilentlyContinue)
    if ($tools.Count -gt 0) {
        return $tools[0]
    } else {
        $tools = @(get-command "tools\$toolName" -ea SilentlyContinue)
        if ($tools.Count -gt 0) {
            return $tools[0]
        }
        return $null;
    }
}

Function _ResolveTool {
    param(
        [string]$toolName,
        [System.Management.Automation.CommandInfo]$installer,
        [object[]]$installerArgs
    )

    Write-Host @("Attemting to find '{0}'..." -f $toolName)
    $tool = @(_FindTool $toolName)
    
    if ($tool) {
        Write-Host @("'{0}' was found on your system!" -f $toolName)
        Write-Host ""
        return $tool;
    } else {
        Write-Host @("'{0}' could not be found in the path." -f $toolName)
        if(!$cmd) {
            throw @("There is no available installer for '{0}'. Please ensure '{0}' is installed and in the path." -f $toolName)
        }

        Write-Host @("Starting installer for '{0}'." -f $toolName)
        Write-Host @("Waiting for installer to finish..." -f $toolName)

        & $installer @installerArgs
        Write-Host ""
    }
}

$nugetCmdLnkTemp = @"
    @Echo off
    "%~dp0{0}" %*
"@

function _NugetInstall {
    param (
        [string]$package,
        [string]$toolPath,
        [System.Management.Automation.CommandInfo]$nugetCmd
    )

    $_args = ("install", "$package", "-OutputDirectory", "$ToolsDirectory", "-ExcludeVersion")

    & $nugetCmd $_args
    ($nugetCmdLnkTemp -f $toolPath)  | Out-File -Encoding ascii "$ToolsDirectory\$package.bat"
}

function tools-init() {
    param ()
    if (!(Test-Path $ToolsDirectory)) {
        New-Item -ItemType Directory "$ToolsDirectory"
    }
}

function tools-clear() {
    param ()

    Remove-Item "$ToolsDirectory\*" -Recurse -ErrorAction SilentlyContinue
    Remove-Item ".\$InstallToken" -ErrorAction SilentlyContinue -Force
}

function tools-install() {
    param ()

    tools-init
    if (Test-Path ".\$InstallToken")
    {
        _WriteOut -ForegroundColor $ColorScheme.Warning "Tool are already installed. If you wish to re-install the tools run the command 'reinstall' instead."
        return;
    }

    $msbuild = _ResolveTool "MSBuild" $null
    $nuget = _ResolveTool "Nuget" $null
    $fake = _ResolveTool "Fake" @(Get-Command -Name _NugetInstall)[0] ("Fake", ".\FAKE\tools\FAKE.exe", $nuget)

    _MakeToken $InstallToken
}

function tools-reinstall() {
    param()

    tools-clear
    tools-install
}

### The main "entry point"
$cmd = $args[0]

if($args.Length -gt 1) {
    $cmdargs = @($args[1..($args.Length-1)])
} else {
    $cmdargs = @()
}

if(!$cmd) {
    _WriteOut "You must specify a command!"
    $cmd = "help"
    $Script:ExitCode = $ExitCodes.InvalidArguments
}

# Check for the command and run it
try {
    if(Get-Command -Name "$CommandPrefix$cmd" -ErrorAction SilentlyContinue) {
        _WriteDebug "& $CommandPrefix$cmd $cmdargs"
        & "$CommandPrefix$cmd" @cmdargs
    }
    else {
        _WriteOut "Unknown command: '$cmd'"
        $Script:ExitCode = $ExitCodes.UnknownCommand
    }
} catch {
    throw
    if(!$Script:ExitCode) { $Script:ExitCode = $ExitCodes.OtherError }
}