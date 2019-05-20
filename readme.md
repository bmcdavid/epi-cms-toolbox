# Read Me

The CMS toolbox contains many useful tools for developers to use in Episerver CMS projects.

* Strategy based content area rendering
* Strongly typed content area rendering options
* Sync attributes from interfaces to IContent models allowing composition in favor of inheritance.
* Feature Manager
* Episerver Environment 
* Ancestor property finder

## Strategy Based ContentArea Rendering

The strategy based content area renderer is a very flexible content area renderer that can handle about any use case. A default strategy may also be applied using the **IDefaultContentStrategy** interface, which defaults to nothing/null. Several strategies are provided in this package, but any additional ones can be created by implementing **IContentAreaItemRenderingStrategy**. Below are the ones provided in this package:

* Even / odd CSS class item rendering
* First / last CSS class item rendering
* Items per row grouping strategy
* Alloy based item rendering

### Rendering Options

The standard way of passing options for content area rendering involve passing a dynamic object, which often leaves developers, especially new ones to Episerver guessing what should be passed. This package includes a **ContentAreaRenderOptions** class to eliminate the guessing game. The options also include additional properties that the strategy based content area renderer uses, which are noted below:

* Prefiltered (bool) - This instructs the content area rendering that the given items have already been filtered for the site visitor, bypassing the default filtering behavior.
* ContentAreaItemRenderingStrategy (IContentAreaItemRenderingStrategy) - Gives the strategy based content area renderer great flexibility, allowing many different strategies to be used.
* ITemplateResolverItemStrategy - Gives the strategy based content area renderer the ability to determine how templates are chosen. One strategy is provided in this package for allowing a single resolve per IContent type in the content area.

## Attribute Sync for Interface (Composition)

Allows developers the choice of using composition in favor over inheritance. The provided **SyncTypeAttribute** can be applied to any IContentData property, which requires two constructor arguments, the Type and property name to sync attributes from. This allows for interfaces to have Episerver and other attributes applied to their properties and during Episerver initialization they are applied to the classes implementing the interface.

## Content Data Rendering Models

Blocks are a very powerful component in Episerver allowing developers and editors to share components/content in a variety of ways. Many times these blocks will require services such as an IContentLoader to transform content references into fully populated models. Often to do this, develoeprs will follow traditional MVC and create a controller and view model for these blocks. The CMS toolbox provides an **IRendering<T>** interface to remove the controller portion in these simpler scenarios. A developer simply needs to create a view model implementing the previously mentioned interface and in the Razor file for the native block, transform it. See the rendering tests for more details.

## Cms Toolbox Configuration

Some developers may not want every tool use all tools in the toolbox. A configuration class is provided to allow developers to disable configurable modules that overwrite default Episerver behavior, which can be applied in an **IConfigurableModule** with no dependencies.

```cs
[InitializableModule]
public class DisableAttributeSyncAndStrategyContentRendering : IConfigurableModule
{
    public void ConfigureContainer(ServiceConfigurationContext context)
    {
        CmsToolboxConfiguration.Configure(config =>
        {
            config
                .DisableAttributeSync()
                .DisableContentRenderer();
        });
    }
}
```

## Episerver Environment

Code that is aware of which environment it executes in allows for developers to create some functionality stored in version control and not have to worry with extra configuration. For example, many sites don't want their content indexed by search engines if the environment isn't production. Having environment aware code simplifies this. In a root MVC razor layout that has an **IEpiserverEnvironment** property, a developer can simply add:

```cs
@if(!Model.EpiserverEnvironment.IsProduction())
{
	<meta name="robots" content="noindex,nofollow" />
}
```

And this robots tag is then applied to all environments but production.