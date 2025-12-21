using System.Collections;
using System.Collections.Generic;
using RSFramework;
using UnityEngine;
using VContainer;

namespace Game
{


    public class GeneralObject : MonoBehaviour
    {
        [Inject] IUIManager _uiManager;
        [Inject] TestData _testData;
        // Start is called before the first frame update
        void Start()
        {
            _uiManager.PushPage<HomeScreen>();
            Debug.Log(_testData.gameName);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}