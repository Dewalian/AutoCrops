using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

public class PriceEffect : MonoBehaviour
{
    private TMP_Text priceText;
    [SerializeField] private float duration;
    private Vector3 startPos;
    [SerializeField] private float yStartAxisOffset;
    private Vector3 endPos;
    [SerializeField] private float yEndAxisOffset;
    private IObjectPool<PriceEffect> pool;

    private void Awake()
    {
        priceText = GetComponent<TMP_Text>();
    }

    public void Init(int price, Vector3 startPos)
    {
        this.startPos = startPos;

        SetPos();
        priceText.text = "+$" + price.ToString();

        StartCoroutine(Raise());
    }

    public void SetPool(IObjectPool<PriceEffect> pool)
    {
        this.pool = pool;
    }

    private void SetPos()
    {
        endPos = startPos;

        startPos.y += yStartAxisOffset;
        endPos.y += yEndAxisOffset;
    }

    private IEnumerator Raise()
    {
        float timePassed = 0;

        while(timePassed < duration){
            timePassed += Time.deltaTime;
            float durationNorm = timePassed / duration;
            
            transform.position = Vector3.Lerp(startPos, endPos, durationNorm);

            yield return null;
        }

        pool.Release(this);
    }
}
