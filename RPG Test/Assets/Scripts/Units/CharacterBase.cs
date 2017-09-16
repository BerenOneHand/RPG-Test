using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

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
            DealDamage(damage);
        }
        queuedDamage = new List<DamageInstance>();

        //Calculate new stats after removing effects
        if (effectsRemoved > 0)
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

    public float DealDamage(DamageInstance di)
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
                        var randomValue = Random.value;
                        if (randomValue * 100 < dodge) return actualDamageDealt;
                    }
                }
                foreach (var crit in _computedStats.MagicCritInstances)
                {
                    var randomValue = Random.value;
                    if (randomValue * 100 < crit.CritChance) di *= (crit.CritDamage / 100);
                }
            }
            else if (di.isAttack)
            {
                if (!di.trueStrike)
                {
                    foreach (var dodge in _computedStats.DodgeInstances)
                    {
                        var randomValue = Random.value;
                        if (randomValue * 100 < dodge) return actualDamageDealt;
                    }
                }
                foreach (var crit in _computedStats.CritInstances)
                {
                    var randomValue = Random.value;
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
                    return (_computedStats.MagicResistance) / GlobalVariables.magicResistenceEffectiveness;
                case (DamageTypes.Hero):
                    return ((float)0.8 * _computedStats.MagicResistance) / GlobalVariables.magicResistenceEffectiveness;
                case (DamageTypes.Chaos):
                    return ((float)0.6 * _computedStats.MagicResistance) / GlobalVariables.magicResistenceEffectiveness;
                case (DamageTypes.Pure):
                    return 1;
                default:
                    return (_computedStats.MagicResistance) / GlobalVariables.magicResistenceEffectiveness;
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
                    return (_computedStats.ArmorRating) / GlobalVariables.armorEffectiveness;
                case (DamageTypes.Hero):
                    return ((float)0.8 * _computedStats.ArmorRating) / GlobalVariables.armorEffectiveness;
                case (DamageTypes.Chaos):
                    return ((float)0.6 * _computedStats.ArmorRating) / GlobalVariables.armorEffectiveness;
                case (DamageTypes.Pure):
                    return 1;
                default:
                    return (_computedStats.ArmorRating) / GlobalVariables.armorEffectiveness;
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

    public void Die()
    {
        //Perform die animation

        //Kill object
    }
}