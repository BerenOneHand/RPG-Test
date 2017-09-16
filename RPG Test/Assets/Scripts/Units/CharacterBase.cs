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
        foreach( var effect in activeEffects)
        {
            effect.DoEffect(this);
        }
        //Apply all queued damage instances
        foreach(var damage in queuedDamage)
        {
            DealDamage(damage);
        }
        queuedDamage = new List<DamageInstance>();

        //Calculate new stats after removing effects
        if (effectsRemoved > 0)
        {
            _statsMutation = new UnitStats();
            _computedStats = new UnitStats();
        }
    }
    private void LateUpdate()
    {

    }

    public float DealDamage(DamageInstance di)
    {
        float actualDamageDealt = 0;
        //if (!isInvulnerable)
        //{
        //    if(di.isAbility)
        //    {
        //        if (!di.trueStrike) {
        //            var randomValue = Random.value;
        //            if (randomValue * 100 < computedStats.MagicDodge) return actualDamageDealt;
        //        }
        //    } else if (di.isAttack)
        //    {
        //        if (!di.trueStrike)
        //        {
        //            var randomValue = Random.value;
        //            if (randomValue * 100 < computedStats.Dodge) return actualDamageDealt;
        //        }
        //    }
        //}
        return actualDamageDealt;
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
        } else
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
            case (Attribute.Strength): _computedStats.AttackDamage += _computedStats.Strength;break;
            case (Attribute.Agility): _computedStats.AttackDamage += _computedStats.Agility;break;
            case (Attribute.Intelligence):_computedStats.AttackDamage += _computedStats.Intelligence;break;
            default: print("Unexpected attribute you noob.");break;
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
}