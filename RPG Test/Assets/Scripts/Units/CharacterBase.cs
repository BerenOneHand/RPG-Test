using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterBase : MonoBehaviour
{
    public GameObject owner;
    public GameObject target;

    public ArmorTypes armorType;
    public DamageTypes attackType;
    public Attribute primaryAttribute;

    public List<ItemBase> inventory;
    //public bool inventoryMutated;
    public List<IAbility> abilities;
    public List<IEffect> attackEffects;
    //public bool abilitiesMutated;
    public List<IEffect> activeEffects;
    public List<DamageInstance> queuedDamage;

    public ItemBase headItem;
    public ItemBase chestItem;
    public ItemBase weaponLeftItem;
    public ItemBase weaponRightItem;
    public ItemBase glovesItem;
    public ItemBase waistItem;
    public ItemBase legsItem;
    public ItemBase feetItem;

    public bool isIdling;
    public bool isMoving;
    public bool isAttacking;
    public bool isCasting;
    public bool isStunned;
    public bool isEthereal;
    public bool isMagicImmune;
    public bool isInvulnerable;

    public UnitStats baseStats;
    private UnitStats _computedStats;
    private UnitStats _statsMutation;
    private UnitStats _statsPercentageMutation;

    public UnitStats statsGrowth;
    public int abilityPoints;
    public Int64 currentExperience;
    public Int64 neededExperience;
    public Int16 level;

    public UnitStats ComputedStats { get { return _computedStats; } }

    [SerializeField] double currentHitPoints;
    [SerializeField] double currentManaPoints;

    void Start()
    {
        inventory = new List<ItemBase>();
        abilities = new List<IAbility>();
        activeEffects = new List<IEffect>();
        queuedDamage = new List<DamageInstance>();

        _statsMutation = new UnitStats();
        _computedStats = new UnitStats();
    }


    void Update()
    {
        //Remove Expired Effects
        int effectsRemoved = activeEffects.RemoveAll(x => x.GetTimeLeft() <= 0);

        //Do all effects which are due to run
        foreach (var effect in activeEffects)
        {
            effect.DoEffect(this);
        }
        //Apply all queued damage instances
        foreach (var damage in queuedDamage)
        {
            TakeDamage(damage);
        }
        queuedDamage = new List<DamageInstance>();

        //Calculate new stats after removing effects
        if (currentHitPoints <= 0) Die();
        else if (currentExperience > neededExperience) LevelUp();
        else if (effectsRemoved > 0)
        {
            _statsMutation = new UnitStats();
            _computedStats = new UnitStats();
            foreach (var effect in activeEffects)
            {
                effect.DoEffect(this);
            }
        }
    }

    private void LateUpdate()
    {
        if (currentHitPoints <= 0)
        {
            Die();
        }
    }

    public void AttackTarget()
    {

    }

    public float TakeDamage(DamageInstance di)
    {
        float actualDamageDealt = 0;
        if (!isInvulnerable)
        {
            if (di.isAbility)
            {
                if (!di.trueStrike)
                {
                    foreach (int dodge in _computedStats.MagicDodgeInstances)
                    {
                        var randomValue = UnityEngine.Random.value;
                        if (randomValue * 100 < dodge) return actualDamageDealt;
                    }
                }
                foreach (var crit in _computedStats.MagicCritInstances)
                {
                    var randomValue = UnityEngine.Random.value;
                    if (randomValue * 100 < crit.CritChance) di *= (crit.CritDamage / 100);
                }
            }
            else if (di.isAttack)
            {
                if (!di.trueStrike)
                {
                    foreach (var dodge in _computedStats.DodgeInstances)
                    {
                        var randomValue = UnityEngine.Random.value;
                        if (randomValue * 100 < dodge) return actualDamageDealt;
                    }
                }
                foreach (var crit in _computedStats.CritInstances)
                {
                    var randomValue = UnityEngine.Random.value;
                    if (randomValue * 100 < crit.CritChance) di *= (crit.CritDamage / 100);
                }
            }
        }
        float damageMultiplier = GetDamageMultiplier(di);
        float defenseMultiplier = GetDefenseMultiplier();
        actualDamageDealt = (di.hitPoints) * damageMultiplier * defenseMultiplier;
        currentHitPoints -= actualDamageDealt;
        currentManaPoints -= (di.manaPoints) * damageMultiplier * defenseMultiplier;
        return actualDamageDealt;
    }

    private float GetDamageMultiplier(DamageInstance di)
    {
        if (di.isAbility)
        {
            switch (di.damageType)
            {
                case (DamageTypes.Magic):
                case (DamageTypes.Normal):
                case (DamageTypes.Siege):
                case (DamageTypes.Piercing):
                case (DamageTypes.Unarmed):
                    return (_computedStats.MagicResistance) / (GlobalVariables.magicResistenceEffectiveness + _computedStats.MagicResistance);
                case (DamageTypes.Hero):
                    return ( _computedStats.MagicResistance) / (GlobalVariables.magicResistenceEffectiveness + (float)0.8 *_computedStats.MagicResistance);
                case (DamageTypes.Chaos):
                    return ( _computedStats.MagicResistance) / (GlobalVariables.magicResistenceEffectiveness + (float)0.6 * _computedStats.MagicResistance);
                case (DamageTypes.Pure):
                    return 1;
                default:
                    return (_computedStats.MagicResistance) / (GlobalVariables.magicResistenceEffectiveness + _computedStats.MagicResistance);
            }
        }
        if (di.isAttack)
        {
            switch (di.damageType)
            {
                case (DamageTypes.Magic):
                case (DamageTypes.Normal):
                case (DamageTypes.Siege):
                case (DamageTypes.Piercing):
                case (DamageTypes.Unarmed):
                    return (_computedStats.ArmorRating) / (GlobalVariables.armorEffectiveness + _computedStats.ArmorRating);
                case (DamageTypes.Hero):
                    return (_computedStats.ArmorRating) / (GlobalVariables.armorEffectiveness + (float)0.8 * _computedStats.ArmorRating);
                case (DamageTypes.Chaos):
                    return (_computedStats.ArmorRating) / (GlobalVariables.armorEffectiveness + (float)0.6 * _computedStats.ArmorRating);
                case (DamageTypes.Pure):
                    return 1;
                default:
                    return (_computedStats.ArmorRating) / (GlobalVariables.armorEffectiveness + _computedStats.ArmorRating);
            }
        }
        return 0;
    }

    private float GetDefenseMultiplier()
    {
        switch (armorType)
        {
            case (ArmorTypes.Light):
                return (float)1;
            case (ArmorTypes.Normal):
                return (float)1;
            case (ArmorTypes.Unarmored):
                return (float)1;
            case (ArmorTypes.Heavy):
                return (float)0.95;
            case (ArmorTypes.Fortified):
                return (float)0.80;
            case (ArmorTypes.Divine):
                return (float)0.65;
            case (ArmorTypes.Impregnable):
                return (float)0.5;
            default:
                return 1;
        }
    }

    void Target(GameObject gameObject)
    {
        var targetChar = gameObject.GetComponent<CharacterBase>();
        if (targetChar != null)
        {
            target = gameObject;
        }
    }

    public UnitStats MutateStats(UnitStats unitStats)
    {
        if (unitStats.isPercentage)
        {
            _statsPercentageMutation += unitStats;
        }
        else
        {
            _statsMutation += unitStats;
        }

        var percStats = _statsPercentageMutation / 100;
        percStats = percStats + 1;
        _computedStats = _statsMutation + baseStats;
        _computedStats = _computedStats * percStats;

        // Compute strength additions
        switch (primaryAttribute)
        {
            case (Attribute.Strength): _computedStats.AttackDamage += _computedStats.Strength; break;
            case (Attribute.Agility): _computedStats.AttackDamage += _computedStats.Agility; break;
            case (Attribute.Intelligence): _computedStats.AttackDamage += _computedStats.Intelligence; break;
            default: print("Unexpected attribute you noob."); break;
        }

        _computedStats.HitPoints += (int)(_computedStats.Strength * GlobalVariables.hpPerStrength);
        _computedStats.HitPointRegen += (int)(_computedStats.Strength * GlobalVariables.healthRegenPerStrength);
        _computedStats.ArmorRating += (int)(_computedStats.Agility * GlobalVariables.armorPerAgility);
        _computedStats.AttackSpeed += (int)(_computedStats.Agility * GlobalVariables.attackSpeedPerAgility);
        _computedStats.ManaPoints += (int)(_computedStats.Intelligence * GlobalVariables.manaPerIntelligence);
        _computedStats.ManaRegen += (int)(_computedStats.Intelligence * GlobalVariables.manaRegenPerIntelligence);
        _computedStats.MagicAmplification += (int)(_computedStats.Intelligence * GlobalVariables.magicAmplificationPerIntelligence);

        return _computedStats;
    }

    public void ActivateAbility(int index)
    {
        if (abilities.Count > index)
            abilities[index].OnActivate();
        else print("Ability index out of bounds");
    }

    public void LevelUp()
    {
        baseStats += statsGrowth;
        currentExperience = 0;
        neededExperience = (long)(neededExperience * GlobalVariables.experienceGrowthFactor);
        _statsMutation = new UnitStats();
        _computedStats = new UnitStats();
        foreach (var effect in activeEffects)
        {
            effect.DoEffect(this);
        }
        abilityPoints += 1;
        level += 1;
    }

    public void LevelAbility(int index)
    {
        if (abilities.Count > index)
            abilities[index].OnLevelUp();
        else print("Ability index out of bounds");
    }

    public void Die()
    {
        //Perform die animation

        //Kill object
    }
}