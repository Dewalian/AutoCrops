using UnityEngine;
using UnityEngine.InputSystem;

public class CursorMarkerUI : MarkerUI
{
    [SerializeField] private GameObject keyboardMarker;
    private Vector3 mousePos;
    private bool isInBound;

    protected override void Awake()
    {
        base.Awake();
        keyboardMovement.performed += EnableKeyboardMarker;
    }

    protected override void Update()
    {
        base.Update();
        FollowMouse();
    }

    protected override void DetectTile()
    {
        mousePos = Camera.main.ScreenToWorldPoint(cursorMovement.ReadValue<Vector2>());
        Vector3Int tile = area.GetTile(mousePos);
        isInBound = area.CheckBound(tile);

        if(isInBound){
            if(area.GetSoil(tile).soilState == SoilState.Ready){
                spriteRenderer.sprite = openHand;
            }
            else{
                spriteRenderer.sprite = currIcon;
            }
        }
    }

    private void FollowMouse()
    {
        mousePos = Camera.main.ScreenToWorldPoint(cursorMovement.ReadValue<Vector2>());
        transform.position = new Vector3(mousePos.x, mousePos.y, 0);
    }

    private void EnableKeyboardMarker(InputAction.CallbackContext input)
    {
        keyboardMarker.SetActive(true);
        gameObject.SetActive(false);
    }

    protected override void SelectTile(InputAction.CallbackContext context)
    {
        if(isInBound){
            Vector3Int tile = area.GetTile(mousePos);
            marker.ActivateItem(tile);
        }
    }
}
