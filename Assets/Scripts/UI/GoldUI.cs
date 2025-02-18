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

    private void SetGold()
    {
        if(changeGoldCoroutine != null){
            StopCoroutine(changeGoldCoroutine);
        }

        changeGoldCoroutine = StartCoroutine(ChangeGold());
    }

    private IEnumerator ChangeGold()
    {
        while(gold != GoldManager.instance.gold){
            if(gold < GoldManager.instance.gold){
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
