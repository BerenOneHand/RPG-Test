using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBase : MonoBehaviour {
	public CharacterBase owner;
    public int effectId;
	public bool causeStun;
	public bool causeEthereal;
	public bool causeMagicImmune;
    public bool causeInvulnerable;
	public bool passive;
	public bool aura;
	public AbilityBase.TargetType targetType;

	public UnitStats statsMutation;

	public float? duration;
	public float? initialTime;
    public float? lastInstance;
    public float? interval;
    public float? radius;

    public CharacterBase DoEffect(CharacterBase targetChar)
    {
        if (initialTime == null) initialTime = Time.time;
        if ((lastInstance??0) + interval <= Time.time)
        {
            lastInstance = Time.time;
        }
        return targetChar;
    }
}
