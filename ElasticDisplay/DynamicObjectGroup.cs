using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticDisplay
{
    public class DynamicObjectGroup :CustomTypeDescriptor
    {
        public string GroupName { get; set; }
        private readonly IDictionary<string, Type> _propertyDictionary;
        private readonly PropertyDescriptorCollection _propertyDescriptorCollection;
        public DynamicObjectGroup(string groupName,IDictionary<string,Type> propertyDictionary)
        {
            GroupName = groupName;
            _propertyDictionary = propertyDictionary;

            _propertyDescriptorCollection = new PropertyDescriptorCollection(propertyDictionary.Select(keyValuePair => new DynamicPropertyDescriptor(groupName, keyValuePair.Key, keyValuePair.Value, null)).ToArray());
        }


        public DynamicObjectGroup(string groupName, PropertyDescriptorCollection propertyDescriptorCollection,bool generateTestData=false)
        {
            GroupName = groupName;
            _propertyDescriptorCollection = propertyDescriptorCollection;
            if (generateTestData)
            {
                GenerateTestData();
            }
        }

        public override PropertyDescriptorCollection GetProperties()
        {
            return _propertyDescriptorCollection;
        }

        public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return _propertyDescriptorCollection;
        }



        public object GetValue(string propertName)
        {
            if (_testData != null)
            {
                return _testData[propertName];
            }

            return null;
        }


        public void SetValue(string propertyName, object data)
        {
            _testData[propertyName] = data;
        }


        private IDictionary<string, object> _testData = null;

        private void GenerateTestData()
        {
            _testData = new Dictionary<string, object>();
            for (int i=0;i< _propertyDescriptorCollection.Count;i++)
            {
                var pd = _propertyDescriptorCollection[i];
                _testData[pd.Name] = GetSampleData(pd.PropertyType);
            }
        }

        private readonly Random _random = new Random();
        private object GetSampleData(Type t)
        {
            switch (Type.GetTypeCode(t))
            {
                case TypeCode.Empty:
                    break;
                case TypeCode.Object:
                    break;
                case TypeCode.DBNull:
                    break;
                case TypeCode.Boolean:
                    break;
                case TypeCode.Char:
                    break;
                case TypeCode.SByte:
                    break;
                case TypeCode.Byte:
                    break;
                case TypeCode.Int16:
                    break;
                case TypeCode.UInt16:
                    break;
                case TypeCode.Int32:
                    return _random.Next(1, 1000);
                    break;
                case TypeCode.UInt32:
                    break;
                case TypeCode.Int64:
                    break;
                case TypeCode.UInt64:
                    break;
                case TypeCode.Single:
                    break;
                case TypeCode.Double:
                    break;
                case TypeCode.Decimal:
                    break;
                case TypeCode.DateTime:
                    break;
                case TypeCode.String:
                    return string.Format("StringData-{0}", _random.Next(1, 100));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return null;
        }
        
    }
}
