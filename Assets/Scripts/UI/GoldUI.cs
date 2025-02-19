using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GoldUI : MonoBehaviour
{
    [SerializeField] private TMP_Text goldText;
    private Coroutine changeGoldCoroutine;
    private float gold;

    private void OnEnable()
    {
        GoldManager.instance.OnAddGold += SetGold;
    }

    private void OnDisable()
    {
        GoldManager.instance.OnAddGold -= SetGold;
    }

    private void Start()
    {
        gold = GoldManager.instance.gold;
        goldText.text = "$" + gold;
    }

    private void SetGold()
    {
        if(changeGoldCoroutine != null){
            StopCoroutine(changeGoldCoroutine);
        }

        changeGoldCoroutine = StartCoroutine(ChangeGold());
    }

    private IEnumerator ChangeGold()
    {
        float timePassed = 0;
        float delay = 0.005f;

        while(gold != GoldManager.instance.gold){

            if(gold < GoldManager.instance.gold){
                gold++;
            }
            else{
                gold--;
            }
            yield return new WaitForSeconds(delay);
            timePassed += delay;

            if(timePassed >= 0.5f){
                gold = GoldManager.instance.gold;
                goldText.text = "$" + gold;
                break;
            }

            goldText.text = "$" + gold;
        }
    }
}
