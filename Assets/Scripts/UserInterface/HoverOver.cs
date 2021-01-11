using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UserInterface {
    public class HoverOver : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler {

        GameObject _tileToPutInHotbar;
        GameObject _tileAddWindow;
        public static GameObject SelectedTile;
        Vector3 _oldPosition;
        
        private void Awake()
        {
            SelectedTile = null;
        }
        
        public void OnPointerEnter(PointerEventData eventData) {
            SelectedTile = gameObject;
        }
    
        public void OnPointerDown(PointerEventData eventData)
        {
            if (!UI.EditMode) return;
            var editWindow = FindObjectOfType<UI>();
            editWindow.OpenAddTileToWindow();
            editWindow.picker.CurrentColor = this.GetComponent<RawImage>().color;
            editWindow.tileNameInput.text = this.name.Remove(this.name.Length - 7, 7);
            //SelectedTile = gameObject;
        }
    }
}