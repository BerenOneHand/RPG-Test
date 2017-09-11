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

    private int _ComputedAttackSpeed;
    private int _ComputedAttackDamage;

    private long _ComputedHitPoints;
    private long _ComputedManaPoints;
    private float _ComputedHitPointRegen;
    private float _ComputedManaRegen;
    private int _ComputedStrength;
    private int _ComputedAgility;
    private int _ComputedIntelligence;

    private int _ComputedAttackRating;
    private int _ComputedDefenseRating;
    private int _ComputedArmorRating;
    private int _ComputedDodge;

    private int _ComputedMagicDodge;
    private int _ComputedMagicResistance;
    private int _ComputedMagicAmplification;

    private int _ComputedMovementSpeed;

    private float _ComputedCritChance;
    private float _ComputedCritDamage;

    public int ComputedAttackSpeed{ get;  }
    public int ComputedAttackDamage{ get;  }

    public long ComputedHitPoints{ get;  }
    public long ComputedManaPoints{ get;  }
    public float ComputedHitPointRegen{ get;  }
    public float ComputedManaRegen{ get;  }
    public int ComputedStrength{ get;  }
    public int ComputedAgility{ get;  }
    public int ComputedIntelligence{ get;  }

    public int ComputedAttackRating{ get;  }
    public int ComputedDefenseRating{ get;  }
    public int ComputedArmorRating{ get;  }
    public int ComputedDodge{ get;  }

    public int ComputedMagicDodge{ get;  }
    public int ComputedMagicResistance{ get;  }
    public int ComputedMagicAmplification{ get;  }

    public int ComputedMovementSpeed{ get;  }

    public float ComputedCritChance{ get;  }
    public float ComputedCritDamage{ get;  }

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
