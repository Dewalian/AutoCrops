using UnityEngine;
using UnityEngine.Pool;

public class FertilizeEffectPool : MonoBehaviour
{
    public static FertilizeEffectPool instance = null;
    [SerializeField] private FertilizeEffect fertilizeEffectPrefab;
    private Vector3 startPos;
    private IObjectPool<FertilizeEffect> pool;
    
    private void Awake()
    {
        instance = this;
        pool = new ObjectPool<FertilizeEffect>(CreateParticle, OnGet, OnRelease);
    }

    public void Spawn(Vector3 startPos)
    {
        this.startPos = startPos;

        pool.Get();
    }

    private FertilizeEffect CreateParticle()
    {
        FertilizeEffect fertilizeEffect = Instantiate(fertilizeEffectPrefab, transform);
        fertilizeEffect.transform.position = startPos;
        fertilizeEffect.SetPool(pool);
        
        return fertilizeEffect;
    }

    private void OnGet(FertilizeEffect fertilizeEffect)
    {
        fertilizeEffect.gameObject.SetActive(true);
        fertilizeEffect.transform.position = startPos;
    }

    private void OnRelease(FertilizeEffect fertilizeEffect){
        fertilizeEffect.gameObject.SetActive(false);
    }
}
