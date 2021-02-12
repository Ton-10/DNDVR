using System.Collections.Generic;
using UnityEngine;
using Skills;

public class ControllerScript : MonoBehaviour
{
    public List<MonoBehaviour> SkillList = new List<MonoBehaviour>();
    List<ISkill> attachedSkills = new List<ISkill>();
    GameObject RHand = null;
    GameObject LHand = null;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Object skill in SkillList)
        {
            System.Type type = skill.GetType();
            attachedSkills.Add(gameObject.AddComponent(skill.GetType()) as ISkill);
        }
        RHand = GameObject.FindGameObjectWithTag("RightHand");
        LHand = GameObject.FindGameObjectWithTag("LeftHand");
    }

    // Update is called once per frame
    void Update()
    {
        PrimaryAttack(attachedSkills[0], OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch);
        PrimaryAttack(attachedSkills[1], OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch);
    }

    public void PrimaryAttack(ISkill skill, OVRInput.Button button, OVRInput.Controller controller)
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
            Debug.Log("Holding Down");
            skill.InvokeSkill(true, controller);
        }
    }
}
