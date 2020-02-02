using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    public Text nameText;
    public Slider hpSlider;
    public Slider mpSlider;

    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitName;
        hpSlider.maxValue = unit.MaxHP;
        hpSlider.value = unit.CurrentHP;
        if(mpSlider != null)
        {
            mpSlider.maxValue = unit.MaxMP;
            mpSlider.value = unit.CurrentMP;
        }
    }

    public void SetHp(int hp)
    {
        hpSlider.value = hp;
    }
    public void SetMp(int mp)
    {
        if(mpSlider != null)
        {
            mpSlider.value = mp;
        }
    }
}
