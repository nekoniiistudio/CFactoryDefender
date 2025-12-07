using System.Collections;
using System.Collections.Generic;
using RSFramework;
using UnityEngine;
using UnityScreenNavigator.Runtime.Core.Page;
using VContainer;

namespace Game
{
    public class PlayScreen : Page
    {
        [Inject] IUIManager _uiManager; 
        public void TouchSetting()
        {
            _uiManager.PushPopup<SettingPopup>();
        }
    }
}