@Echo off

PowerShell -NoProfile -NoLogo -ExecutionPolicy unrestricted -Command "& '%~dp0tools.ps1' %*"