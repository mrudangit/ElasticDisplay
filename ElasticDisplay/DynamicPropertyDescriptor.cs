using System;
using System.ComponentModel;

namespace ElasticDisplay
{
    public class DynamicPropertyDescriptor : PropertyDescriptor
    {
        private readonly  string _propertyName;
        private readonly Type _propertyType;
        private readonly string _groupName;

        protected DynamicPropertyDescriptor(MemberDescriptor descr) : base(descr)
        {
        }

        protected DynamicPropertyDescriptor(MemberDescriptor descr, Attribute[] attrs) : base(descr, attrs)
        {
        }

        public DynamicPropertyDescriptor(string groupName,string name,Type type, Attribute[] attrs) : base(name, attrs)
        {
            _groupName = groupName;
            _propertyName = name;
            _propertyType = type;
        }

        public override Type ComponentType
        {
            get
            {
                return typeof(ElasticRow);
            }
        }

        public override bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public override Type PropertyType
        {
            get { return  _propertyType; }
        }

        public override bool CanResetValue(object component)
        {
            return true;
        }

        public override object GetValue(object component)
        {
            var row = component as ElasticRow;

            return row.GetValue(_groupName, Name);
        }

        public override void ResetValue(object component)
        {
            
        }

        public override void SetValue(object component, object value)
        {
            var row = component as ElasticRow;
            Console.WriteLine("SetValue : GroupName = {0} PropertyName={1} Value = {2}",_groupName,Name,value);
            row.SetValue(_groupName,Name,value);
        }

        public override bool ShouldSerializeValue(object component)
        {
            return false;
        }


      
    }
}
