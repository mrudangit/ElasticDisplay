using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ElasticDisplay
{
    public class GridViewModel
    {
        private ElasticRowCollection _rowCollection;

        public GridViewModel()
        {

            var s = LoadSChema("PositionSchema.xml");
            _rowCollection = new ElasticRowCollection(null, new List<Schema>() {s});


        }





        public ElasticRowCollection RowCollection
        {

            get { return _rowCollection; }
        }


        private Schema LoadSChema(string schemaFile)
        {
            DataContractSerializer ser =
              new DataContractSerializer(typeof(Schema));
            FileStream fs = new FileStream(schemaFile,FileMode.Open);


            var obj = ser.ReadObject(new XmlTextReader(fs));


            var s = obj as Schema;

            return s;
        }
    }
}
