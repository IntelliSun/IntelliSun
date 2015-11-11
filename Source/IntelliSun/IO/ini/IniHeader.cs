using System;
using System.Collections.Generic;
using System.Text;
using IntelliSun.Helpers;
using IntelliSun.Text;

namespace IntelliSun.IO
{
    public class IniHeader : IniObject
    {
        public IniHeader()
        {

        }

        public IniHeader(IEnumerable<string> comments)
            : this()
        {
            this.GetComments.AddRange(comments);
        }

        public override string GetString(IniParameters parameters)
        {
            return this.GetString(parameters, DateTime.Now.Ticks + ".ini", DateTime.Now);
        }

        public string GetString(IniParameters parameters, string file, DateTime dateTime)
        {
            var blr = new StringBuilder();

            var cc = (char)parameters.CommentChar;
            foreach (var comment in this.Comments)
            {
                blr.AppendLine(cc + new TokenFormatter(new[] {
                    FormatToken.New("t", dateTime.ToShortTimeString),
                    FormatToken.New("d", dateTime.ToShortDateString),
                    FormatToken.New("f", () => file)
                }).Format(comment));
            }

            return blr.ToString();
        }
    }
}
