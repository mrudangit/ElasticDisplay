using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticDisplay
{
    public class AmpsParseUtils
    {

        public IDictionary<string, string> ParseAmpsMessagetoDictionary(string data)
        {

            var nv = new Dictionary<string, string>();

            var length = !string.IsNullOrEmpty(data) ? data.Length : 0;
            int index = 0;

            while (index < length)
            {

                int startIndex = index;
                int i = -1;

                while (index < length)
                {
                    var ch = data[index];

                    if (ch == '=')
                    {
                        if (i < 0)
                            i = index;
                    }
                    else if (ch == (char)1)
                    {
                        break;
                    }

                    index++;
                }

                String name = null;
                String value = null;

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
