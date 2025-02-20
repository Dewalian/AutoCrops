using System;
using UnityEngine;
using UnityEngine.Pool;

public class Marker : MonoBehaviour
{
    [SerializeField] private Area area;
    // private IObjectPool<Crop> cropPool;
    private Sprite icon;
    private Item selectedItem;
    private Crop selectedCrop;
    private Vector3Int currTile;
    public Action<Sprite> OnSetItem;

    public void ActivateItem(Vector3Int tile)
    {
        currTile = tile;
        if(area.GetSoil(currTile).soilState == SoilState.Ready){
            GameObject cropObj = area.GetSoil(tile).crop.gameObject;
            Destroy(cropObj);
        }
        else if(selectedItem != null){  
            selectedItem.Activate(currTile);
        }
    }

    public void SetCropSpawner(Item item, Crop crop)
    {
        if(item == null){
            selectedItem = null;
            icon = null;
        }
        else if(item != null && item.TryGetComponent(out CropSpawner cropSpawner)){
            selectedItem = item;
            selectedCrop = crop;

            icon = selectedCrop.GetMarkerIcon();
            cropSpawner.SetCrop(selectedCrop);
        }

        OnSetItem?.Invoke(icon);
    }

    public void SetItem(Item item)
    {
        if(item == null){
            selectedItem = null;
            icon = null;
        }
        else{
            selectedItem = item;
            icon = item.GetMarkerIcon();
        }

        OnSetItem?.Invoke(icon);
    }
}
