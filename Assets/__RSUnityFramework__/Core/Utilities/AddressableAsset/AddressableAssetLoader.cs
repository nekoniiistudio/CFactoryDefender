using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace RSFramework.Utilities
{
    public static class AddressableAssetLoader
    {//Load asset
        public static UniTask<IList<T>> LoadAssetsByKey<T>(this IList<T> retList, string key)
        {
            Addressables.LoadAssetsAsync<T>(key, obj =>
            {
                retList.Add(obj);
            }).WaitForCompletion();
            return default;
        }
        //Clean current assetlist then reload asset
        public static UniTask<IList<T>> ReLoadAssetsByKey<T>(this IList<T> retList, string key)
        {
            retList.Clear();
            Addressables.LoadAssetsAsync<T>(key, obj =>
            {
                retList.Add(obj);
            }).WaitForCompletion();
            return default;
        }

        public static async UniTask<IList<T>> LoadAssetsByKeyFromPrefabs<T>(this IList<T> retList, string key)
       
        {
            // Load prefabs matching the key
            var prefabs = new List<GameObject>();
            await prefabs.LoadAssetsByKey(key);

            // Create result list (can reuse retList if needed)
            retList ??= new List<T>();

            // Extract components of type T from each prefab
            foreach (var prefab in prefabs)
            {
                if (prefab.TryGetComponent(out T component))
                    retList.Add(component);
            }

            return retList;
        }

    }
}
