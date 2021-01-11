using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using HSVPicker;
using MapLogic;
using TileSystem;
using TMPro;

namespace UserInterface {
    public class UI : MonoBehaviour {
        public GameObject tileEditWindow;
        public GameObject editTileBTN;
        public GameObject deleteBTN;
        public GameObject tileAddMenu;
        public GameObject addBTN;
    
        public GameObject mapPrefsWindow;
        public GameObject mapWidthInputField;
        public GameObject mapHeightInputField;
    
        public GameObject instructions;
        public GameObject editModeText;
    
        public ColorPicker picker;
        public InputField tileNameInput;
        Palette _palette => FindObjectOfType<Palette>();
        public static bool EditMode;

        public GameObject pickerHandleBar;


        void Awake() {
            pickerHandleBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        }

        void RefreshWindow(bool removeOnly)
        {
            for (var i = 0; i < tileEditWindow.transform.childCount; i++) {
                if (tileEditWindow.transform.GetChild(i).gameObject.name != "OpenAddTileBTN" && tileEditWindow.transform.GetChild(i).gameObject.name != "EditTileBTN") {
                    Destroy(tileEditWindow.transform.GetChild(i).gameObject);
                }
            }
            if (removeOnly) return;
        
            foreach (var tile in Palette.TilePalette) {
                GameObject tileUI = new GameObject();
                Destroy(tileUI);
                tileUI.AddComponent<RawImage>().color = tile.GetComponent<SpriteRenderer>().color;
                tileUI.name = tile.name;
                tileUI.AddComponent<HoverOver>();
                tileUI.tag = "Tile";
                tileUI.AddComponent<CanvasGroup>();
            
            
                GameObject tileUIText = new GameObject();
                Destroy(tileUIText);
            
                tileUIText.AddComponent<TextMeshProUGUI>().text = tileUI.name;
            
                if (tileUI.GetComponent<RawImage>().color == Color.black) {
                    tileUIText.GetComponent<TextMeshProUGUI>().color = Color.white;
                }
                else {
                    tileUIText.GetComponent<TextMeshProUGUI>().color = Color.black;
                }
            
                tileUIText.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
                tileUIText.GetComponent<TextMeshProUGUI>().fontSize = 12;
                tileUIText.GetComponent<TextMeshProUGUI>().raycastTarget = false;
            
                Instantiate(tileUIText, tileUI.transform);
                Instantiate(tileUI, tileEditWindow.transform);
            }
        }
    
        public void OpenTileWindow()
        {
            if (!tileEditWindow.activeInHierarchy) {
                FindObjectOfType<Hotkeys>().bindingMode = true;
                tileEditWindow.SetActive(true);
                editTileBTN.transform.GetChild(0).GetComponent<Text>().text = "Close Tile Edit";
                RefreshWindow(false);
                instructions.SetActive(true);
            }
            else {
                RefreshWindow(true);
                FindObjectOfType<Hotkeys>().bindingMode = false;
                tileEditWindow.SetActive(false);
                editTileBTN.transform.GetChild(0).GetComponent<Text>().text = "Edit Tiles";
                instructions.SetActive(false);
            }
        }
    
        public void OpenAddTileToWindow() {
            tileAddMenu.SetActive(true); 
            deleteBTN.SetActive(false);
            instructions.SetActive(false);
            if (!EditMode) return;
            addBTN.GetComponent<TextMeshProUGUI>().text = "Change";
            deleteBTN.SetActive(true);
            editModeText.SetActive(false);
        }

        public void DeleteTile()
        {
            var tileToRemove = HoverOver.SelectedTile.name.Remove(HoverOver.SelectedTile.name.Length - 7, 7);
            _palette.RemoveFromPalette(tileToRemove);
            EditModeSwitch();
            CloseAddTileWindow();
            RefreshWindow(false);
            AssetDatabase.Refresh();
        }

        //TODO: MOVE LOGIC FROM HERE TO PALETTE.CS
        public void ChangeTile()
        {
            var tileToChange = HoverOver.SelectedTile;
        
            FindObjectOfType<Hotkeys>().UpdateHotbars(tileToChange.GetComponent<RawImage>().color, picker.CurrentColor);
        
            tileToChange.GetComponent<RawImage>().color = picker.CurrentColor;
            tileToChange.name = tileNameInput.text;
            _palette.ChangeTileInPalette(tileToChange, picker.CurrentColor, tileNameInput.text);
            CloseAddTileWindow();
            FindObjectOfType<Map>().RefreshMap();
            tileToChange.name = tileToChange.name + "(Clone)";
        }

        public void ToggleMapPrefsWindow() {
            mapPrefsWindow.SetActive(!mapPrefsWindow.activeInHierarchy);
        }

        public void ChangeMapPrefs() {
            var map = FindObjectOfType<Map>();
            var mapHeight = 0;
            int.TryParse(mapHeightInputField.GetComponent<InputField>().text, out mapHeight);

            var mapWidth = 0;
            int.TryParse(mapWidthInputField.GetComponent<InputField>().text, out mapWidth);

            map.mapWidth = mapWidth;
            map.mapHeight = mapHeight;
            map.ResetMap(false);
        }

        public void EditModeSwitch()
        {
            EditMode = !EditMode;
            if (EditMode) {
                editModeText.SetActive(true);
            }
            else {
                editModeText.SetActive(false);
            }
        }

        public void CloseAddTileWindow() {
            tileAddMenu.SetActive(false);
            instructions.SetActive(true);
        }
    
        public void AddTileFinished()
        {
            if (EditMode) {
                ChangeTile();
                instructions.SetActive(true);
            }
            else {
                tileAddMenu.SetActive(false);
                _palette.AddToPalette(picker.CurrentColor, tileNameInput.text);
                RefreshWindow(false);
                instructions.SetActive(true);
            }
        }
    }
}