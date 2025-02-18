using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardMarkerUI : MarkerUI
{
    [SerializeField] private GameObject cursorMarker;
    private Vector3Int currTile;

    protected override void Awake()
    {
        base.Awake();
        keyboardMovement.performed += Movement;        
    }

    protected override void Start()
    {
        base.Start();
        Init();
    }

    protected override void Update()
    {
        base.Update();
        EnableCursorMarker();
    }

    private void Init()
    {
        Vector3Int pos = new Vector3Int(area.width / 2, area.height / 2, 0);
        Vector3 posCenter = area.GetTileCenter(pos);
        transform.position = posCenter;
        currTile = area.GetTile(transform.position);
    }

    protected override void DetectTile()
    {
        if(area.GetSoil(currTile).soilState == SoilState.Ready){
            spriteRenderer.sprite = openHand;
        }
        else{
            spriteRenderer.sprite = currIcon;
        }
    }

    private void EnableCursorMarker()
    {
        if(Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0){
            cursorMarker.SetActive(true);
            gameObject.SetActive(false);
        }
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

    protected override void SelectTile(InputAction.CallbackContext input)
    {
        marker.ActivateItem(currTile);
    }
}
