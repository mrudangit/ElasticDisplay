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

            var props = new List<DynamicPropertyDescriptor>();
            foreach (KeyValuePair<string, Type> keyValuePair in propertyDictionary)
            {
                
                var p =new DynamicPropertyDescriptor(groupName,keyValuePair.Key, keyValuePair.Value, null);
                props.Add(p);

            }

            _propertyDescriptorCollection = new PropertyDescriptorCollection(props.ToArray());
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

        private Random random = new Random();
        private object GetSampleData(Type t)
        {
            switch (t.Name)
            {
                case "String":
                    return string.Format("StringData-{0}", random.Next(1, 100));
                case "Int32":
                    return random.Next(1, 1000);
            }

            return null;
        }
        
    }
}
