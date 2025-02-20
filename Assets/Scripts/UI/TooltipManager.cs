using TMPro;
using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager instance;
    [SerializeField] private RectTransform toolTip;
    [SerializeField] private TMP_Text headerText;
    [SerializeField] private TMP_Text contentText;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        MoveTooltip();
    }

    private void MoveTooltip()
    {
        Vector2 mousePos = Input.mousePosition;

        float pivotX = mousePos.x / Screen.width;
        float pivotY = mousePos.y / Screen.height;
        
        float finalPivotX;
        float finalPivotY;
            
        if(pivotX < 0.5f){ //If mouse on left of screen move tooltip to right of cursor and vice versa
            finalPivotX = -0.1f;
        }
        else{
            finalPivotX = 1.01f;
        }

        if(pivotY < 0.5f){ //If mouse on lower half of screen move tooltip above cursor and vice versa 
            finalPivotY = 0;
        }
        else{
            finalPivotY = 1;
        }

        toolTip.pivot = new Vector2(finalPivotX, finalPivotY);
        Vector3 screenMousePos = Camera.main.ScreenToWorldPoint(mousePos);

        toolTip.transform.position = new Vector3(screenMousePos.x, screenMousePos.y, 0);
    }

    public void Show(string header, string content)
    {
        headerText.text = header;
        contentText.text = content;

        toolTip.gameObject.SetActive(true);
    }

    public void Hide()
    {
        toolTip.gameObject.SetActive(false);
    }
}
