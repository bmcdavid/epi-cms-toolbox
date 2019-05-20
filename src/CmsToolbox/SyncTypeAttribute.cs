using System;

namespace bmcdavid.Episerver.CmsToolbox
{
    /// <summary>
    /// When applied to a class, Episerver initialization will add attributes assigned to provided type
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class SyncTypeAttribute : Attribute
    {
        /// <summary>
        /// Global error switch on how to handle type not resolved
        /// </summary>
        public static bool ErrorOnStringGetType { get; set; } = true;

        /// <summary>
        /// Target type to sync attribute
        /// </summary>
        public Type ProviderType { get; }

        /// <summary>
        /// Type to sync attributes from
        /// </summary>
        /// <param name="type"></param>
        public SyncTypeAttribute(Type type) => ProviderType = type;

        /// <summary>
        /// Fully qualified type string, other constructor usage is preferred.
        /// </summary>
        /// <param name="typeQualifiedName"></param>
        public SyncTypeAttribute(string typeQualifiedName) => ProviderType = Type.GetType(typeQualifiedName, ErrorOnStringGetType);
    }
}