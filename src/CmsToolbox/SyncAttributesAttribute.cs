using System;
using System.ComponentModel;

namespace bmcdavid.Episerver.CmsToolbox
{
    /// <summary>
    /// Allows attributes to be maintained on another class or interface property
    /// </summary>
    public class SyncAttributesAttribute : AttributeProviderAttribute
    {
        /// <summary>
        /// Sync attributes, for best practice use nameof() instead of strings
        /// </summary>
        /// <param name="type"></param>
        /// <param name="propertyName"></param>
        public SyncAttributesAttribute(Type type, string propertyName)
            : base(type.AssemblyQualifiedName, propertyName) => ProviderType = type;

        /// <summary>
        /// Target type to sync attribute
        /// </summary>
        public Type ProviderType { get; }
    }
}