using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Grass : MonoBehaviour
{
    [SerializeField] private Area area;
    [SerializeField] private TileBase grassTile;
    private Tilemap tilemap;

    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        for(int x=-1; x<area.width+1; x++){
            for(int y=-1; y<area.height+1; y++){
                tilemap.SetTile(new Vector3Int(x, y, 0), grassTile);
            }
        }
    }
}
