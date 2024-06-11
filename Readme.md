# Orchard Core Commerce

[![Join the chat at https://discord.gg/PtaYnX63](https://img.shields.io/badge/chat_on-Discord-7289d9?color=7289d9)](https://discord.gg/PtaYnX63) [![Documentation](https://readthedocs.org/projects/orchardcorecommerce/badge/)](https://commerce.orchardcore.net/) [![OrchardCore.Commerce NuGet](https://img.shields.io/nuget/v/OrchardCore.Commerce?label=OrchardCore.Commerce)](https://www.nuget.org/packages/OrchardCore.Commerce/) [![Cloudsmith](https://api-prd.cloudsmith.io/badges/version/orchardcore/commerce/nuget/OrchardCore.Commerce/latest/x/?render=true&badge_token=gAAAAABey9hKFD_C-ZIpLvayS3HDsIjIorQluDs53KjIdlxoDz6Ntt1TzvMNJp7a_UWvQbsfN5nS7_0IbxCyqHZsjhmZP6cBkKforo-NqwrH5-E6QCrJ3D8%3D)](https://cloudsmith.io/~orchardcore/repos/commerce/packages/detail/nuget/OrchardCore.Commerce/latest/)

The commerce module for [Orchard Core](https://github.com/OrchardCMS/OrchardCore).

It's also available on all sites of [DotNest, the Orchard SaaS](https://dotnest.com/).

Do you want to chat with other community members? [Check out our channel on the Orchard discord server.](https://discord.gg/PtaYnX63)

## History, status, and planning

Orchard Core Commerce will be an Orchard Core port and partial rewrite of the open source [Nwazet Commerce](https://github.com/bleroy/Nwazet.Commerce) module that was built for Orchard CMS 1.x. Nwazet Commerce was initially built in 2012 by [Bertrand Le Roy](https://github.com/bleroy), loosely based on [a commerce sample](http://www.ideliverable.com/blog/writing-an-orchard-webshop-module-from-scratch-part-1) by [Sipke Schoorstra](https://github.com/sfmskywalker). The initial goal of Nwazet Commerce was to power the web site of the hardware startup Nwazet. While Nwazet is no longer operating, the Nwazet Commerce project went on, and was further developed by a group of passionate contributors who are using the platform for their own, and their customer's web sites.

Like Orchard, Nwazet Commerce was built with extensibility in mind, and as such it has its own extensions (typical examples include local tax and shipping modules). It's also pure, idiomatic Orchard.

Orchard Core represents a major evolution of the Orchard design principles, and is sufficiently different that running Nwazet Commerce on it will necessitate significant work. As such, we've decided that starting from a blank slate was the best way to go, so we'll port Nwazet Commerce piece by piece, being careful to accurately apply Orchard Core's new design principles. We also decided to adopt a new name, that gets rid of the now obsolete origins, and establishes our ambition for the module to become the go-to commerce module for Orchard Core.

This work is in its initial design phases. There's a lot of work to do, and yes, we do welcome participation in any shape or form. The first stage of this was the [minimum viable feature set](https://github.com/OrchardCMS/OrchardCore.Commerce/milestone/1) which has now been released to NuGet [here](https://www.nuget.org/packages/OrchardCore.Commerce/1.0.0). [Here](https://youtu.be/Sw2jvE82UwE) you can watch a demo video about the MVP.

See the [discussions page](https://github.com/OrchardCMS/OrchardCore.Commerce/discussions) for the latest news and announcements.

## Setting up your dev environment

### Pre-requisites

This project uses [`Lombiq Node.js Extensions`](https://github.com/Lombiq/NodeJs-Extensions) to compile and lint client-side assets. See its pre-requisites [here](https://github.com/Lombiq/NodeJs-Extensions/tree/dev#pre-requisites).

If you want to include OrchardCore.Commerce as a git submodule instead of NuGet package, include the [`Lombiq Helpful Libraries`](https://github.com/Lombiq/Helpful-Libraries/) and `Lombiq Node.js Extensions` submodules as well. In your solution directory create a _Directory.Build.props_ file if one doesn't exist. This should contain the properties that configure the path to the _Lombiq.HelpfulLibraries.OrchardCore.csproj_ file and the _Lombiq.NodeJs.Extensions_ project directory. For example:

```xml
<Project>
  <PropertyGroup>
    <LombiqHelpfulLibrariesPath>$(MSBuildThisFileDirectory)/src/Libraries/Lombiq.HelpfulLibraries</LombiqHelpfulLibrariesPath>
    <LombiqNodeJsExtensionsPath>$(MSBuildThisFileDirectory)/src/Utilities/Lombiq.NodeJs.Extensions/Lombiq.NodeJs.Extensions</LombiqNodeJsExtensionsPath>
  </PropertyGroup>
</Project>
```

If you have [Lombiq Analyzers](https://github.com/Lombiq/.NET-Analyzers) included in your project as a submodule you should also add the following property to the _Directory.Build.props_ file:

```xml
<Project>
  <PropertyGroup>
    <LombiqAnalyzersPath>$(MSBuildThisFileDirectory)/tools/Lombiq.Analyzers</LombiqAnalyzersPath>
  </PropertyGroup>
</Project>
```

### Setup

1. Clone this repository.
2. Build and run the `OrchardCore.Commerce.Web` project.
3. Thanks to [Auto Setup](https://docs.orchardcore.net/en/latest/docs/reference/modules/AutoSetup/), the site will be set up with the `OrchardCore Commerce - Development` recipe.
4. Go to the dashboard, using the credentials `admin` and `Password1!`.
5. If you want to test Stripe, go to _Configuration_ → _Commerce_ → _Stripe API_. Set the keys to the test keys found [here](docs/features/stripe-payment.md). If the keys are not set, the Stripe payment button won't appear during checkout.
6. Go to _Content_ → _Content Items_, and create your first `Product`.

## Documentation

Check out the complete documentation portal here: <https://commerce.orchardcore.net/>

- [Inventory](docs/features/inventory.md)
- [Products and Prices](docs/features/products-and-prices.md)
- [Promotions](docs/features/promotions.md)
- [Payment providers](docs/features/payment-providers.md)
  - [Exactly® Payment](docs/features/exactly-payment.md)
  - [Stripe Payment](docs/features/stripe-payment.md)
- [Taxation](docs/features/taxation.md)
- [User Features](docs/features/user-features.md)
- [Workflows](docs/features/workflows.md)

## Demo video

[![Watch the video](https://img.youtube.com/vi/EVvwS1UaIk4/maxresdefault.jpg)](https://youtu.be/EVvwS1UaIk4)
