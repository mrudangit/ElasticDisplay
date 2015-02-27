using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticDisplay
{
    public class KeyProperty
    {
        public KeyProperty(string name, string value)
        {
            Name = name;
            Value = value;
        }


        public string Name { get; private set; }
        public string Value { get; private set; }


    }
}
