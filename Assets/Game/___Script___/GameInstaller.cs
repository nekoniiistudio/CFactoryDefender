using System.Collections;
using System.Collections.Generic;
using RSFramework;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game
{
    public class GameInstaller : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            UIManagerInstaller.Install(builder); 
            //builder.RegisterComponentInHierarchy<GeneralObject>();
            builder.RegisterComponentInHierarchy<TestData>();
        }
    }
}
