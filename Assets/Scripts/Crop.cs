using System;
using UnityEngine;

public class Crop : MonoBehaviour
{
    [SerializeField] private CropSO stats;
    private Area area;
    private SpriteRenderer spriteRenderer;
    private Vector3Int tile;
    private float quality;
    private float secondsPassed;
    private int phase;
    private float timerThird;
    private float timerThirdCopy;
    private bool isReady;
    public Action<float> OnTimePassed;
    public Action OnReady;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnDisable()
    {
        quality -= 50;
        int price = stats.price + Mathf.FloorToInt(stats.price * quality / 20);
        GoldManager.instance.AddGold(price);

        area.SetSoil(tile, SoilState.Empty, null);
    }

    private void Start()
    {
        area.SetSoil(tile, SoilState.Occupied, this);

        quality = 50;

        timerThird = stats.time / 3;
        timerThirdCopy = timerThird;

        spriteRenderer.sprite = stats.icons[phase];

        GoldManager.instance.AddGold(-stats.cost);
    }

    public void Init(Area area, Vector3Int tile)
    {
        this.area = area;
        this.tile = tile;
    }

    private void Update()
    {
        Timer();
    }

    private void Timer()
    {
        if(isReady){
            return;
        }

        if(secondsPassed >= stats.time){
            OnReady?.Invoke();
            isReady = true;

            spriteRenderer.sprite = stats.readyIcon;
            area.SetSoil(tile, SoilState.Ready, this);

            return;
        }

        if(secondsPassed > timerThird){
            timerThird += timerThirdCopy;
            phase++;
            spriteRenderer.sprite = stats.icons[phase];
        }

        secondsPassed += Time.deltaTime;

        OnTimePassed?.Invoke(secondsPassed);
    }

    public void Fertilize(float timerBoost, float qualityBoost)
    {
        secondsPassed += timerBoost;
        quality += qualityBoost;
    }

    public Sprite GetMarkerIcon()
    {
        return stats.seedIcon;
    }

    public Sprite GetInventoryIcon()
    {
        return stats.readyIcon;
    }

    public CropSO GetStats()
    {
        return stats;
    }
}
