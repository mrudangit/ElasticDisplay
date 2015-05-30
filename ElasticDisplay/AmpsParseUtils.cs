using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticDisplay
{
    public class AmpsParseUtils
    {

        private const char FieldValueSeperator='=';
        private const char  FieldSeperator=(char)1;

        public IDictionary<string, string> ParseAmpsMessagetoDictionary(string data)
        {
            
            var nv = new Dictionary<string, string>();

            if (string.IsNullOrWhiteSpace(data))
            {
                return nv;
            }

            var length = data.Length;
            int index = 0;

            while (index < length)
            {

                int startIndex = index;
                int i = -1;

                while (index < length)
                {
                    var ch = data[index];

                    if (ch == FieldValueSeperator)
                    {
                        if (i < 0)
                            i = index;
                    }
                    else if (ch == FieldSeperator)
                    {
                        break;
                    }

                    index++;
                }

                string name = null;
                string value;

                if (i >= 0)
                {
                    name = data.Substring(startIndex, i - startIndex);
                    value = data.Substring(i + 1, index - i - 1);
                }
                else
                {
                    value = data.Substring(startIndex, index - startIndex);
                }

                nv[name] = value;

                index++;
            }
            return nv;

        }
    }
}
