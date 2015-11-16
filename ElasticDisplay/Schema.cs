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

    public class TypeUtils
    {
        internal static Type GetType(TypeCode typeCode)
        {
            switch (typeCode)
            {
                case TypeCode.Empty:
                    break;
                case TypeCode.Object:
                    return typeof (Object);
                    
                case TypeCode.DBNull:
                    break;
                case TypeCode.Boolean:
                    return typeof (bool);
                    
                case TypeCode.Char:
                    return  typeof(char);
                case TypeCode.SByte:
                    return typeof(sbyte);
                case TypeCode.Byte:
                    return  typeof(byte);
                case TypeCode.Int16:
                    return typeof (short);
                    
                case TypeCode.Int32:
                    return  typeof(int);
                case TypeCode.UInt32:
                    return  typeof(uint);
                case TypeCode.Int64:
                    return typeof(long);
                case TypeCode.UInt64:
                    return typeof(ulong);
                case TypeCode.Single:
                    return typeof(float);
                case TypeCode.Double:
                    return typeof(double);
                case TypeCode.Decimal:
                    return  typeof(decimal);
                case TypeCode.DateTime:
                    return typeof(DateTime);
                case TypeCode.String:
                    return typeof (string);
                default:
                    throw new ArgumentOutOfRangeException(nameof(typeCode), typeCode, null);
            }
            return null;
        }
    }
}
