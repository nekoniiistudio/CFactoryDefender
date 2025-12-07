using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RSFramework.UI
{
    public class TextItemUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI tx_text;

        public void ShowText(string tx)
        {
            tx_text.text = tx;
        }
    }
}