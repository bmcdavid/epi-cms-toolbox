using EPiServer.Core;
using EPiServer.DataAbstraction.RuntimeModel;
using EPiServer.DataAnnotations;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace bmcdavid.Episerver.CmsToolbox
{
    /// <summary>
    /// Decorated IContentTypeModelAssigned to provide additional synchronization
    /// </summary>
    public class AttributeProviderContentTypeModelAssigner : IContentTypeModelAssigner
    {
        private readonly IContentTypeModelAssigner _defaultContentModelAssigner;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="defaultAssigner"></param>
        public AttributeProviderContentTypeModelAssigner(IContentTypeModelAssigner defaultAssigner) => _defaultContentModelAssigner = defaultAssigner;

        /// <summary>
        /// Assigns values for interfaces targets
        /// </summary>
        /// <param name="contentTypeModel"></param>
        public void AssignValues(ContentTypeModel contentTypeModel)
        {
            var originalModelType = contentTypeModel.ModelType;

            foreach (var i in contentTypeModel.ModelType.GetInterfaces())
            {
                SyncType(contentTypeModel, i);
            }

            if (contentTypeModel.ModelType.BaseType is object && contentTypeModel.ModelType.BaseType.IsClass && contentTypeModel.ModelType.BaseType != typeof(PageData))
            {
                SyncType(contentTypeModel, contentTypeModel.ModelType.BaseType);
            }

            SyncType(contentTypeModel, contentTypeModel.ModelType);
            contentTypeModel.ModelType = originalModelType;
            _defaultContentModelAssigner.AssignValues(contentTypeModel);

            var allowedTypes = contentTypeModel.Attributes.GetAllAttributes<AvailableContentTypesAttribute>()?.LastOrDefault();
            contentTypeModel.AvailableContentTypes = allowedTypes ?? contentTypeModel.AvailableContentTypes;
        }

        /// <summary>
        /// Assigns values for synced interface attributes
        /// </summary>
        /// <param name="propertyDefinitionModel"></param>
        /// <param name="property"></param>
        /// <param name="contentTypeModel"></param>
        public void AssignValuesToPropertyDefinition(PropertyDefinitionModel propertyDefinitionModel, PropertyInfo property, ContentTypeModel contentTypeModel)
        {
            var providedAttr = property.GetCustomAttribute<AttributeProviderAttribute>(inherit: true);

            if (providedAttr is object && providedAttr.PropertyName is string)
            {
                var providedProp = ((providedAttr as SyncAttributesAttribute)?.ProviderType ?? Type.GetType(providedAttr.TypeName, false))?
                    .GetProperty(providedAttr.PropertyName);

                if (providedProp is object)
                {
                    _defaultContentModelAssigner
                        .AssignValuesToPropertyDefinition(propertyDefinitionModel, providedProp, contentTypeModel);
                }
            }

            _defaultContentModelAssigner.AssignValuesToPropertyDefinition(propertyDefinitionModel, property, contentTypeModel);
        }

        private void SyncType(ContentTypeModel contentTypeModel, Type checkType)
        {
            foreach (var syncAttr in checkType.GetCustomAttributes(typeof(SyncTypeAttribute)).OfType<SyncTypeAttribute>())
            {
                contentTypeModel.ModelType = syncAttr.ProviderType;
                _defaultContentModelAssigner.AssignValues(contentTypeModel);
            }
        }
    }
}