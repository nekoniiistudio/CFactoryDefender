using System.Collections;
using System.Collections.Generic;
using RSFramework.UI;
using UnityEngine;

namespace RSFramework
{
    public class DebugInfoManager : MonoBehaviour
    {
        public Transform container;

        [SerializeField] private TextItemUI pp_TextItem;
        private List<TextItemUI> textItems = new();

        private TextItemUI GetTextItem(int index)
        {
            // Ensure list has enough items
            while (textItems.Count <= index)
            {
                var newItem = Instantiate(pp_TextItem, container);
                textItems.Add(newItem);
            }

            return textItems[index];
        }
        public void UpdateAxisExample(Vector2 input)  => GetTextItem(0).ShowText($"UpdateAxisExample: {input.x:00},{input.y:00}");
        public void UpdateMessageExample(string messege) => GetTextItem(1).ShowText($"UpdateMessageExample: {messege}");
        
    }
    
}