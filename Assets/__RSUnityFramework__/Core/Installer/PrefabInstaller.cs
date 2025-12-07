using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace RSUnityFramework.Installer
{
    public interface IPrefabFactory
    {
        T Create<T>(GameObject prefab) where T : Component;
    }
    public class PrefabFactory 
    {
        private IObjectResolver _resolver;
        [Inject]
        public void Construct(IObjectResolver resolver)
        {
            _resolver = resolver;
        }
        public T Instantiate<T>(GameObject prefab) where T : Component
        {
            var instance = _resolver.Instantiate(prefab);
            _resolver.InjectGameObject(instance); // auto inject dependencies
            return instance.GetComponent<T>();
        }
    }
    
}
