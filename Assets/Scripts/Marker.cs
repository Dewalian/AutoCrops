using System;
using UnityEngine;
using UnityEngine.Pool;

public class Marker : MonoBehaviour
{
    [SerializeField] private Area area;
    private IObjectPool<Crop> cropPool;
    private Sprite icon;
    private Item selectedItem;
    private Crop selectedCrop;
    private Vector3Int currTile;
    public Action<Sprite> OnSetItem;

    private void Awake()
    {
        cropPool = new ObjectPool<Crop>(CreateCrop, OnGetCrop, OnReleaseCrop);
    }

    private Crop CreateCrop()
    {
        GameObject cropObj = Instantiate(selectedCrop.gameObject, area.GetTileCenter(currTile), Quaternion.identity);
        Crop crop = cropObj.GetComponent<Crop>();

        crop.Init(area, currTile);
        area.SetDirt(currTile, SoilState.Occupied, crop);

        return crop;
    }

    private void OnGetCrop(Crop crop)
    {
        crop.gameObject.SetActive(true);
        crop.transform.position = area.GetTileCenter(currTile);

        crop.Init(area, currTile);
        area.SetDirt(currTile, SoilState.Occupied, crop);
    }

    private void OnReleaseCrop(Crop crop)
    {
        crop.gameObject.SetActive(false);
        area.SetDirt(currTile, SoilState.Empty, null);
    }

    public void ActivateItem(Vector3Int tile)
    {
        currTile = tile;
        if(area.GetSoil(currTile).soilState == SoilState.Ready){
            Crop crop = area.GetSoil(currTile).crop;
            cropPool.Release(crop);
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
            cropSpawner.SetCrop(selectedCrop, cropPool);
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
