using UnityEngine;

public class Crop : MonoBehaviour
{
    [SerializeField] private CropStatsSO stats;
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

    private void Update()
    {
        Timer();
    }

    private void Timer()
    {
        if(secondsPassed >= stats.time){
            spriteRenderer.sprite = stats.readyIcon;
            return;
        }

        if(secondsPassed > timerThird){
            timerThird += timerThirdCopy;
            phase++;
            spriteRenderer.sprite = stats.icons[phase];
        }

        secondsPassed += Time.deltaTime;
    }

    public Sprite GetSeedIcon()
    {
        return stats.seedIcon;
    }
}
