using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ElasticDisplay
{


    [DataContract]
    public class Column
    {
    
        private TypeCode _typeCode;

        [DataMember]
        public string Name { get; set; }

       

        [DataMember]
        public TypeCode TypeCode
        {
            get { return _typeCode; }
            set
            {
                _typeCode = value;
            }
        }
    }
}
