using UnityEngine;
namespace Skills
{
    public class FireBall : MonoBehaviour, ISkill
    {
        public Skill baseSkillInfo = new Skill();

        // Start is called before the first frame update
        void Start()
        {
            baseSkillInfo.SkillPrefab = Resources.Load("Prefabs/FireBall") as GameObject;
            baseSkillInfo.RHand = GameObject.FindGameObjectWithTag("RightHand");
            baseSkillInfo.LHand = GameObject.FindGameObjectWithTag("LeftHand");
        }

        void Update()
        {

        }

        public void InvokeSkill(bool pressing, OVRInput.Controller controller)
        {

            GameObject hand = null;

            if(controller == OVRInput.Controller.LTouch)
            {
                hand = baseSkillInfo.LHand;
            }
            if(controller == OVRInput.Controller.RTouch){
                hand = baseSkillInfo.RHand;
            }

            if (pressing && !baseSkillInfo.onCoolDown)
            {
                // Pressing
                baseSkillInfo.onCoolDown = true;
                baseSkillInfo.skillObject = Instantiate(baseSkillInfo.SkillPrefab, hand.transform.position, Quaternion.identity);
            }
            else if (!pressing && baseSkillInfo.onCoolDown && baseSkillInfo.skillObject != null)
            {
                // Release
                baseSkillInfo.onCoolDown = false;
                baseSkillInfo.skillObject.GetComponent<Rigidbody>().AddForce(transform.forward * 500);
                Destroy(baseSkillInfo.skillObject, 5.0f);
            }
            else if (pressing && baseSkillInfo.skillObject != null)
            {
                // Holding
                baseSkillInfo.skillObject.transform.position = hand.transform.position;
                baseSkillInfo.skillObject.transform.rotation = hand.transform.rotation;
            }
        }
    }
}
