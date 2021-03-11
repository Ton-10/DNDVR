using UnityEngine;

namespace Skills
{
    public interface ISkill
    {
        Skill GetSkillInfo();

        void InvokeSkill(bool pressing, OVRInput.Controller controller = OVRInput.Controller.None);
    }
}

