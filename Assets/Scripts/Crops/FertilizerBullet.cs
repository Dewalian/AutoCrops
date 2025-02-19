using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class FertilizerBullet : MonoBehaviour
{
    [SerializeField] private FertilizerSO stats;
    [SerializeField] private AnimationCurve trajectoryCurve;
    [SerializeField] private float maxHeight;
    [SerializeField] private float duration;
    private Area area;
    private IObjectPool<FertilizerBullet> pool;
    private Vector3Int startTile;
    private Vector3Int endTile;

    public void init(Area area, IObjectPool<FertilizerBullet> pool, Vector3Int startTile, Vector3Int endTile)
    {
        this.area = area;
        this.pool = pool;
        this.startTile = startTile;
        this.endTile = endTile;

        StartCoroutine(Lob());
    }

    private IEnumerator Lob(){
        float timePassed = 0;
        Vector3 startTileCenter = area.GetTileCenter(startTile);
        Vector3 endTileCenter = area.GetTileCenter(endTile);

        while(timePassed < duration)
        {
            timePassed += Time.deltaTime;
            
            float durationNorm = timePassed / duration;
            float height = Mathf.Lerp(0, maxHeight, trajectoryCurve.Evaluate(durationNorm));

            Vector3 bulletPos = Vector3.Lerp(startTileCenter, endTileCenter, durationNorm) + new Vector3(0, height);

            transform.up = (bulletPos - transform.position).normalized;

            transform.position = bulletPos;

            yield return null;
        }

        Soil soil = area.GetSoil(endTile);
        if(soil.crop != null && soil.soilState == SoilState.Occupied){
            soil.crop.Fertilize(stats.qualityBoost, stats.timeBoost);
        }

        pool.Release(this);
    }
}
