using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float radius;
    [Range(0, 360)]
    public float angle;

    private NavMeshAgent agent;
    private Animator animator;

    private GameManager gameManager;
    public GameObject gameOverScreen;
    public GameObject playerRef;
    public GameObject patrolPath;
    public float followSpeed;
    public float patrolSpeed;
    public Transform[] waypoints;
    private int currentWaypointIndex = 0;

    public LayerMask obstructionMask;

    public bool canSeePlayer;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        gameManager = GameObject.FindFirstObjectByType<GameManager>();
        if (playerRef == null)
        {
            playerRef = GameObject.FindGameObjectWithTag("Player");
        }
        PopulateWaypoints();
        StartCoroutine(FOVRoutine());
    }

    private void PopulateWaypoints()
    {
        waypoints = new Transform[patrolPath.transform.childCount];
        for (int i = 0; i < patrolPath.transform.childCount; i++)
        {
            waypoints[i] = patrolPath.transform.GetChild(i);
        }
    }

    private void Update()
    {
        if (canSeePlayer)
        {
            FollowPlayer();
        }
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        agent.speed = patrolSpeed;

        if (waypoints.Length == 0) return;

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            agent.destination = waypoints[currentWaypointIndex].position;
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }

        animator.SetBool("isChasing", false);

    }

    private void FollowPlayer()
    {
        agent.speed = followSpeed;

        agent.destination = playerRef.transform.position;

        animator.SetBool("isChasing", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerRef)
        {
            gameManager.GameOver();
        }
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }
    private void FieldOfViewCheck()
    {
        Vector3 directionToPlayer = (playerRef.transform.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, playerRef.transform.position);

        if (distanceToPlayer < radius)
        {
            if (Vector3.Angle(transform.forward, directionToPlayer) < angle / 2)
            {
                if (!Physics.Raycast(transform.position, directionToPlayer, distanceToPlayer, obstructionMask))
                    canSeePlayer = true;
                else
                    canSeePlayer = false;
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }

}