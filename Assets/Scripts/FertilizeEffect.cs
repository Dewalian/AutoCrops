using UnityEngine;
using UnityEngine.Pool;

public class FertilizeEffect : MonoBehaviour
{
    private IObjectPool<FertilizeEffect> pool;
    public void SetPool(IObjectPool<FertilizeEffect> pool)
    {
        this.pool = pool;
    }

    private void OnParticleSystemStopped()
    {
        pool.Release(this);
    }
}
