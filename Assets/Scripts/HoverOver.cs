using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverOver : MonoBehaviour, IPointerUpHandler ,IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler {

    GameObject tileToPutInHotbar;
    private GameObject tileAddWindow;
    public static GameObject SelectedTile;

    Vector3 _oldPosition;

    Canvas _canvas;
    CanvasGroup _canvasGroup;
    RectTransform _rectTransform;
    
    //TODO: Fix the issue with the "Dragging" where tile is not exactly on mouse.
    //TODO: Fix the issue where when you drop a tile it gets put to Slot 7 automatically?
    
    private void Awake()
    {
        _canvas = FindObjectOfType<Canvas>().GetComponent<Canvas>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _rectTransform = GetComponent<RectTransform>();

        SelectedTile = null;
    }

    // public void OnPointerEnter(PointerEventData eventData) {
    //     Debug.Log($"Currently hovering: {this.GetComponentInParent<Transform>().name}");
    // }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!UI.EditMode) return;
        var editWindow = FindObjectOfType<UI>();
        editWindow.OpenAddTileToWindow();
        editWindow.picker.CurrentColor = this.GetComponent<RawImage>().color;
        editWindow.tileNameInput.text = this.name.Remove(this.name.Length - 7, 7);
        SelectedTile = this.gameObject;
    }

    public void OnDrag(PointerEventData eventData) {
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }
    
    public void OnBeginDrag(PointerEventData eventData) {
        _oldPosition = _rectTransform.anchoredPosition;
        _canvasGroup.alpha = 0.6f;
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.alpha = 1f;
    }
    
    PointerEventData _pointerEventData = new PointerEventData(EventSystem.current);
    
    public void OnPointerUp(PointerEventData eventData) {
        bool CantPutHere = false;
        _pointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(_pointerEventData, results);
        
        //Debug.Log(results.Count);

        if (results.Count <= 0) {
            _rectTransform.anchoredPosition = _oldPosition;
        }
        else if (results.Count > 0) {
            for (int i = 0; i < results.Count; i++) {
                if (results[i].gameObject.CompareTag("Tile")) {
                    CantPutHere = true;
                }
                else if (results[i].gameObject.CompareTag("TileSlot") && !CantPutHere) {
                    _rectTransform.anchoredPosition =
                        results[i].gameObject.GetComponent<RectTransform>().anchoredPosition;
                    _oldPosition = _rectTransform.anchoredPosition;
                    gameObject.transform.SetParent(results[i].gameObject.transform);
                }
                else if(CantPutHere){
                    _rectTransform.anchoredPosition = _oldPosition;
                }
            }
        }
    }
    
}
