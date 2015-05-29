using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation.Peers;
using System.Windows.Documents.DocumentStructures;

namespace ElasticDisplay
{

    [DataContract]
    public class Schema
    {

        public Schema()
        {
            Columns = new List<Column>();
        }
        [DataMember]
        public String Name { get; set; }

        [DataMember]
        public IList<Column> Columns;


        public List<DynamicPropertyDescriptor> GetPropertyDesciptors()
        {
            return Columns.Select(column => new DynamicPropertyDescriptor(Name, column.Name, Type.GetType(column.Type), null)).ToList();
        }
    }
}
