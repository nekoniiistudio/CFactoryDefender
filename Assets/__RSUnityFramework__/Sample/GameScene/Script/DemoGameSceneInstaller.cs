using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

using RSFramework;

namespace DemoGameScene
{
    public class DemoGameSceneInstaller : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            RSFrameworkInstaller.Install(builder);
            UIManagerInstaller.Install(builder);
            // Find and register the existed class in scene
            builder.RegisterComponentInHierarchy<DemoGeneralClass>();
        }
    }
}

