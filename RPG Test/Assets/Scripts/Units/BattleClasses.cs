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

    public DamageInstance(int hp, int mana)
    {
        hitPoints = hp;
        manaPoints = mana;

    }
}

public class CritInstance
{
    public float CritChance;
    public float CritDamage;
}