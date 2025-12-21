using System.Collections;
using System.Collections.Generic;
using RSFramework;
using UnityEngine;
using UnityScreenNavigator.Runtime.Core.Page;
using VContainer;

namespace Game
{
 
    public class LevelSelectScreen : Page
    {
        [Inject] IUIManager _uiManager;

        public void TouchPlay()
        {
            _uiManager.PushPage<PlayScreen>();
        }

        public void TouchBack()
        {
            _uiManager.PushPage<HomeScreen>();
        }
    }
}
