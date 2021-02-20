using UnityEngine;
namespace Skills
{
    public class FireBall : MonoBehaviour, ISkill
    {
        public Skill BaseSkillInfo;

        // Start is called before the first frame update
        void Start()
        {
            BaseSkillInfo =
                new Skill(
                    skillPrefab: Resources.Load("Prefabs/FireBall") as GameObject,
                    rHand: GameObject.FindGameObjectWithTag("RightHand"),
                    lHand: GameObject.FindGameObjectWithTag("LeftHand"),
                    damageType: Helper.DamageType.Fire,
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
                BaseSkillInfo.SkillObject = Instantiate(BaseSkillInfo.SkillPrefab, hand.transform.position, Quaternion.identity);
            }
            else if (!pressing && BaseSkillInfo.OnCoolDown && BaseSkillInfo.SkillObject != null)
            {
                // Release
                BaseSkillInfo.OnCoolDown = false;
                BaseSkillInfo.SkillObject.GetComponent<Rigidbody>().AddForce(BaseSkillInfo.SkillObject.transform.forward * 500);

                DamageScript DmgScript = BaseSkillInfo.SkillObject.AddComponent<DamageScript>();
                DmgScript.Attack = BaseSkillInfo.PlayerStats.ATK;
                DmgScript.DamageType = BaseSkillInfo.DamageType;

                Destroy(BaseSkillInfo.SkillObject, 10.0f);
            }
            else if (pressing && BaseSkillInfo.SkillObject != null)
            {
                // Holding
                BaseSkillInfo.SkillObject.transform.position = hand.transform.position;
                BaseSkillInfo.SkillObject.transform.rotation = hand.transform.rotation;
            }
        }
    }
}
