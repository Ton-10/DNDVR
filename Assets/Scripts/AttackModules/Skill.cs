using System.Collections.Generic;
using UnityEngine;
using static Helper;

public class Skill
{
    public List<GameObject> SkillPrefabs = new List<GameObject>();
    public GameObject RHand = null;
    public GameObject LHand = null;
    public bool OnCoolDown = false;
    public GameObject[] SkillObjects;
    public RaycastHit RayTarget;
    public GameObject TargetEnemy;

    public DamageType DamageType;
    public EntityStats PlayerStats;

    public void SetEnemy(RaycastHit hit)
    {
        RayTarget = hit;
        if (hit.transform.gameObject.CompareTag("Enemy"))
        {
            TargetEnemy = hit.transform.gameObject;
        };
    }

    public Skill(
        List<GameObject> skillPrefabs,
        GameObject rHand = null,
        GameObject lHand = null,
        DamageType damageType = DamageType.None,
        EntityStats playerStats = null)
    {
        SkillPrefabs = skillPrefabs;
        SkillObjects = new GameObject[SkillPrefabs.Count];
        RHand = rHand;
        LHand = lHand;
        DamageType = damageType;
        PlayerStats = playerStats;
    }
}
