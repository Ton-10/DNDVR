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
    public GameObject User;

    public DamageType DamageType;
    public string TargetTag;
    public EntityStats PlayerStats;

    public void SetEnemy(RaycastHit hit)
    {
        RayTarget = hit;
        if (hit.transform.gameObject.CompareTag(TargetTag))
        {
            TargetEnemy = hit.transform.gameObject;
        };
    }

    public Skill(
        List<GameObject> skillPrefabs,
        GameObject user,
        GameObject rHand = null,
        GameObject lHand = null,
        DamageType damageType = DamageType.None,
        EntityStats playerStats = null)
    {
        SkillPrefabs = skillPrefabs;
        User = user;
        SkillObjects = new GameObject[SkillPrefabs.Count];
        RHand = rHand;
        LHand = lHand;
        DamageType = damageType;
        PlayerStats = playerStats;

        if (User.CompareTag("Player"))
        {
            TargetTag = "Enemy";
        }
        if (User.CompareTag("Enemy"))
        {
            TargetTag = "Player";
        }
    }
}
