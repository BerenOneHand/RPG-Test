using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitStats {

	public int AttackSpeed = 0;
	public int AttackDamage = 0;
    public int MissChance = 0;

	public int HitPoints = 0;
	public int ManaPoints = 0;
	public float HitPointRegen = 0;
	public float ManaRegen = 0;
	public int Strength = 0;
	public int Agility = 0;
	public int Intelligence = 0;
	public int ArmorRating = 0;
	//public int Dodge = 0;

	public int MagicDodge = 0;
	public int MagicResistance = 0;
	public int MagicAmplification = 0;

	public int MovementSpeed = 0;

    public List<int> DodgeInstances;
    public List<CritInstance> CritInstances;
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
            MagicDodge = u1.MagicDodge + u2.MagicDodge,
            MagicResistance = u1.MagicResistance + u2.MagicResistance,
            MagicAmplification = u1.MagicAmplification + u2.MagicAmplification,
            MovementSpeed = u1.MovementSpeed + u2.MovementSpeed,
            //CritChance = u1.CritChance + u2.CritChance,
            //CritDamage = u1.CritDamage + u2.CritDamage
            DodgeInstances = new List<int>(u1.DodgeInstances.Union(u2.DodgeInstances)),
            CritInstances = new List<CritInstance>(u1.CritInstances.Union(u2.CritInstances))
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
            MagicDodge = u1.MagicDodge + u2,
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
            MagicDodge = u1.MagicDodge * u2.MagicDodge,
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
            MagicDodge = u1.MagicDodge / u2,
            MagicResistance = u1.MagicResistance / u2,
            MagicAmplification = u1.MagicAmplification / u2,
            MovementSpeed = u1.MovementSpeed / u2,
            //CritChance = u1.CritChance / u2,
            //CritDamage = u1.CritDamage / u2

        };
    }
}
