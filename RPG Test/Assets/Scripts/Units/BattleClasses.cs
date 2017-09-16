using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageInstance
{
    public int hitPoints;
    public int manaPoints;
    public bool isAttack;
    public bool isAbility;
    public DamageTypes damageType;
    public bool trueStrike;

    //public DamageInstance(int hp, int mana, bool isAttack, bool isAbility, DamageTypes damageType, bool trueStrike)
    //{
    //    hitPoints = hp;
    //    manaPoints = mana;

    //}

    public static DamageInstance operator * (DamageInstance di1, float di2)
    {
        di1.hitPoints = (int)(di1.hitPoints * di2);
        di1.manaPoints = (int)(di1.manaPoints * di2);
        return di1;
    }
}

public class CritInstance
{
    public float CritChance;
    public float CritDamage;
}