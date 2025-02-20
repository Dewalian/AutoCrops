using UnityEngine;
using UnityEngine.Pool;

public class PriceEffectPool : MonoBehaviour
{
    public static PriceEffectPool instance = null;
    [SerializeField] private PriceEffect priceEffectPrefab;
    private int price;
    private Vector3 startPos;
    public IObjectPool<PriceEffect> pool;

    private void Awake()
    {
        instance = this;
        pool = new ObjectPool<PriceEffect>(CreatePriceEffect, OnGet, OnRelease);
    }

    public void Spawn(int price, Vector3 startPos)
    {
        this.price = price;
        this.startPos = startPos;

        pool.Get();
    }

    private PriceEffect CreatePriceEffect()
    {
        PriceEffect priceEffect = Instantiate(priceEffectPrefab, transform);
        priceEffect.SetPool(pool);
        priceEffect.Init(price, startPos);

        return priceEffect;
    }

    private void OnGet(PriceEffect priceEffect)
    {
        priceEffect.gameObject.SetActive(true);
        priceEffect.Init(price, startPos);
    }

    private void OnRelease(PriceEffect priceEffect)
    {
        priceEffect.gameObject.SetActive(false);
    }
}
