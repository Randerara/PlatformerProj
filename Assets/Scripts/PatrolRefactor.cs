using UnityEditor.VersionControl;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatrolRefactor : MonoBehaviour
{
    public Stats enemyStats;
    
    public int currentPatrolPoint = 0;
    
    public GameObject player;
    
    public Transform[] patrolPoints;
    
    public RefactorEnemy refactor;
    
    [System.Serializable]
    public struct Stats
    {
        [Header("Enemy Settings")]
        [Tooltip("How fast the enemy walks (only when idle is true).")]
        public float walkSpeed;

        [Tooltip("How fast the enemy runs after the player (only when idle is false).")]
        public float chaseSpeed;

        [Tooltip("Whether the enemy is idle or not. Once the player is within distance, idle will turn false and the enemy will chase the player.")]
        public bool idle;

        [Tooltip("How close the enemy needs to be to explode")]
        public float explodeDist;

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        //start chasing if the player gets close enough
        if (other.gameObject.tag == "Player")
        {
            player = other.gameObject;
            enemyStats.idle = false;
        }
    }
    // Update is called once per frame
    internal void Patrol()
    {
        if (enemyStats.idle == true)
        {
            //Patrol Logic
            Vector3 moveToPoint = patrolPoints[currentPatrolPoint].position;
            transform.position = Vector3.MoveTowards(transform.position, moveToPoint, 
                enemyStats.walkSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, moveToPoint) < 0.01f)
            {
                currentPatrolPoint++;
                if (currentPatrolPoint > patrolPoints.Length - 1)
                {
                    currentPatrolPoint = 0;
                }
            }
        }
        else if (enemyStats.idle == false)
        {
            refactor.Chase(player.transform);
        }
    }
}
