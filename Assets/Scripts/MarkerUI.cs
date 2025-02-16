using UnityEngine;
using UnityEngine.InputSystem;

public abstract class MarkerUI : MonoBehaviour
{
    [SerializeField] protected Area area;
    [SerializeField] protected Marker marker;
    protected SpriteRenderer spriteRenderer;
    protected Sprite currIcon;
    [SerializeField] protected Sprite openHand;
    [SerializeField] protected Sprite neutral;
    protected PlayerInput playerInput;
    protected InputAction keyboardMovement;
    protected InputAction cursorMovement;

    protected virtual void Awake()
    {   
        spriteRenderer = GetComponent<SpriteRenderer>();
        Cursor.visible = false;

        marker.OnSetItem += ChangeIcon;

        playerInput = new PlayerInput();
        keyboardMovement = playerInput.Marker.MoveKeyboard;
        cursorMovement = playerInput.Marker.MoveCursor;

        keyboardMovement.Enable();
        cursorMovement.Enable();
    }

    private void OnEnable()
    {
        playerInput.Marker.Select.performed += SelectTile;
        playerInput.Marker.Select.Enable();
    }

    private void OnDisable()
    {
        playerInput.Marker.Select.Disable();
    }

    protected virtual void Start()
    {
        currIcon = neutral;
    }

    protected virtual void Update()
    {
        DetectTile();
    }

    protected abstract void DetectTile();
    protected abstract void SelectTile(InputAction.CallbackContext context);

    private void ChangeIcon(Sprite icon)
    {
        if(icon == null){
            spriteRenderer.sprite = neutral;
        }
        else{
            spriteRenderer.sprite = icon;
        }
        
        currIcon = spriteRenderer.sprite;
    }
}
