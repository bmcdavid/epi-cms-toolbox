using EPiServer.DataAnnotations;
using System;

namespace bmcdavid.Episerver.CmsToolbox
{
    /// <summary>
    /// Extended default to allow assignment to interfaces
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface, Inherited = true)]
    public class AvailableContentTypesExtendedAttribute : AvailableContentTypesAttribute { }
}