using System.Collections;
using System.Collections.Generic;
using RSFramework;
using UnityEngine;
using VContainer;

namespace DemoGameScene
{
    public class DemoGeneralClass : MonoBehaviour
    {
        [Inject] IUIManager _UIManager;

        void Start()
        {
            PushPageA();
        }

        public void PushPageA()
        {
            _UIManager.PushPage<PageA>();
        }
    }
}