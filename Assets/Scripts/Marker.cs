using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Marker : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Vector3Int currTile;
    private Crop selectedCrop;
    private Area area;
    private PlayerInput playerInput;
    private InputAction movement;
    public UnityEvent OnClick;
    public Action OnPress;

    private void Awake()
    {
        playerInput = new PlayerInput();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        movement = playerInput.Marker.Move;
        movement.performed += Movement;
        movement.Enable();

        playerInput.Marker.Select.performed += SelectTile;
        playerInput.Marker.Select.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
        playerInput.Marker.Select.Disable();
    }

    private void Start()
    {
        area = GetComponentInParent<Area>();
        Init();
    }

    private void Movement(InputAction.CallbackContext input)
    {
        Vector3Int nextTile = area.GetTile(transform.position + (Vector3)input.ReadValue<Vector2>());
        bool isInBound = area.CheckBound(nextTile);

        if(isInBound){
            transform.position += (Vector3)input.ReadValue<Vector2>();
            currTile = area.GetTile(transform.position);
        }
    }

    private void SelectTile(InputAction.CallbackContext input)
    {
        DirtState dirtState = area.GetDirt(currTile).dirtState;
        if(selectedCrop == null || dirtState == DirtState.Occupied){
            Debug.Log("occupied");
        }
        else if(dirtState == DirtState.Ready)
        {
            GameObject crop = area.GetDirt(currTile).crop;
            Destroy(crop);
        }
        else{
            GameObject cropObj = Instantiate(selectedCrop.gameObject, transform.position, Quaternion.identity);
            cropObj.GetComponent<Crop>().Init(area, currTile);
            area.SetDirt(currTile, DirtState.Occupied, cropObj);
        }
    }

    private void Init()
    {
        Vector3Int pos = new Vector3Int(area.width / 2, area.height / 2, 0);
        Vector3 posCenter = area.GetTileCenter(pos);
        transform.position = posCenter;
        currTile = area.GetTile(transform.position);
    }

    public void SetCrop(Crop crop)
    {
        if(crop == null){
            selectedCrop = null;
            spriteRenderer.sprite = null;

            return;
        }
        
        selectedCrop = crop;
        spriteRenderer.sprite = crop.GetSeedIcon();
    }
}
