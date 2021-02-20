using System;
using UnityEngine;
using static Helper;

public class EntityStats : MonoBehaviour
{
    public int ATK,HP,DEF,MR;
    public StatusEffect Status;

    // Start is called before the first frame update
    void Start()
    {
        ATK = 5;
        HP = 100;
        DEF = 5;
        MR = 5;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int attackValue, DamageType damageType)
    {
        int baseDamage;
        if(damageType == DamageType.Physical)
        {
            baseDamage = (int)(attackValue - (DEF * 0.2));
        }
        else
        {
            baseDamage = (int)(attackValue - (MR * 0.2));
        }
        Debug.Log($"Base Damage : {baseDamage}");

        int finalDamage = 0;
        switch (Status)
        {
            case StatusEffect.Bleeding:
                break;
            case StatusEffect.Burning:
                finalDamage = BurnReaction(baseDamage, damageType);
                break;
            case StatusEffect.Wet:
                finalDamage = WetReaction(baseDamage, damageType);
                break;
            case StatusEffect.Cracked:
                finalDamage = CrackedReaction(baseDamage, damageType);
                break;
            case StatusEffect.Swirled:
                finalDamage = SwirlReaction(baseDamage, damageType);
                break;
            default:
                finalDamage = SetStatus(baseDamage, damageType);
                break;
        }
        Status = GetStatusFromType(damageType);

        if( finalDamage < 0)
        {
            finalDamage = 0;
        }

        if((HP -= finalDamage) >= 0)
        {
            HP -= finalDamage;
        }
        else
        {
            HP = 0;
        }

        Debug.Log($"Took {finalDamage} points of {damageType} damage. Enemy is at {HP} HP with a {Status} status effect!");
    }

    public int BurnReaction(int damage, DamageType damageType)
    {
        if (damageType == DamageType.Water)
        {
            return (int)(damage * 1.5);
        }
        Status = StatusEffect.Burning;
        return damage;
    }

    public int WetReaction(int damage, DamageType damageType)
    {
        if (damageType == DamageType.Fire)
        {
            return (int)(damage * 1.5);
        }
        return damage;
    }

    public int CrackedReaction(int damage, DamageType damageType)
    {
        if(damageType == DamageType.Physical)
        {
            return (int)(damage * 1.5);
        }
        return damage;
    }

    public int SwirlReaction(int damage, DamageType damageType)
    {
        SetStatus(damage, damageType);
        return damage;
    }


    public int SetStatus(int damage, DamageType damageType)
    {
      Status = GetStatusFromType(damageType);
      return damage;
    }
}
