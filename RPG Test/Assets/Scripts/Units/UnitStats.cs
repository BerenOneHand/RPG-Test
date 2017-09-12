using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour {

	public int AttackSpeed;
	public int AttackDamage;

	public long HitPoints;
	public long ManaPoints;
	public float HitPointRegen;
	public float ManaRegen;
	public int Strength;
	public int Agility;
	public int Intelligence;
	public int ArmorRating;
	public int Dodge;

	public int MagicDodge;
	public int MagicResistance;
	public int MagicAmplification;

	public int MovementSpeed;

	public float CritChance;
	public float CritDamage;

    public bool isPercentage;// Only to be used by stats mutation
}
