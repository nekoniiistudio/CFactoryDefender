using System.Collections;
using System.Collections.Generic;
using RSFramework;
using UnityEngine;
using UnityScreenNavigator.Runtime.Core.Modal;
using VContainer;

namespace Game
{
    public class SettingPopup : Popup
    {
        [Inject] IUIManager _uiManager;
        public async void TouchBack()
        {
            await _uiManager.PopPopup();
        }
        public async void TouchBackToHome()
        {
            await _uiManager.PopAllPopup();
            _uiManager.PushPage<HomeScreen>();
        }
    }
}