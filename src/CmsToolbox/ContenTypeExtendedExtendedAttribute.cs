using EPiServer.DataAnnotations;
using System;

namespace bmcdavid.Episerver.CmsToolbox
{
    /// <summary>
    /// Extended attribute for interfaces
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface, Inherited = true)]
    public class ContentTypeExtendedAttribute : ContentTypeAttribute
    {
        /// <summary>
        /// EditMode
        /// </summary>
        public enum EditMode
        {
            /// <summary>
            /// Allow content to be created of this type
            /// </summary>
            Available,
            /// <summary>
            /// Content cannot be created of this type
            /// </summary>
            Unavailable,
            /// <summary>
            /// Default, not set
            /// </summary>
            NotSet
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="availableInEditMode"></param>
        /// <param name="order"></param>
        /// <param name="displayName"></param>
        /// <param name="groupName"></param>
        /// <param name="description"></param>
        public ContentTypeExtendedAttribute
        (
            EditMode availableInEditMode = EditMode.NotSet,
            int order = 0,
            string displayName = null,
            string groupName = null,
            string description = null
        ) : base()
        {
            if (availableInEditMode != EditMode.NotSet)
            {
                AvailableInEditMode = availableInEditMode == EditMode.Available;
            }

            Order = order;
            DisplayName = displayName;
            GroupName = groupName;
            Description = description;
        }

        /// <summary>
        /// Do not allow GUIDs to be read from interfaces
        /// </summary>
        [Obsolete("Cannot assign GUID here", true)]
        public new string GUID { get; set; }
    }
}