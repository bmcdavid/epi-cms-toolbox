using EPiServer.DataAnnotations;
using System;

namespace bmcdavid.Episerver.CmsToolbox
{
    /// <summary>
    /// Extended attribute for interfaces
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface, Inherited = true)]
    public class ImageDescriptorExtendedAttribute : ImageDescriptorAttribute { }
}