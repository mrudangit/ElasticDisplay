using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticDisplay
{
    public class MessageKey
    {

        public MessageKey(params string[] fields)
        {
            KeyFields = fields.ToList();
            KeyString = String.Join(":", fields);
        }

        public IList<string> KeyFields { get; private set; }

        public string KeyString { get; private set; }

    }
}
