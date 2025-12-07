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
        // Start is called before the first frame update
        void Start()
        {
            _uiManager.PushPage<HomeScreen>();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}