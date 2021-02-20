using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour
{
    public enum DamageType
    {
        None,
        Physical,
        Fire,
        Water,
        Earth,
        Air,
    }
    public enum StatusEffect
    {
        None,
        Bleeding,
        Burning,
        Wet,
        Cracked,
        Swirled,
    }

    public static StatusEffect GetStatusFromType( DamageType damageType)
    {
        switch (damageType)
        {
            case DamageType.None:
                return StatusEffect.None;
            case DamageType.Physical:
                return StatusEffect.Bleeding;
            case DamageType.Fire:
                return StatusEffect.Burning;
            case DamageType.Water:
                return StatusEffect.Wet;
            case DamageType.Earth:
                return StatusEffect.Cracked;
            case DamageType.Air:
                return StatusEffect.Swirled;
        }
        return StatusEffect.None;
    }
}
