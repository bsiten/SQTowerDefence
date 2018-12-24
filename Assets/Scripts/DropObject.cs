using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class DropObject : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Sprite nowSprite;
    public int MinionId;

    void Start()
    {
        nowSprite = null;
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (pointerEventData.pointerDrag == null) return;
        Image droppedImage = pointerEventData.pointerDrag.GetComponent<Image>();
        gameObject.GetComponent<Image>().sprite = droppedImage.sprite;
        gameObject.GetComponent<Image>().color = Vector4.one * 0.6f;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (pointerEventData.pointerDrag == null) return;
        gameObject.GetComponent<Image>().sprite = nowSprite;
        if (nowSprite == null)
            gameObject.GetComponent<Image>().color = Vector4.zero;
        else
            gameObject.GetComponent<Image>().color = Vector4.one;
    }

    public void OnDrop(PointerEventData pointerEventData)
    {
        Image droppedImage = pointerEventData.pointerDrag.GetComponent<Image>();
        MinionId = pointerEventData.pointerDrag.GetComponent<DragObject>().MinionId;
        gameObject.GetComponent<Image>().sprite = droppedImage.sprite;
        nowSprite = droppedImage.sprite;
        gameObject.GetComponent<Image>().color = Vector4.one;
    }
}
