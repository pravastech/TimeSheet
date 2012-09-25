using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace Pos360
{
    public class JSUtil
    {
        public static string encloseInJavascriptTag(string script)
        {
            return "<script type=\"text/javascript\">\n" + script + "\n</script>";
        }

        public static string Enquote(string s)
        {
            return JSUtil.Enquote(s, '"', true);
        }

        public static string EnquoteJS(string s)
        {
            return JSUtil.Enquote(s, '\'', false);
        }

        public static string Enquote(string s, char encloser, bool encloseOutput)
        {
            if (s == null || s.Length == 0)
            {
                if (encloseOutput)
                {
                    return encloser.ToString() + encloser.ToString();
                }
                else
                {
                    return "";
                }
            }
            char c;
            int i;
            int len = s.Length;
            StringBuilder sb = new StringBuilder(len + 4);
            string t;
            if (encloseOutput)
            {
                sb.Append(encloser);
            }
            for (i = 0; i < len; i += 1)
            {
                c = s[i];
                if ((c == '\\') || (c == encloser) || (c == '>'))
                {
                    sb.Append('\\');
                    sb.Append(c);
                }
                else if (c == '\b')
                    sb.Append("\\b");
                else if (c == '\t')
                    sb.Append("\\t");
                else if (c == '\n')
                    sb.Append("\\n");
                else if (c == '\f')
                    sb.Append("\\f");
                else if (c == '\r')
                    sb.Append("\\r");
                else if (c == '/')
                    sb.Append("\\/");
                else
                {
                    if (c < ' ')
                    {
                        //t = "000" + Integer.toHexString(c); 
                        string tmp = new string(c, 1);
                        t = "000" + int.Parse(tmp, System.Globalization.NumberStyles.HexNumber);
                        sb.Append("\\u" + t.Substring(t.Length - 4));
                    }
                    else
                    {
                        sb.Append(c);
                    }
                }
            }
            if (encloseOutput)
            {
                sb.Append(encloser);
            }
            return sb.ToString();
        }

        public static string ToJSProperty(Type fieldType, object fieldValue)
        {
            return "";
        }
    }
}