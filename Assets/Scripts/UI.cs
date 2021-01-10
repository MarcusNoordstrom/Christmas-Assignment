using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using HSVPicker;
using TMPro;
using UnityEngine.UIElements;

public class UI : MonoBehaviour {
    public GameObject tileEditWindow;
    public GameObject editTileBTN;
    public GameObject deleteBTN;
    public GameObject tileAddMenu;
    public GameObject addBTN;
    
    public ColorPicker picker;
    public InputField tileNameInput;
    Palette _palette => FindObjectOfType<Palette>();
    public static bool EditMode;

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

        // for (var i = 0; i < tileEditWindow.transform.childCount; i++) {
        //     if (tileEditWindow.transform.GetChild(i).gameObject.name == "OpenAddTileBTN") {
        //         tileEditWindow.transform.GetChild(i).gameObject.transform.SetAsLastSibling();
        //     }
        //
        //     if (tileEditWindow.transform.GetChild(i).gameObject.name == "EditTileBTN") {
        //         tileEditWindow.transform.GetChild(i).gameObject.transform.SetAsLastSibling();
        //     }
        // }
    }
    
    public void OpenTileWindow()
    {
        if (!tileEditWindow.activeInHierarchy) {
            tileEditWindow.SetActive(true);
            editTileBTN.transform.GetChild(0).GetComponent<Text>().text = "Close Tile Edit";
            RefreshWindow(false);
        }
        else {
            RefreshWindow(true);
            tileEditWindow.SetActive(false);
            editTileBTN.transform.GetChild(0).GetComponent<Text>().text = "Edit Tiles";
        }
    }
    
    public void OpenAddTileToWindow() {
        tileAddMenu.SetActive(true); 
        deleteBTN.SetActive(false);
        if (!EditMode) return;
        addBTN.GetComponent<TextMeshProUGUI>().text = "Change";
        deleteBTN.SetActive(true);
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

        tileToChange.GetComponent<RawImage>().color = picker.CurrentColor;
        tileToChange.name = tileNameInput.text;
        _palette.ChangeTileInPalette(tileToChange, picker.CurrentColor, tileNameInput.text);
        CloseAddTileWindow();
    }

    public void EditModeSwitch()
    {
        EditMode = !EditMode;
    }

    public void CloseAddTileWindow() {
        tileAddMenu.SetActive(false);
    }
    
    public void AddTileFinished()
    {
        if (EditMode) {
            ChangeTile();
        }
        else {
            tileAddMenu.SetActive(false);
            _palette.AddToPalette(picker.CurrentColor, tileNameInput.text);
            RefreshWindow(false);
        }
    }
}