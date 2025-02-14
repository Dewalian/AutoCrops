using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Area : MonoBehaviour
{
    
    [SerializeField] private TileBase tile;
    private Dirt[,] grid;
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
        grid = new Dirt[width, height];

        for(int x=0; x<width; x++){
            for(int y=0; y<height; y++){
                tilemap.SetTile(new Vector3Int(x, y, 0), tile);
                grid[x, y] = new Dirt(DirtState.Empty, null);
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

    public Dirt GetDirt(Vector3Int tile)
    {
        return grid[tile.x, tile.y];
    }

    public void SetDirt(Vector3Int tile, DirtState dirtState, GameObject crop)
    {
        grid[tile.x, tile.y].dirtState = dirtState;
        grid[tile.x, tile.y].crop = crop;
    }
}
