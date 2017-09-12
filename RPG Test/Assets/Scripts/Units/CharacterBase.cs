using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour {
    public enum ArmorTypes { Normal, Unarmored, Light, Heavy, Fortified, Heroic, Divine, Impregnable};
    public enum AttackTypes { Normal, Unarmed, Piercing, Magic, Siege, Hero, Chaos, Pure};

    public int BaseAttackSpeed;
    public int BaseAttackDamage;

    public long BaseHitPoints;
    public long BaseManaPoints;
    public float BaseHitPointRegen;
    public float BaseManaRegen;
    public int BaseStrength;
    public int BaseAgility;
    public int BaseIntelligence;

    public int BaseAttackRating;
    public int BaseDefenseRating;
    public int BaseArmorRating;
    public int BaseDodge;

    public int BaseMagicDodge;
    public int BaseMagicResistance;
    public int BaseMagicAmplification;

    public int BaseMovementSpeed;

    public float BaseCritChance;
    public float BaseCritDamage;

    public ArmorTypes ArmorType;
    public AttackTypes AttackType;

    public List<ItemBase> Inventory;
    public bool InventoryMutated;
    public List<AbilityBase> Abilities;
    public bool AbilitiesMutated;
    public List<EffectBase> ActiveEffects;
    public bool ActiveEffectsMutated;

    public ItemBase HeadItem;
    public ItemBase ChestItem;
    public ItemBase WeaponLeftItem;
    public ItemBase WeaponRightItem;
    public ItemBase GlovesItem;
    public ItemBase WaistItem;
    public ItemBase LegsItem;
    public ItemBase FeetItem;

    public bool isIdling;
    public bool isMoving;
    public bool isAttacking;
    public bool isCasting;
    public bool isStunned;
    public bool isEthereal;
    public bool isMagicImmune;

    private int ComputedAttackSpeed;
    private int ComputedAttackDamage;

    private long ComputedHitPoints;
    private long ComputedManaPoints;
    private float ComputedHitPointRegen;
    private float ComputedManaRegen;
    private int ComputedStrength;
    private int ComputedAgility;
    private int ComputedIntelligence;

    private int ComputedAttackRating;
    private int ComputedDefenseRating;
    private int ComputedArmorRating;
    private int ComputedDodge;

    private int ComputedMagicDodge;
    private int ComputedMagicResistance;
    private int ComputedMagicAmplification;

    private int ComputedMovementSpeed;

    private float ComputedCritChance;
    private float ComputedCritDamage;
	
	public double CurrentHitPoints;
	public double CurrentManaPoints;
    // Use this for initialization
    void Start () {
        Inventory = new List<ItemBase>();
        Abilities = new List<AbilityBase>();
        ActiveEffects = new List<EffectBase>();

        InventoryMutated = false;
        AbilitiesMutated = false;
        ActiveEffectsMutated = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
