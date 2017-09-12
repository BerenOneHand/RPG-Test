using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityBase : MonoBehaviour {
	public enum TargetType { Self, Ally, Enemy, Ground }
    public float castTime;
    public float effectRadius;
    public float coolDown;
	public float lastUse;
	public List<TargetType> targetable;

    public List<EffectBase> effects;

}
