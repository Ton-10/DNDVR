using Skills;
using UnityEngine;

public class FireErupt : MonoBehaviour, ISkill
{
    public Skill baseSkillInfo = new Skill();

    LineRenderer lineRenderer = null;
    Vector3[] Arraywithpositions = null;

    // Start is called before the first frame update
    void Start()
    {
        baseSkillInfo.SkillPrefab = Resources.Load("Prefabs/Eruption") as GameObject;
        baseSkillInfo.RHand = GameObject.FindGameObjectWithTag("RightHand");
        baseSkillInfo.LHand = GameObject.FindGameObjectWithTag("LeftHand");
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
            hand = baseSkillInfo.LHand;
        }
        if (controller == OVRInput.Controller.RTouch)
        {
            hand = baseSkillInfo.RHand;
        }

        if (pressing && !baseSkillInfo.onCoolDown)
        {
            // Pressing
            baseSkillInfo.onCoolDown = true;
            Physics.Raycast(hand.transform.position, hand.transform.TransformDirection(Vector3.forward), out baseSkillInfo.rayTarget, 1000f);
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.material = Resources.Load("Materials/FireBall") as Material;
            lineRenderer.widthMultiplier = 0.01f;
            lineRenderer.positionCount = 2;
            Arraywithpositions = new Vector3[2];
            Arraywithpositions[0] = hand.transform.position;
            Arraywithpositions[1] = baseSkillInfo.rayTarget.point;
            lineRenderer.SetPositions(Arraywithpositions);
        }
        else if (!pressing && baseSkillInfo.onCoolDown && lineRenderer != null)
        {
            // Release
            baseSkillInfo.onCoolDown = false;
            baseSkillInfo.skillObject = Instantiate(baseSkillInfo.SkillPrefab, baseSkillInfo.rayTarget.point, Quaternion.identity);
            Destroy(lineRenderer);
            Destroy(baseSkillInfo.skillObject, 10.0f);
        }
        else if (pressing && lineRenderer != null)
        {
            // Holding
            Physics.Raycast(hand.transform.position, hand.transform.TransformDirection(Vector3.forward), out baseSkillInfo.rayTarget, 1000f);
            Arraywithpositions[0] = hand.transform.position;
            Arraywithpositions[1] = baseSkillInfo.rayTarget.point;
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
