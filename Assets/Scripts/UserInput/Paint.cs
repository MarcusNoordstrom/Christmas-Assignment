using UnityEngine;
using UserInterface;

namespace UserInput {
    public class Paint : MonoBehaviour {
        Hotkeys _hotkeys => FindObjectOfType<Hotkeys>();
        UI ui;

        void Awake() {
            ui = FindObjectOfType<UI>();
        }

        void OnMouseDown() {
            if (_hotkeys.currentSelectedTile == null) return;
            if (ui.tileEditWindow.activeInHierarchy || ui.mapPrefsWindow.activeInHierarchy) return;
            gameObject.name = _hotkeys.currentSelectedTile.name;
            gameObject.GetComponent<SpriteRenderer>().color = _hotkeys.currentSelectedTile.GetComponent<SpriteRenderer>().color;
        }
    }
}