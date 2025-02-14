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

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        timerThird = stats.time / 3;
        timerThirdCopy = timerThird;
        phase = 0;
        spriteRenderer.sprite = stats.icons[phase];
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
            spriteRenderer.sprite = stats.readyIcon;
            area.SetDirt(tile, DirtState.Ready, gameObject);
            return;
        }
    }

    public Sprite GetSeedIcon()
    {
        return stats.seedIcon;
    }
}
