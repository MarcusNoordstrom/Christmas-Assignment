using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverTileText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
     GameObject hoverText;
    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverText = Instantiate(new GameObject(), new Vector3(transform.position.x, transform.position.y + 5), Quaternion.identity);
        hoverText.AddComponent<RectTransform>();
        hoverText.AddComponent<Text>().text = eventData.hovered[0].gameObject.name;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Destroy(hoverText.gameObject);
    }
}
