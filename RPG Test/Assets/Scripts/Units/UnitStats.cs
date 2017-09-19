using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitStats {
    public float AttackTime = 0;
	public float AttackSpeed = 0;
	public float AttackDamage = 0;
    public float AttackRange = 0;
    public float MissChance = 0;

	public float HitPoints = 0;
	public float ManaPoints = 0;
	public float HitPointRegen = 0;
	public float ManaRegen = 0;
	public float Strength = 0;
	public float Agility = 0;
	public float Intelligence = 0;
	public float ArmorRating = 0;
	//public int Dodge = 0;

	//public int MagicDodge = 0;
	public float MagicResistance = 0;
	public float MagicAmplification = 0;
           
	public float MovementSpeed = 0;

    public List<int> DodgeInstances;
    public List<int> MagicDodgeInstances;
    public List<CritInstance> CritInstances;
    public List<CritInstance> MagicCritInstances;
    //public float CritChance = 0;
    //public float CritDamage = 0;

    public bool isPercentage = false;// Only to be used by stats mutation

    public static UnitStats operator +(UnitStats u1, UnitStats u2)
    {
        return new UnitStats()
        {
            AttackSpeed = u1.AttackSpeed + u2.AttackSpeed,
            AttackDamage = u1.AttackDamage + u2.AttackDamage,
            MissChance = u1.MissChance + u2.MissChance,
            HitPoints = u1.HitPoints + u2.HitPoints,
            ManaPoints = u1.HitPoints + u2.HitPoints,
            HitPointRegen = u1.HitPointRegen + u2.HitPointRegen,
            ManaRegen = u1.ManaRegen + u2.ManaRegen,
            Strength = u1.Strength + u2.Strength,
            Agility = u1.Agility + u2.Agility,
            Intelligence = u1.Intelligence + u2.Intelligence,
            ArmorRating = u1.ArmorRating + u2.ArmorRating,
            //Dodge = u1.Dodge + u2.Dodge,
            //MagicDodge = u1.MagicDodge + u2.MagicDodge,
            MagicResistance = u1.MagicResistance + u2.MagicResistance,
            MagicAmplification = u1.MagicAmplification + u2.MagicAmplification,
            MovementSpeed = u1.MovementSpeed + u2.MovementSpeed,
            //CritChance = u1.CritChance + u2.CritChance,
            //CritDamage = u1.CritDamage + u2.CritDamage
            DodgeInstances = new List<int>(u1.DodgeInstances.Union(u2.DodgeInstances)),
            MagicDodgeInstances = new List<int>(u1.MagicDodgeInstances.Union(u2.MagicDodgeInstances)),
            CritInstances = new List<CritInstance>(u1.CritInstances.Union(u2.CritInstances)),
            MagicCritInstances = new List<CritInstance>(u1.MagicCritInstances.Union(u2.MagicCritInstances))
        };
    }

    public static UnitStats operator +(UnitStats u1, int u2)
    {
        return new UnitStats()
        {
            AttackSpeed = u1.AttackSpeed + u2,
            AttackDamage = u1.AttackDamage + u2,
            MissChance = u1.MissChance + u2,
            HitPoints = u1.HitPoints + u2,
            ManaPoints = u1.HitPoints + u2,
            HitPointRegen = u1.HitPointRegen + u2,
            ManaRegen = u1.ManaRegen + u2,
            Strength = u1.Strength + u2,
            Agility = u1.Agility + u2,
            Intelligence = u1.Intelligence + u2,
            ArmorRating = u1.ArmorRating + u2,
            //Dodge = u1.Dodge + u2,
            //MagicDodge = u1.MagicDodge + u2,
            MagicResistance = u1.MagicResistance + u2,
            MagicAmplification = u1.MagicAmplification + u2,
            MovementSpeed = u1.MovementSpeed + u2,
            //CritChance = u1.CritChance + u2,
            //CritDamage = u1.CritDamage + u2

        };
    }

    public static UnitStats operator *(UnitStats u1, UnitStats u2)
    {
        return new UnitStats()
        {
            AttackSpeed = u1.AttackSpeed * u2.AttackSpeed,
            AttackDamage = u1.AttackDamage * u2.AttackDamage,
            MissChance = u1.MissChance * u2.MissChance,
            HitPoints = u1.HitPoints * u2.HitPoints,
            ManaPoints = u1.HitPoints * u2.HitPoints,
            HitPointRegen = u1.HitPointRegen * u2.HitPointRegen,
            ManaRegen = u1.ManaRegen * u2.ManaRegen,
            Strength = u1.Strength * u2.Strength,
            Agility = u1.Agility * u2.Agility,
            Intelligence = u1.Intelligence * u2.Intelligence,
            ArmorRating = u1.ArmorRating * u2.ArmorRating,
            //Dodge = u1.Dodge * u2.Dodge,
            //MagicDodge = u1.MagicDodge * u2.MagicDodge,
            MagicResistance = u1.MagicResistance * u2.MagicResistance,
            MagicAmplification = u1.MagicAmplification * u2.MagicAmplification,
            MovementSpeed = u1.MovementSpeed * u2.MovementSpeed,
            //CritChance = u1.CritChance * u2.CritChance,
            //CritDamage = u1.CritDamage * u2.CritDamage

        };
    }

    public static UnitStats operator /(UnitStats u1, int u2)
    {
        return new UnitStats()
        {
            AttackSpeed = u1.AttackSpeed / u2,
            AttackDamage = u1.AttackDamage / u2,
            MissChance = u1.MissChance / u2,
            HitPoints = u1.HitPoints / u2,
            ManaPoints = u1.HitPoints / u2,
            HitPointRegen = u1.HitPointRegen / u2,
            ManaRegen = u1.ManaRegen / u2,
            Strength = u1.Strength / u2,
            Agility = u1.Agility / u2,
            Intelligence = u1.Intelligence / u2,
            ArmorRating = u1.ArmorRating / u2,
            //Dodge = u1.Dodge / u2,
            //MagicDodge = u1.MagicDodge / u2,
            MagicResistance = u1.MagicResistance / u2,
            MagicAmplification = u1.MagicAmplification / u2,
            MovementSpeed = u1.MovementSpeed / u2,
            //CritChance = u1.CritChance / u2,
            //CritDamage = u1.CritDamage / u2

        };
    }
}
