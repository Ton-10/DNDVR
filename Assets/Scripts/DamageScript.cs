﻿using UnityEngine;
using static Helper;

public class DamageScript : MonoBehaviour
{
    public int Attack;
    public DamageType DamageType;
    public string TargetTag;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        GameObject enemy = contact.otherCollider.gameObject;
        if (enemy.CompareTag(TargetTag))
        {
            EntityStats enemyStats = enemy.GetComponent<EntityStats>();
            enemyStats.TakeDamage(Attack, DamageType);
        }
        else
        {
            Debug.Log("Hit non target");
        }
        Destroy(gameObject);
    }
}
