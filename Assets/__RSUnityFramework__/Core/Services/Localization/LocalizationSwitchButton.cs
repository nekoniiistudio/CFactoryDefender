using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace RSFramework
{
    [RequireComponent(typeof(Button))]
    public class LocalizationSwitchButton : MonoBehaviour
    {
        public int languageIndex = 0;

        private void Awake()
        {
            Button button = GetComponent<Button>();
            button.onClick.RemoveListener(() => TouchSwitchLanguage(languageIndex)); 
            button.onClick.AddListener(() => TouchSwitchLanguage(languageIndex)); 
        }

        public void TouchSwitchLanguage(int index)
        {  
            if (index >= 0 && index < LocalizationSettings.AvailableLocales.Locales.Count)
            {
                LocalizationSettings.SelectedLocale =
                    LocalizationSettings.AvailableLocales.Locales[index];
            }
        }
    }
}