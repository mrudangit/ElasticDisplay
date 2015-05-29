using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticDisplay
{
    public class ElasticRowCollection : ObservableCollection<ElasticRow>, ITypedList
    {
        private readonly string _listName;
        private readonly IDictionary<string,PropertyDescriptorCollection>  _groupPropertyDescriptorCollections  = new ConcurrentDictionary<string, PropertyDescriptorCollection>();
        private readonly PropertyDescriptorCollection _propertyDescriptorCollection;
        public ElasticRowCollection(string listName, List<Schema> schemas )
        {
            _listName = listName;


            var l = new List<DynamicPropertyDescriptor>();

            foreach (var schema in schemas)
            {
                l.AddRange(schema.GetPropertyDesciptors());
                _groupPropertyDescriptorCollections[schema.Name] =
                    new PropertyDescriptorCollection(schema.GetPropertyDesciptors().ToArray());
            }

            _propertyDescriptorCollection =  new PropertyDescriptorCollection(l.ToArray());
            AddNewRow();
            AddNewRow();
        }

        public string GetListName(PropertyDescriptor[] listAccessors)
        {
            return _listName;
        }

        public PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors)
        {
            return _propertyDescriptorCollection;
        }


        public void AddNewRow()
        {
            IList<DynamicObjectGroup> grps = _groupPropertyDescriptorCollections.Select(pair => new DynamicObjectGroup(pair.Key, pair.Value, true)).ToList();

            this.Add(new ElasticRow(grps));

        }
    }
}
