using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    public GameObject owner;
    public GameObject target;

    public UnitStats baseStats;
    public ArmorTypes armorType;
    public DamageTypes attackType;

    public List<ItemBase> inventory;
    //public bool inventoryMutated;
    public List<AbilityBase> abilities;
    //public bool abilitiesMutated;
    public List<EffectBase> activeEffects;
    public List<DamageInstance> queuedDamage;

    public ItemBase headItem;
    public ItemBase chestItem;
    public ItemBase weaponLeftItem;
    public ItemBase weaponRightItem;
    public ItemBase glovesItem;
    public ItemBase waistItem;
    public ItemBase legsItem;
    public ItemBase feetItem;

    public bool equipmentMutated;

    public bool isIdling;
    public bool isMoving;
    public bool isAttacking;
    public bool isCasting;
    public bool isStunned;
    public bool isEthereal;
    public bool isMagicImmune;
    public bool isInvulnerable;

    public UnitStats computedStats;

    public double currentHitPoints;
    public double currentManaPoints;

    void Start()
    {
        inventory = new List<ItemBase>();
        abilities = new List<AbilityBase>();
        activeEffects = new List<EffectBase>();
        queuedDamage = new List<DamageInstance>();

        //inventoryMutated = true;
        //abilitiesMutated = true;
        equipmentMutated = true;
    }


    void Update()
    {
        //Do all effects which are due to run
        foreach( var effect in activeEffects.Where(x => (x.lastInstance ?? 0) + (x.interval ?? 0) <= Time.time))
        {
            effect.DoEffect(this);
        }
        //Apply all queued damage instances
        foreach(var damage in queuedDamage)
        {
            Damage(damage);
        }
        queuedDamage = new List<DamageInstance>();

        //Remove Expired effects
        int effectsRemoved = activeEffects.RemoveAll(effect => effect.aura && Vector3.Distance(effect.owner.owner.transform.position, owner.transform.position) > (effect.radius?? 0));
        effectsRemoved += activeEffects.RemoveAll(effect => !effect.passive && (effect.initialTime??0) + (effect.duration??0) < Time.time);

        //Calculate new stats after removing effects
        if (effectsRemoved > 0)
        {

        }
    }
    private void LateUpdate()
    {

    }

    void Damage(DamageInstance di)
    {
        if (!isInvulnerable)
        {
            if(di.isAbility)
            {
                if (!di.trueStrike) {
                    var randomValue = Random.value;
                    if (randomValue * 100 < computedStats.MagicDodge) return;
                }
            } else if (di.isAttack)
            {
                if (!di.trueStrike)
                {
                    var randomValue = Random.value;
                    if (randomValue * 100 < computedStats.Dodge) return;
                }
            }
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
    
    void LevelUp()
    {

    }

    void LevelAbility()
    {

    }
}

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