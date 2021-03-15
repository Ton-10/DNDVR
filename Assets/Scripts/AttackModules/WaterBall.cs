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
                    user: gameObject,
                    rHand: GameObject.FindGameObjectWithTag("RightHand"),
                    lHand: GameObject.FindGameObjectWithTag("LeftHand"),
                    damageType: Helper.DamageType.Water,
                    playerStats: gameObject.GetComponent<EntityStats>()
                );
        }

        void Update()
        {

        }

        public Skill GetSkillInfo()
        {
            return BaseSkillInfo;
        }

        public void InvokeSkill(bool pressing, OVRInput.Controller controller)
        {
            GameObject hand = null;

            if (gameObject.CompareTag("Player"))
            {
                if (controller == OVRInput.Controller.LTouch)
                {
                    hand = BaseSkillInfo.LHand;
                }
                if (controller == OVRInput.Controller.RTouch)
                {
                    hand = BaseSkillInfo.RHand;
                }
            }

            // Bypass Player Logic If skill used by enemy
            if (gameObject.CompareTag("Enemy"))
            {
                hand = gameObject;
                BaseSkillInfo.SkillObjects[0] = Instantiate(BaseSkillInfo.SkillPrefabs[0], hand.transform.localPosition + new Vector3(0, 0, 5), Quaternion.identity);
                ReleaseWaterBall();
            }
            else if (pressing && !BaseSkillInfo.OnCoolDown)
            {
                // Pressing
                BaseSkillInfo.OnCoolDown = true;
                BaseSkillInfo.SkillObjects[0] = Instantiate(BaseSkillInfo.SkillPrefabs[0], hand.transform.position, Quaternion.identity);
            }
            else if (!pressing && BaseSkillInfo.OnCoolDown && BaseSkillInfo.SkillObjects[0] != null)
            {
                // Release
                ReleaseWaterBall();
            }
            else if (pressing && BaseSkillInfo.SkillObjects[0] != null)
            {
                // Holding
                BaseSkillInfo.SkillObjects[0].transform.position = hand.transform.position;
                BaseSkillInfo.SkillObjects[0].transform.rotation = hand.transform.rotation;
            }
        }
        public void ReleaseWaterBall()
        {
            BaseSkillInfo.OnCoolDown = false;

            if (gameObject.CompareTag("Enemy"))
            {
                BaseSkillInfo.SkillObjects[0].transform.LookAt(BaseSkillInfo.TargetEnemy.transform.position);
            }

            BaseSkillInfo.SkillObjects[0].GetComponent<Rigidbody>().AddForce(BaseSkillInfo.SkillObjects[0].transform.forward * 500);

            DamageScript DmgScript = BaseSkillInfo.SkillObjects[0].AddComponent<DamageScript>();
            DmgScript.TargetTag = BaseSkillInfo.TargetTag;
            DmgScript.Attack = BaseSkillInfo.PlayerStats.ATK;
            DmgScript.DamageType = BaseSkillInfo.DamageType;

            Destroy(BaseSkillInfo.SkillObjects[0], 10.0f);
        }
    }
}
