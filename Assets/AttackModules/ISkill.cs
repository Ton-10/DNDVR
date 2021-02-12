using UnityEngine;

namespace Skills
{
    public interface ISkill
    {
        void InvokeSkill(bool pressing, OVRInput.Controller controller);
    }
}

