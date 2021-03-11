using Skills;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthErupt : MonoBehaviour, ISkill
{
    public Skill BaseSkillInfo;

    LineRenderer lineRenderer = null;
    Vector3[] Arraywithpositions = null;
    RaycastHit TempTarget;
    Vector3 Target;
    GameObject HitIndicator;

    // Start is called before the first frame update
    void Start()
    {
        BaseSkillInfo =
            new Skill(
                skillPrefabs: new List<GameObject> { Resources.Load("Prefabs/EarthPreEruption") as GameObject, Resources.Load("Prefabs/EarthEruption") as GameObject },
                user: gameObject,
                rHand: GameObject.FindGameObjectWithTag("RightHand"),
                lHand: GameObject.FindGameObjectWithTag("LeftHand"),
                damageType: Helper.DamageType.Earth,
                playerStats: gameObject.GetComponent<EntityStats>()
            );
    }

    // Update is called once per frame
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
            ReleaseErupt();
        }
        else if (pressing && !BaseSkillInfo.OnCoolDown)
        {
            Debug.Log("Started Eruption!");
            // Pressing
            BaseSkillInfo.OnCoolDown = true;

            Physics.Raycast(hand.transform.position, hand.transform.TransformDirection(Vector3.forward), out TempTarget, 1000f);
            BaseSkillInfo.RayTarget = TempTarget;

            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.material = Resources.Load("Materials/FireBall") as Material;
            lineRenderer.widthMultiplier = 0.01f;
            lineRenderer.positionCount = 2;

            Arraywithpositions = new Vector3[2];
            Arraywithpositions[0] = hand.transform.position;
            Arraywithpositions[1] = BaseSkillInfo.RayTarget.point;

            lineRenderer.SetPositions(Arraywithpositions);
        }
        else if ((!pressing && BaseSkillInfo.OnCoolDown && lineRenderer != null))
        {
            // Release
            ReleaseErupt();
        }
        else if (pressing && lineRenderer != null)
        {
            // Holding
            Debug.Log("Holding Eruption!");
            Physics.Raycast(hand.transform.position, hand.transform.TransformDirection(Vector3.forward), out TempTarget, 1000f);
            BaseSkillInfo.SetEnemy(TempTarget);

            Arraywithpositions[0] = hand.transform.position;
            Arraywithpositions[1] = BaseSkillInfo.RayTarget.point;

            lineRenderer.SetPositions(Arraywithpositions);
        }
    }

    public GameObject makeSphere(Vector3 center, float diameter, GameObject sphere = null)
    {
        GameObject obj = null;
        if (sphere == null)
        {
            obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            obj.transform.localScale = new Vector3(diameter, diameter, diameter);
            obj.transform.position = center;
            Destroy(obj, 5f);
            return obj;
        }
        else
        {
            obj = sphere;
            obj.transform.localScale = new Vector3(diameter, diameter, diameter);
            obj.transform.position = center;
            Destroy(obj, 5f);
            return obj;
        }
    }
    public void ReleaseErupt()
    {
        BaseSkillInfo.OnCoolDown = false;
        RaycastHit initialHit;

        if (BaseSkillInfo.TargetEnemy != null)
        {
            Physics.Raycast(
                BaseSkillInfo.TargetEnemy.transform.localPosition,
                BaseSkillInfo.TargetEnemy.transform.TransformDirection(Vector3.down),
                out initialHit,
                50f);
            Target = initialHit.point;

            StartCoroutine(DamageAfterTime(0.5f));
        }
        else
        {
            Target = BaseSkillInfo.RayTarget.point;
        }

        // Eruption Base Effect
        BaseSkillInfo.SkillObjects[0] = Instantiate(
            BaseSkillInfo.SkillPrefabs[0],
            Target + new Vector3(0, BaseSkillInfo.SkillPrefabs[0].transform.localScale.y, 0),
            Quaternion.identity);

        // Eruption Pillar
        BaseSkillInfo.SkillObjects[1] = Instantiate(
            BaseSkillInfo.SkillPrefabs[1],
            Target + new Vector3(0, -BaseSkillInfo.SkillPrefabs[1].transform.localScale.y, 0),
            Quaternion.identity);

        Destroy(lineRenderer);

        // Move Pillar Up
        Vector3 targetPos = Target + new Vector3(0, 12, 0);
        BaseSkillInfo.SkillObjects[1].AddComponent<Move>().SetValues(
            new[] { targetPos, targetPos + new Vector3(0, 6, 0) },
            new[] { 0.001f, 0.005f },
            new[] { 0.5f, 0.5f });

        BaseSkillInfo.TargetEnemy = null;

        Destroy(BaseSkillInfo.SkillObjects[0], 1f);
    }
    IEnumerator DamageAfterTime(float time)
    {
        EntityStats enemyStats = BaseSkillInfo.TargetEnemy.GetComponent<EntityStats>();

        yield return new WaitForSeconds(time);

        Debug.Log($"Damaging {BaseSkillInfo.TargetEnemy}");
        enemyStats.TakeDamage(BaseSkillInfo.PlayerStats.ATK, BaseSkillInfo.DamageType);
    }
}
