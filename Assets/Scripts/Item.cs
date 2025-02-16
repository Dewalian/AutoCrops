using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] protected Sprite inventoryIcon;
    [SerializeField] protected Sprite markerIcon;
    [SerializeField] protected Area area;
    protected Vector3Int tile;

    public virtual void Activate(Vector3Int tile)
    {
        this.tile = tile;
    }
    public Sprite GetInventoryIcon()
    {
        return inventoryIcon;
    }

    public Sprite GetMarkerIcon()
    {
        return markerIcon;
    }
}
