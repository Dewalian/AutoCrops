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

    private void Start()
    {
        gold = GoldManager.instance.gold;
        goldText.text = "$" + gold;
    }

    private void SetGold(float amount)
    {
        if(changeGoldCoroutine != null){
            StopCoroutine(changeGoldCoroutine);
        }

        gold = GoldManager.instance.gold;
        float newGoldAmount = gold + amount;

        changeGoldCoroutine = StartCoroutine(ChangeGold(newGoldAmount));
    }

    private IEnumerator ChangeGold(float newGoldAmount)
    {
        while(gold != newGoldAmount){
            if(gold < newGoldAmount){
                gold++;
            }
            else{
                gold--;
            }

            goldText.text = "$" + gold;
            yield return new WaitForSeconds(0.02f);
        }
    }
}
