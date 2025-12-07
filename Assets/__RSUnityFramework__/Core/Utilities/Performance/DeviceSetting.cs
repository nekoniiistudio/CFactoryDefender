using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  RSFramework
{
    public class DeviceSetting
    {
        public void SetupQualityForAndroidDevice()
        {
            QualitySettings.vSyncCount = 0;
            if (SystemInfo.processorCount <= 4)
                Application.targetFrameRate = 30; // Máy yếu
            else
                Application.targetFrameRate = 60; // Máy mạnh
        }
    }
}

