using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using RSFramework.Service;
using UnityScreenNavigator.Runtime.Core.Modal;
using UnityScreenNavigator.Runtime.Core.Page;

namespace RSFramework
{
    /// <summary>
    /// LifetimeScope for this class for important building function (Dont have yet)
    /// </summary>
    public static class RSFrameworkInstaller
    {
        public static void Install(IContainerBuilder builder)
        {
            builder.Register<IAnalysisService, AnalysisService>(Lifetime.Scoped);
            Debug.Log("RSFrameworkInstaller Finish Configuration");
        }
    }

    public static class UIManagerInstaller
    {
        /// <summary>
        /// Call in the local scope that manager the local UI
        /// </summary>
        /// <param name="builder"></param>
        public static void Install(IContainerBuilder builder)
        {
            builder.Register<PageFactory>(Lifetime.Singleton);
            builder.Register<PopupFactory>(Lifetime.Singleton);
            
            builder.RegisterEntryPoint<UIManager>();
            builder.RegisterComponentInHierarchy<PageContainer>();
            builder.RegisterComponentInHierarchy<PopupContainer>();
        }
    }
}