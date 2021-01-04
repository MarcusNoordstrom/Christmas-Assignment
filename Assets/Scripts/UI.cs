using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {
    public GameObject tileEditWindow;
    public GameObject editTileBTN;
    public GameObject addTileMenu;
    public GameObject addTileBtn;
     Palette _palette => FindObjectOfType<Palette>();
    
     void RefreshWindow(bool removeOnly)
    {
        for (var i = 0; i < tileEditWindow.transform.childCount; i++) {
            Destroy(tileEditWindow.transform.GetChild(i).gameObject);
        }
        if (removeOnly) return;
        
        foreach (var tile in Palette.TilePalette) {
            GameObject tileUI = new GameObject();
            Destroy(tileUI);
            tileUI.AddComponent<RectTransform>();
            tileUI.AddComponent<RawImage>().color = tile.GetComponent<SpriteRenderer>().color;
            tileUI.AddComponent<HoverTileText>();
            tileUI.name = tile.name;
            Instantiate(tileUI, tileEditWindow.transform);
        }
        Instantiate(addTileBtn, tileEditWindow.transform);
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
    
    public void OpenAddTileToWindow()
    {
        if (!addTileMenu.activeInHierarchy) {
            addTileMenu.SetActive(true);
        }
        else {
            addTileMenu.SetActive(false);
        }
    }
    
    public void AddTileFinished()
    {
        var name = addTileMenu.transform.GetChild(0).GetComponent<Text>().text;
        var colorValues = addTileMenu.transform.GetChild(1).GetComponent<Color>();
        var color = new Color(colorValues.r, colorValues.g, colorValues.b);
        _palette.AddToPalette(color, name);
    }
}