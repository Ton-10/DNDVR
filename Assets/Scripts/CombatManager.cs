using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public List<GameObject> Combatants = new List<GameObject>();
    public List<GameObject> Players = new List<GameObject>();
    public List<GameObject> Enemies = new List<GameObject>();
    public GameObject CurrentActor;
    private int currentIndice;

    // Start is called before the first frame update
    void Start()
    {
        currentIndice = 0;
        foreach (GameObject combatant in Combatants)
        {
            EntityStats entityStats = combatant.GetComponent<EntityStats>();
            entityStats.CanAttack = false;
            entityStats.TurnFinished = false;
            if (combatant.CompareTag("Player"))
            {
                combatant.GetComponent<OVRPlayerController>().EnableLinearMovement = entityStats.CanAttack;
            }
        }

        SplitCombatantTypes();
    }

    // Update is called once per frame
    void Update()
    {
        bool areEnemiesAlive = Enemies.Select(enemy => enemy.GetComponent<EntityStats>().HP > 0).ToList().Contains(true);
        bool arePlayersAlive = Players.Select(player => player.GetComponent<EntityStats>().HP > 0).ToList().Contains(true);

        bool isFightOver = !areEnemiesAlive || !arePlayersAlive;

        // Core fight loop logic
        if (!isFightOver)
        {
            int numberOfCombatants = Combatants.Count;
            CurrentActor = Combatants[currentIndice];
            EntityStats currentActorStats = CurrentActor.GetComponent<EntityStats>();

            currentActorStats.CanAttack = true;
            if (CurrentActor.CompareTag("Player"))
            {
                CurrentActor.GetComponent<OVRPlayerController>().EnableLinearMovement = currentActorStats.CanAttack;
            }
            

            if (CurrentActor.GetComponent<EntityStats>().TurnFinished)
            {
                currentActorStats.TurnFinished = false;
                currentActorStats.CanAttack = false;

                if (CurrentActor.CompareTag("Player"))
                {
                    CurrentActor.GetComponent<OVRPlayerController>().EnableLinearMovement = currentActorStats.CanAttack;
                }

                currentIndice++;

                if (currentIndice > numberOfCombatants - 1)
                {
                    currentIndice = 0;
                }
            }

        }
        else
        {
            Debug.Log("Fight Over");
            Destroy(this);
        }

    }

    public void SplitCombatantTypes()
    {
        Players = new List<GameObject>();
        Enemies = new List<GameObject>();
      foreach (GameObject combatant in Combatants)
        {
            if (combatant.CompareTag("Player"))
            {
                Players.Add(combatant);
            }
            else
            {
                Enemies.Add(combatant);
            }
        }
    }

    public void AddPlayer(GameObject player)
    {
        Players.Add(player);
    }

    public void AddCombatant(GameObject combatant)
    {
        Combatants.Add(combatant);
    }
}
