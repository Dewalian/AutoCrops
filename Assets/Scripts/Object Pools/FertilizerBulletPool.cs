using UnityEngine;
using UnityEngine.Pool;

public class FertilizerBulletPool : MonoBehaviour
{
    public static FertilizerBulletPool instance = null;
    [SerializeField] private Area area;
    [SerializeField] private FertilizerBullet fertilizerBulletPrefab;
    private Vector3Int startTile;
    private Vector3Int endTile;
    public IObjectPool<FertilizerBullet> pool;

    private void Awake()
    {
        instance = this;
        pool = new ObjectPool<FertilizerBullet>(CreateBullet, OnGet, OnRelease);
    }

    public void Spawn(Vector3Int startPos, Vector3Int endPos)
    {
        this.startTile = startPos;
        this.endTile = endPos;

        pool.Get();
    }

    private FertilizerBullet CreateBullet()
    {
        FertilizerBullet fertilizerBullet = Instantiate(fertilizerBulletPrefab, transform);
        fertilizerBullet.init(area, pool, startTile, endTile);
        
        return fertilizerBullet;
    }

    private void OnGet(FertilizerBullet fertilizerBullet)
    {
        fertilizerBullet.gameObject.SetActive(true);
        fertilizerBullet.init(area, pool, startTile, endTile);
    }

    private void OnRelease(FertilizerBullet fertilizerBullet){
        fertilizerBullet.gameObject.SetActive(false);
    }
}
