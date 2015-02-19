using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;

namespace ElasticDisplay
{
    public class ElasticRow :CustomTypeDescriptor
    {
        private readonly PropertyDescriptorCollection _propertyDescriptorCollection;
        private readonly ConcurrentDictionary<string,DynamicObjectGroup>  _objectGroups = new ConcurrentDictionary<string, DynamicObjectGroup>();

        public ElasticRow(IList<DynamicObjectGroup> columnGroups )
        {
            var props = new List<PropertyDescriptor>();

            foreach (var columnGroup in columnGroups)
            {
                var numOfProps = columnGroup.GetProperties().Count;
                for (int i = 0; i<numOfProps; i++)
                {
                    props.Add(columnGroup.GetProperties()[i]);
                }
                _objectGroups[columnGroup.GroupName] = columnGroup;

            }

            _propertyDescriptorCollection = new PropertyDescriptorCollection(props.ToArray());


        }


        public override PropertyDescriptorCollection GetProperties()
        {
            return _propertyDescriptorCollection;
        }


        public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return _propertyDescriptorCollection;
                
        }


        public object GetValue(string groupName, string propertyName)
        {
            var group = _objectGroups[groupName];
            return group.GetValue(propertyName);
        }

        public void SetValue(string groupName, string propertyName, object data)
        {
            var group = _objectGroups[groupName];
            group.SetValue(propertyName,data);
        }

        


    }
}
