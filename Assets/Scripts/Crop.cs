using System;
using UnityEngine;

public class Crop : MonoBehaviour
{
    [SerializeField] private CropStatsSO stats;
    private Area area;
    private Vector3Int tile;
    private float secondsPassed;
    private SpriteRenderer spriteRenderer;
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

    void OnEnable()
    {
        isReady = false;

        timerThird = stats.time / 3;
        timerThirdCopy = timerThird;

        phase = 0;
        spriteRenderer.sprite = stats.icons[phase];
        secondsPassed = 0;
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
        if(secondsPassed >= stats.time){
            return;
        }

        if(secondsPassed > timerThird){
            timerThird += timerThirdCopy;
            phase++;
            spriteRenderer.sprite = stats.icons[phase];
        }

        secondsPassed += Time.deltaTime;

        if(secondsPassed >= stats.time){
            OnReady?.Invoke();
            isReady = true;

            spriteRenderer.sprite = stats.readyIcon;
            area.SetDirt(tile, SoilState.Ready, this);

            return;
        }

        OnTimePassed?.Invoke(secondsPassed);
    }

    public Sprite GetSeedIcon()
    {
        return stats.seedIcon;
    }

    public Sprite GetReadyIcon()
    {
        return stats.readyIcon;
    }

    public CropStatsSO GetStats()
    {
        return stats;
    }

    public bool IsReady()
    {
        return isReady;
    }
}
