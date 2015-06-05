using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;
using ElasticDisplay;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ElasticDisplayTests
{
    [TestClass]
    public class SchemaTests
    {
        [TestMethod]
        public void SchemaSerializeTest()
        {
            var s = new Schema
            {
                Name = "Position",
                
            };


            s.Columns = new List<Column>();

            s.Columns.Add(new Column() {Name = "Account",TypeCode = TypeCode.String});
            s.Columns.Add(new Column() { Name = "Cusip", TypeCode=TypeCode.String});
            s.Columns.Add(new Column() { Name = "Quantity",TypeCode = TypeCode.Int32});



            DataContractSerializer ser =
                 new DataContractSerializer(typeof(Schema));

            var str = new StringWriter();
            var sxmltr = new XmlTextWriter(str);
            sxmltr.Formatting= Formatting.Indented;
            ser.WriteObject(sxmltr, s);


            Console.WriteLine(str.ToString());




        }



        [TestMethod]
        [DeploymentItem("SampleSchema.xml")]

        public void SchemaDeserializeTest()
        {
            DataContractSerializer ser =
              new DataContractSerializer(typeof(Schema));
            FileStream fs = new FileStream("SampleSchema.xml",
           FileMode.Open);


            var obj =ser.ReadObject(new XmlTextReader(fs));


            var s = obj as Schema;

            Debug.Assert(s != null);
            Debug.Assert(s.Columns[0].TypeCode == TypeCode.String);
            Console.WriteLine(s.ToString());
        }

    }
}
