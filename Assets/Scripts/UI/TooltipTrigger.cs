using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string header;
    [TextArea][SerializeField] private string content;
    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipManager.instance.Show(header, content);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipManager.instance.Hide();
    }
}
