using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderNode : Node
{
    public enum WanderType { Random, WayPoint };

    [SerializeField] private LayerMask floorMask = 0;
    [SerializeField] private float viewDistance;

    public WanderType wanderType;
    //public PlayerMovement player;
    public Transform playerTransform;
    public Vector3 wanderPoint;
    public float distance = 10f;

    private Transform transform;
    private NavMeshAgent agent;
    private EnemyAI ai;
    private bool isAware = false;


    public WanderNode(Transform transform, NavMeshAgent agent, EnemyAI ai, WanderType wanderType, float viewDistance)
    {
        this.transform = transform;
        this.agent = agent;
        this.ai = ai;
        this.wanderType = wanderType;
        this.viewDistance = viewDistance;

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        isAware = false;
        wanderPoint = RandomNavSphere();
    }

    public override NodeState Evaluate()
    {
        SearchForPlayer();

        if (isAware)
        {
            Debug.Log("have to chase");
            //ai.animator.SetBool("Aware", true);
            agent.speed = ai.chaseSpeed;
            return NodeState.FAILURE;
        }
        else
        {
            Debug.Log("wander");
            agent.speed = ai.wanderSpeed;
            Wander();
            ai.animator.SetBool("Aware", false);
        }
        
        return NodeState.RUNNING;
    }

    public void SearchForPlayer()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.red);

        if(isAware == true)
        {
            if (Vector3.Distance(playerTransform.position, transform.position) >= viewDistance)
            {
                isAware = false;
            }
        }
        else
        {
            if (Vector3.Angle(Vector3.forward, transform.InverseTransformPoint(playerTransform.position)) < 60f)
            {
                if (Vector3.Distance(playerTransform.position, transform.position) < viewDistance)
                {
                    OnAware(true);
                    //Debug.Log("aware" + isAware);

                    //RaycastHit hit;
                    //if(Physics.Raycast(transform.position, playerTransform.position, out hit))
                    //{
                    //    Debug.Log("Enemy is Wake");
                    //    if (hit.transform.CompareTag("Player"))
                    //    {
                    //        OnAware(true);
                    //    }
                    //}
                }
            }
        }
        
    }

    public void OnAware(bool aware)
    {
        isAware = aware;
    }

    public void Wander()
    {
        if(wanderType == WanderType.Random)
        {
            if (Vector3.Distance(transform.position, wanderPoint) < 2f)
            {
                wanderPoint = RandomNavSphere();
            }
            else
            {
                agent.SetDestination(wanderPoint);
            }
        }
        else
        {
            //int index = ai.GetWayPointIndex();
            //Transform waypointTransform = ai.GetWayPointsTransform(index);
            Transform waypointTransform = ai.wayPoints[ai.wayPointIndex];

            if(Vector3.Distance(waypointTransform.position, transform.position) < 2f)
            {
                if(ai.wayPointIndex == ai.wayPoints.Length - 1)
                {
                    ai.wayPointIndex = 0;
                }
                else
                {
                    ++ai.wayPointIndex;
                }
                //ai.SetWayPointIndex(++index);
            }
            else
            {
                agent.SetDestination(waypointTransform.position);
            }
        }
    }

    public Vector3 RandomNavSphere()
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;
        randomDirection += transform.position;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, distance, floorMask);

        return new Vector3(navHit.position.x, navHit.position.y, navHit.position.z);
    }
}
