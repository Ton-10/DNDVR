using Skills;
using UnityEngine;

public class FireErupt : MonoBehaviour, ISkill
{
    public Skill BaseSkillInfo;

    LineRenderer lineRenderer = null;
    Vector3[] Arraywithpositions = null;

    // Start is called before the first frame update
    void Start()
    {
        BaseSkillInfo =
            new Skill(
                skillPrefab: Resources.Load("Prefabs/Eruption") as GameObject,
                rHand: GameObject.FindGameObjectWithTag("RightHand"),
                lHand: GameObject.FindGameObjectWithTag("LeftHand"),
                damageType: Helper.DamageType.Fire,
                playerStats: gameObject.GetComponent<EntityStats>()
            );
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InvokeSkill(bool pressing, OVRInput.Controller controller)
    {
        GameObject hand = null;

        if (controller == OVRInput.Controller.LTouch)
        {
            hand = BaseSkillInfo.LHand;
        }
        if (controller == OVRInput.Controller.RTouch)
        {
            hand = BaseSkillInfo.RHand;
        }

        if (pressing && !BaseSkillInfo.OnCoolDown)
        {
            // Pressing
            BaseSkillInfo.OnCoolDown = true;
            Physics.Raycast(hand.transform.position, hand.transform.TransformDirection(Vector3.forward), out BaseSkillInfo.RayTarget, 1000f);
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.material = Resources.Load("Materials/FireBall") as Material;
            lineRenderer.widthMultiplier = 0.01f;
            lineRenderer.positionCount = 2;
            Arraywithpositions = new Vector3[2];
            Arraywithpositions[0] = hand.transform.position;
            Arraywithpositions[1] = BaseSkillInfo.RayTarget.point;
            lineRenderer.SetPositions(Arraywithpositions);
        }
        else if (!pressing && BaseSkillInfo.OnCoolDown && lineRenderer != null)
        {
            // Release
            BaseSkillInfo.OnCoolDown = false;
            BaseSkillInfo.SkillObject = Instantiate(BaseSkillInfo.SkillPrefab, BaseSkillInfo.RayTarget.point, Quaternion.identity);
            Destroy(lineRenderer);
            Destroy(BaseSkillInfo.SkillObject, 10.0f);
        }
        else if (pressing && lineRenderer != null)
        {
            // Holding
            Physics.Raycast(hand.transform.position, hand.transform.TransformDirection(Vector3.forward), out BaseSkillInfo.RayTarget, 1000f);
            Arraywithpositions[0] = hand.transform.position;
            Arraywithpositions[1] = BaseSkillInfo.RayTarget.point;
            lineRenderer.SetPositions(Arraywithpositions);
        }
    }

    public GameObject makeSphere(Vector3 center, float diameter, GameObject sphere = null)
    {
        GameObject obj = null;
        if(sphere == null)
        {
            obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            obj.transform.localScale = new Vector3(diameter, diameter, diameter);
            obj.transform.position = center;
            return obj;
        }
        else
        {
            obj = sphere;
            obj.transform.localScale = new Vector3(diameter, diameter, diameter);
            obj.transform.position = center;
            return obj;
        }

    }
}
