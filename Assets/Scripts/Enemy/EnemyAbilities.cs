using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Skills;

public class EnemyAbilities : MonoBehaviour
{
    public List<MonoBehaviour> SkillList = new List<MonoBehaviour>();
    List<ISkill> attachedSkills = new List<ISkill>();
    List<GameObject> Players = new List<GameObject>();
    GameObject RHand = null;
    GameObject LHand = null;
    EntityStats entityStats;
    Random random = new Random();
    bool checkingAP = false;

    // Start is called before the first frame update
    void Start()
    {
        AssignSkills();
        RHand = GameObject.FindGameObjectWithTag("RightHand");
        LHand = GameObject.FindGameObjectWithTag("LeftHand");
        entityStats = gameObject.GetComponent<EntityStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (checkingAP == false)
        {
            StartCoroutine(CheckNoAP());
        }
        if (entityStats.AP > 0 && entityStats.CanAttack && Players.Count > 0)
        {
            entityStats.AP--;
            int playerNumber = Random.Range(0, Players.Count);
            TriggerSkill(attachedSkills[0], Players[playerNumber]);
        }
    }

    public void StartAttack(List<GameObject> players)
    {
        Players = players;
    }

    public void TriggerSkill(ISkill skill, GameObject target)
    {
        skill.GetSkillInfo().TargetEnemy = target;
        skill.InvokeSkill(true);
    }

    public void AssignSkills()
    {
        foreach (Object skill in SkillList)
        {
            System.Type type = skill.GetType();
            attachedSkills.Add(gameObject.AddComponent(skill.GetType()) as ISkill);
        }
    }

    IEnumerator CheckNoAP()
    {
        checkingAP = true;
        yield return new WaitForSeconds(1.0f);
        if(entityStats.AP <= 0)
        {
            entityStats.TurnFinished = true;
        }
        checkingAP = false;
    }
}
