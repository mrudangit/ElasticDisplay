using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;

namespace ElasticDisplay
{

    [DataContract]
    public class Schema
    {
        public override string ToString()
        {
            DataContractSerializer ser =
                new DataContractSerializer(typeof(Schema));

            var str = new StringWriter();
            var sxmltr = new XmlTextWriter(str);
            sxmltr.Formatting = Formatting.Indented;
            ser.WriteObject(sxmltr, this);


            return str.ToString();
        }

        public Schema()
        {
            Columns = new List<Column>();
        }
        [DataMember]
        public String Name { get; set; }

        [DataMember]
        public IList<Column> Columns { get; set; }

        [DataMember]
        public IList<Column> KeyColumns { get; set; }



        public List<DynamicPropertyDescriptor> GetPropertyDesciptors()
        {
            return Columns.Select(column => new DynamicPropertyDescriptor(Name, column.Name, TypeUtils.GetType(column.TypeCode), null)).ToList();
        }
    }
}
