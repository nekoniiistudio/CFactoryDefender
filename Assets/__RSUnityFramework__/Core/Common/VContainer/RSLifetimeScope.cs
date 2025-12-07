using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

using System.Reflection;
using Wigi.Utilities;
using System.Linq;

#if UNITY_EDITOR
using Sirenix.OdinInspector;
#endif

namespace RSFramework
{
    public class RSLifetimeScope : LifetimeScope
    {
        protected void RegisterOwnerComponents(IContainerBuilder builder)
        {
            var components = GetComponents<MonoBehaviour>();
            foreach (var component in components)
            {
                if(component is LifetimeScope)
                    continue;
                
                builder.RegisterComponent(component).As(component.GetType());
            }
        }

        /// <summary>
        /// Warning Performance (20ms): Hàm này sẽ quét toàn bộ Components Children của Scope và Register.
        /// </summary>
        /// <param name="builder"></param>
        protected void RegisterAllComponentsInHierarchy(IContainerBuilder builder)
        {
            long time = TimeUtil.Now();
            MonoBehaviour[] allScripts = GetComponentsInChildren<MonoBehaviour>(true);
            foreach (var script in allScripts)
            {
                var type = script.GetType();
                if (HasInjectAttribute(type))
                    builder.RegisterComponent(script).As(script.GetType());
            }
            Debug.Log("RSLifetimeScope Register All: " + (TimeUtil.Now() - time));
        }

        // Hàm kiểm tra xem Class có dùng [Inject] ở đâu đó không (Field, Property, Method)
        private bool HasInjectAttribute(System.Type type)
        {

            // Kiểm tra Field
            if (type.GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)
                .Any(f => f.GetCustomAttribute<InjectAttribute>() != null)) return true;

            // Kiểm tra Property
            // if (type.GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)
            //     .Any(p => p.GetCustomAttribute<InjectAttribute>() != null)) return true;

            // // Kiểm tra Method (ví dụ hàm Construct)
            // if (type.GetMethods(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)
            //     .Any(m => m.GetCustomAttribute<InjectAttribute>() != null)) return true;

            return false;
        }

#if UNITY_EDITOR
        [Button("Reload Inject Objects")]
        private void AutoAddInjectableHiearchy()
        {
            // 2. Quét tất cả MonoBehaviour trong Scene (bao gồm cả đang ẩn)
            MonoBehaviour[] allScripts = GetComponentsInChildren<MonoBehaviour>(true);
            var objectsToAdd = new HashSet<GameObject>(); // Dùng HashSet để tránh trùng lặp

            foreach (var script in allScripts)
            {
                if (script == null || script == this) continue;

                // Kiểm tra xem script này có chứa [Inject] không
                if (HasInjectAttribute(script.GetType()))
                {
                    objectsToAdd.Add(script.gameObject);
                }
            }

            // Xoá các object bị null khỏi danh sách
            autoInjectGameObjects.RemoveAll(item => item == null);

            //Add new Inject Object
            foreach (var obj in objectsToAdd)
            {
                if (autoInjectGameObjects.Contains(obj))
                    continue;
                autoInjectGameObjects.Add(obj);
            }
        }
#endif
    }
}
