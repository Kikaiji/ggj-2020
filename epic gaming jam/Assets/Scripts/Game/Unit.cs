using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    //a battle unit, all the stats are below. animations arent quite working.
    public string unitName;
    public int Defense;
    public int Attack;
    public int Speed;
    public int MaxHP;
    public int CurrentHP;
    public int MaxMP;
    public int CurrentMP;
    public Animator anim;

    void Start()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    //whenever something takes damage
    public bool TakeDamage(int damage)
    {
        CurrentHP -= damage;

        if(CurrentHP <= 0)
        {
            return true;
        }
        return false;
    }

    //takes mp
    public void TakeMP(int cost)
    {
        CurrentMP -= cost;
    }

    //heal function for units
    public bool Heal(int heal, int cost)
    {
        if((CurrentMP -= cost) >= 0)
        {
            CurrentMP -= cost;
            CurrentHP += heal;
            if (CurrentHP > MaxHP) { CurrentHP = MaxHP; }
            return true;
        }
        return false;
    }
}
