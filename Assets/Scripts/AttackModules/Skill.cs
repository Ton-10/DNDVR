using Skills;
using UnityEngine;
using static Helper;

public class Skill
{
    public GameObject SkillPrefab;
    public GameObject RHand = null;
    public GameObject LHand = null;
    public bool OnCoolDown = false;
    public GameObject SkillObject = null;
    public RaycastHit RayTarget;
    public DamageType DamageType;
    public EntityStats PlayerStats;

    public Skill(
        GameObject skillPrefab = null,
        GameObject rHand = null,
        GameObject lHand = null,
        DamageType damageType = DamageType.None,
        EntityStats playerStats = null)
    {
        SkillPrefab = skillPrefab;
        RHand = rHand;
        LHand = lHand;
        DamageType = damageType;
        PlayerStats = playerStats;
    }
}
