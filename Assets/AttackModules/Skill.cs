using Skills;
using UnityEngine;

public class Skill
{
    public GameObject SkillPrefab;
    public GameObject RHand = null;
    public GameObject LHand = null;
    public bool onCoolDown = false;
    public GameObject skillObject = null;
    public RaycastHit rayTarget;
}
