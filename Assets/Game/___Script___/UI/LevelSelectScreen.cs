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
        [Inject] GameDat _gameDat;

        public void TouchPlay()
        {
            _uiManager.PushPage<PlayScreen>();
        }

        public void TouchBack()
        {
            _uiManager.PushPage<HomeScreen>();
        }
        public void TouchLevel(int level)
        {
            _gameDat.currentLevel = level;
            _uiManager.PushPage<PlayScreen>();
        }
    }
}
