using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EncounterListenerScript : MonoBehaviour
{
    public List<Collider> HitColliders;
    public List<GameObject> Combatants;
    public CombatManager CombatManager;
    public int Enemies, Players;
    public bool StartFight;

    // Start is called before the first frame update
    void Start()
    {
        Combatants = new List<GameObject>();
    }

    // for visually debugging distance
    private void OnDrawGizmosSelected()
    {
     Gizmos.color = Color.red;
     //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
     Gizmos.DrawWireSphere(gameObject.transform.position, 8);
    }

    // Update is called once per frame
    void Update()
    {
        // Get all gameBbjects within radius from player
        HitColliders = Physics.OverlapSphere(gameObject.transform.position, 8).ToList();

        int currentNumberEnemies = Enemies;

        // Add the found players and enemies to the combatants list

        List<GameObject> playerList = HitColliders
            .Select(obj => obj.gameObject)
            .Where(obj => obj.CompareTag("Player"))
            .ToList();

        List<GameObject> enemyList = HitColliders
            .Select(obj => obj.gameObject)
            .Where(obj => obj.CompareTag("Enemy"))
            .ToList();

        Players = playerList.Count;
        Enemies = enemyList.Count;

        Combatants = playerList
            .Concat(enemyList)
            .OrderByDescending(obj => obj.GetComponent<EntityStats>().SPEED)
            .ToList();

        if(currentNumberEnemies < Enemies)
        {

        }

        // Initiate fight if there are Combatants and the StartFight bool is on.
        if (Combatants != null && StartFight == true)
        {
            StartFight = false;
            foreach (var combatant in Combatants)
            {
                if (combatant.GetComponent<CombatManager>())
                {
                    CombatManager = combatant.GetComponent<CombatManager>();
                    List<GameObject> newCombatants = Combatants.Except(CombatManager.Combatants).ToList();
                    foreach (var item in newCombatants)
                    {
                        CombatManager.AddCombatant(item);
                    }
                    CombatManager.SplitCombatantTypes();
                }
            }
            if(CombatManager == null)
            {
                CombatManager = gameObject.AddComponent<CombatManager>();
                CombatManager.Combatants = Combatants;
            }
        }
    }
}
