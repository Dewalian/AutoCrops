using UnityEngine;
using UnityEngine.UI;

public class CropUI : MonoBehaviour
{
    [SerializeField] private Crop crop;
    private CropSO stats;
    [SerializeField] private Slider timerSlider;

    private void OnEnable()
    {
        stats = crop.GetStats();
        timerSlider.gameObject.SetActive(true);
        crop.OnTimePassed += UpdateSlider;
        crop.OnReady += HideSlider;
    }

    private void OnDisable()
    {
        crop.OnTimePassed -= UpdateSlider;
        crop.OnReady -= HideSlider;
    }

    private void UpdateSlider(float seconds)
    {
        timerSlider.value = seconds / stats.time;
    }

    private void HideSlider()
    {
        timerSlider.gameObject.SetActive(false);
    }
}
