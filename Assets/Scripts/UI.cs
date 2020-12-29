using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {
    public GameObject tileWindow;
    public GameObject editTileBTN;
    public GameObject addTileMenu;
    private Palette _palette => FindObjectOfType<Palette>();

    private void RefreshWindow(bool removeOnly)
    {
        
        for (var i = 0; i < tileWindow.transform.childCount; i++) {
            Destroy(tileWindow.transform.GetChild(i).gameObject);
        }
        if (removeOnly) return;
        foreach (var tile in Palette.TilePalette) {
            var tileUI = Instantiate(new GameObject(), tileWindow.transform);
            tileUI.AddComponent<RectTransform>();
            tileUI.AddComponent<RawImage>().color = tile.GetComponent<SpriteRenderer>().color;
            tileUI.AddComponent<HoverTileText>();
            tileUI.name = tile.name;
        }
    }
    
    public void OpenTileWindow()
    {
        if (!tileWindow.activeInHierarchy) {
            tileWindow.SetActive(true);
            editTileBTN.transform.GetChild(0).GetComponent<Text>().text = "Close Tile Edit";
            RefreshWindow(false);
        }
        else {
            RefreshWindow(true);
            tileWindow.SetActive(false);
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