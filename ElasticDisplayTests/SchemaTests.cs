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

            s.Columns.Add(new Column() {Name = "Account",Type = "String"});
            s.Columns.Add(new Column() { Name = "Cusip", Type = "String" });
            s.Columns.Add(new Column() { Name = "Quantity", Type = "Int32" });



            DataContractSerializer ser =
                 new DataContractSerializer(typeof(Schema));

            var str = new StringWriter();
            var sxmltr = new XmlTextWriter(str);
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
        }

    }
}
