using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class Area : MonoBehaviour
{
    [SerializeField] private TileBase soilTile;
    [SerializeField] private TileBase dirtTile;
    private Soil[,] grid;
    private Tilemap tilemap;
    public int width;
    public int height;

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
        grid = new Soil[width, height];

        for(int x=-1; x<width+1; x++){
            for(int y=-1; y<height+1; y++){

                if(x >= 0 && x < width && y >= 0 && y < height){
                    grid[x, y] = new Soil(SoilState.Empty, null);
                    tilemap.SetTile(new Vector3Int(x, y, 0), soilTile);
                }
                else{
                    tilemap.SetTile(new Vector3Int(x, y, 0), dirtTile);
                }

            }
        }

        Camera.main.transform.position = transform.position + new Vector3((float)width / 2, (float)height / 2, -10);
    }

    public Vector3Int GetTile(Vector3 pos)
    {
        return tilemap.WorldToCell(pos);
    }

    public Vector3 GetTileCenter(Vector3Int tile)
    {
        return tilemap.GetCellCenterWorld(tile);
    }

    public bool CheckBound(Vector3Int tile)
    {
        if(tile.x < 0 || tile.x > width-1){
            return false;
        }

        if(tile.y < 0 || tile.y > height-1){
            return false;
        }

        return true;
    }

    public Soil GetSoil(Vector3Int tile)
    {
        return grid[tile.x, tile.y];
    }

    public void SetSoil(Vector3Int tile, SoilState soilState, Crop crop)
    {
        grid[tile.x, tile.y].soilState = soilState;
        grid[tile.x, tile.y].crop = crop;
    }
}
