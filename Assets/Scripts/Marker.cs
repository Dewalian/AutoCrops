using System;
using UnityEngine;
using UnityEngine.Pool;

public class Marker : MonoBehaviour
{
    [SerializeField] private Area area;
    private Sprite icon;
    private Crop selectedCrop;
    private Vector3Int currTile;
    private IObjectPool<Crop> cropPool;
    public Action<Sprite> OnSetCrop;

    private void Awake()
    {
        cropPool = new ObjectPool<Crop>(CreateCrop, OnGetCrop, OnReleaseCrop, maxSize: 25);
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

    public void PlantCrop(Vector3Int tile)
    {
        currTile = tile;
        SoilState dirtState = area.GetDirt(currTile).dirtState;
        
        if(dirtState == SoilState.Ready)
        {
            Crop crop = area.GetDirt(currTile).crop;
            cropPool.Release(crop);
        }
        else if(selectedCrop == null)
        {
            Debug.Log("no crop selected");
        }
        else if(dirtState == SoilState.Occupied){
            Debug.Log("occupied");
        }
        else{
            cropPool.Get();
        }
    }

    public void SetCrop(Crop crop)
    {
        if(crop == null){
            selectedCrop = null;
            icon = null;
        }
        else{
            selectedCrop = crop;
            icon = crop.GetSeedIcon();
        }

        OnSetCrop?.Invoke(icon);
    }
}
