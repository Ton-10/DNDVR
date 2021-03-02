using System.Collections.Generic;
using UnityEngine;
namespace Skills
{
    public class WaterBall : MonoBehaviour, ISkill
    {
        public Skill BaseSkillInfo;

        // Start is called before the first frame update
        void Start()
        {
            BaseSkillInfo =
                new Skill(
                    skillPrefabs: new List<GameObject> { Resources.Load("Prefabs/WaterBall") as GameObject },
                    rHand: GameObject.FindGameObjectWithTag("RightHand"),
                    lHand: GameObject.FindGameObjectWithTag("LeftHand"),
                    damageType: Helper.DamageType.Water,
                    playerStats: gameObject.GetComponent<EntityStats>()
                );
        }

        void Update()
        {

        }

        public void InvokeSkill(bool pressing, OVRInput.Controller controller)
        {

            GameObject hand = null;

            if(controller == OVRInput.Controller.LTouch)
            {
                hand = BaseSkillInfo.LHand;
            }
            if(controller == OVRInput.Controller.RTouch){
                hand = BaseSkillInfo.RHand;
            }

            if (pressing && !BaseSkillInfo.OnCoolDown)
            {
                // Pressing
                BaseSkillInfo.OnCoolDown = true;
                BaseSkillInfo.SkillObjects[0] = Instantiate(BaseSkillInfo.SkillPrefabs[0], hand.transform.position, Quaternion.identity);
            }
            else if (!pressing && BaseSkillInfo.OnCoolDown && BaseSkillInfo.SkillObjects[0] != null)
            {
                // Release
                BaseSkillInfo.OnCoolDown = false;
                BaseSkillInfo.SkillObjects[0].GetComponent<Rigidbody>().AddForce(BaseSkillInfo.SkillObjects[0].transform.forward * 500);

                DamageScript DmgScript = BaseSkillInfo.SkillObjects[0].AddComponent<DamageScript>();
                DmgScript.Attack = BaseSkillInfo.PlayerStats.ATK;
                DmgScript.DamageType = BaseSkillInfo.DamageType;

                Destroy(BaseSkillInfo.SkillObjects[0], 10.0f);
            }
            else if (pressing && BaseSkillInfo.SkillObjects[0] != null)
            {
                // Holding
                BaseSkillInfo.SkillObjects[0].transform.position = hand.transform.position;
                BaseSkillInfo.SkillObjects[0].transform.rotation = hand.transform.rotation;
            }
        }
    }
}
