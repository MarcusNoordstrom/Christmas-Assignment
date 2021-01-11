using System.Collections.Generic;
using System.Linq;
using TileSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface {
    public class Hotkeys : MonoBehaviour {
        public List<GameObject> slotHolder = new List<GameObject>();
        public bool bindingMode;

        public GameObject currentSelectedTile;

        void Awake() {
            for (var i = 0; i < gameObject.transform.childCount; i++) {
                if (i <= 8) {
                    slotHolder.Add(gameObject.transform.GetChild(i).gameObject);
                }
            }
            bindingMode = false;
        }

        public void UpdateHotbars(Color oldColor, Color newColor) {
            foreach (var slot in slotHolder.Where(slot => slot.GetComponent<Image>().color == oldColor)) {
                slot.GetComponent<Image>().color = newColor;
            }
        }

        void Update() {
            AddToHotbar();
            CurrentSelectedTile();
        }

        void SelectedTileHelper(int index) {
            for (int i = 0; i < slotHolder.Count; i++) {
                if (i == index) {
                    slotHolder[i].transform.GetChild(1).gameObject.SetActive(true);
                }
                else {
                    slotHolder[i].transform.GetChild(1).gameObject.SetActive(false);
                }
            }
        }
    
        void CurrentSelectedTile() {
            if (bindingMode) return;
            var input = Input.inputString;
            int chosenIndex;
            int.TryParse(input, out chosenIndex);

            if (chosenIndex >= 1 && chosenIndex <= 9) {
                SelectedTileHelper(chosenIndex - 1);
            
                //TODO: GET TILE HERE
                for (int i = 0; i < Palette.TilePalette.Count; i++) {
                    if (slotHolder[chosenIndex - 1].GetComponent<Image>().color ==
                        Palette.TilePalette[i].GetComponent<SpriteRenderer>().color) {
                        currentSelectedTile = Palette.TilePalette[i];
                    }
                }
            }
        }

        void AddToHotbar() {
            if (!bindingMode) return;
            if (Input.GetKeyDown(KeyCode.Alpha1) && HoverOver.SelectedTile != null) {
                slotHolder[0].GetComponent<Image>().color = HoverOver.SelectedTile.GetComponent<RawImage>().color;
            }
        
            if (Input.GetKeyDown(KeyCode.Alpha2) && HoverOver.SelectedTile != null) {
                slotHolder[1].GetComponent<Image>().color = HoverOver.SelectedTile.GetComponent<RawImage>().color;
            }
        
            if (Input.GetKeyDown(KeyCode.Alpha3) && HoverOver.SelectedTile != null) {
                slotHolder[2].GetComponent<Image>().color = HoverOver.SelectedTile.GetComponent<RawImage>().color;
            }
        
            if (Input.GetKeyDown(KeyCode.Alpha4) && HoverOver.SelectedTile != null) {
                slotHolder[3].GetComponent<Image>().color = HoverOver.SelectedTile.GetComponent<RawImage>().color;
            }
        
            if (Input.GetKeyDown(KeyCode.Alpha5) && HoverOver.SelectedTile != null) {
                slotHolder[4].GetComponent<Image>().color = HoverOver.SelectedTile.GetComponent<RawImage>().color;
            }
        
            if (Input.GetKeyDown(KeyCode.Alpha6) && HoverOver.SelectedTile != null) {
                slotHolder[5].GetComponent<Image>().color = HoverOver.SelectedTile.GetComponent<RawImage>().color;
            }
        
            if (Input.GetKeyDown(KeyCode.Alpha7) && HoverOver.SelectedTile != null) {
                slotHolder[6].GetComponent<Image>().color = HoverOver.SelectedTile.GetComponent<RawImage>().color;
            }
        
            if (Input.GetKeyDown(KeyCode.Alpha8) && HoverOver.SelectedTile != null) {
                slotHolder[7].GetComponent<Image>().color = HoverOver.SelectedTile.GetComponent<RawImage>().color;
            }
        
            if (Input.GetKeyDown(KeyCode.Alpha9) && HoverOver.SelectedTile != null) {
                slotHolder[8].GetComponent<Image>().color = HoverOver.SelectedTile.GetComponent<RawImage>().color;
            }
        }
    }
}