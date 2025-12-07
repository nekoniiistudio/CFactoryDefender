using RSFramework.ResourceManagement;
using VContainer;

namespace RSFramework.ResourceManagement
{
    public static class ResourceLoaderInstaller
    {
        public static void Install(IContainerBuilder builder)
        {
            builder.Register<IAssetLoader, AddressableAssetLoader>(Lifetime.Singleton)
                .Keyed(ResourceLoaderType.AddressableAsset);

            builder.Register<IAssetLoader, ResourcesAssetLoader>(Lifetime.Singleton)
                .Keyed(ResourceLoaderType.ResourceAsset);
        }
    }

    public enum ResourceLoaderType
    {
        AddressableAsset,
        ResourceAsset,
    }
}

