using System.Collections.Generic;
using UnityEngine;
using Skills;

public class PlayerAbilities : MonoBehaviour
{
    public List<MonoBehaviour> SkillList = new List<MonoBehaviour>();
    List<ISkill> attachedSkills = new List<ISkill>();
    GameObject RHand = null;
    GameObject LHand = null;
    EntityStats entityStats;

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
        if (entityStats.CanAttack)
        {
            TriggerSkill(attachedSkills[0], OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch);
            TriggerSkill(attachedSkills[1], OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch);
            TriggerSkill(attachedSkills[2], OVRInput.Button.One, OVRInput.Controller.LTouch);
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
        {
            Debug.Log("IK Switched");
            GameObject CharacterModel = GameObject.FindGameObjectWithTag("CharacterModel");
            CharacterIK ik = CharacterModel.GetComponent<CharacterIK>();
            ik.ikActive = !ik.ikActive;
        }
    }

    public void TriggerSkill(ISkill skill, OVRInput.Button button, OVRInput.Controller controller)
    {
        if (OVRInput.GetDown(button, controller))
        {
            Debug.Log("Pressed Trigger");
            skill.InvokeSkill(true, controller);
        }
        if (OVRInput.GetUp(button, controller))
        {
            Debug.Log("Released Trigger");
            skill.InvokeSkill(false, controller);
        }
        if (OVRInput.Get(button, controller))
        {
            skill.InvokeSkill(true, controller);
        }
    }

    public void AssignSkills()
    {
                foreach (Object skill in SkillList)
        {
            System.Type type = skill.GetType();
            attachedSkills.Add(gameObject.AddComponent(skill.GetType()) as ISkill);
        }
    }
}
