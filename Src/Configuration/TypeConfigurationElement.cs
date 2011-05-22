using System;
using System.Configuration;

namespace Tamarack.Configuration
{
    public class TypeConfigurationElement : ConfigurationElement
    {
        public const string NameProperty = "name";
        public const string TypeProperty = "type";

        [ConfigurationProperty(NameProperty)]
        public virtual string Name
        {
            get { return (string)this[NameProperty]; }
            set { this[NameProperty] = value; }
        }

        [ConfigurationProperty(TypeProperty, IsRequired=true)]
        public virtual string TypeName
        {
            get { return (string)this[TypeProperty]; }
            set { this[TypeProperty] = value; }
        }

        public virtual Type Type
        {
            get { return Type.GetType(TypeName, false); }
        }
    }
}
