using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderNode : Node
{
    public enum WanderType { Random, WayPoint };

    [SerializeField] private LayerMask floorMask = 0;
    [SerializeField] private float viewDistance = 10f;

    public WanderType wanderType;
    public PlayerMovement player;
    public Vector3 wanderPoint;
    public float distance = 10f;

    private Transform transform;
    private NavMeshAgent agent;
    private EnemyAI ai;
    private bool isAware = false;


    public WanderNode(Transform transform, NavMeshAgent agent, EnemyAI ai, WanderType wanderType)
    {
        this.transform = transform;
        this.agent = agent;
        this.ai = ai;
        this.wanderType = wanderType;

        isAware = false;
        wanderPoint = RandomNavSphere();
    }

    public override NodeState Evaluate()
    {
        SearchForPlayer();

        if (isAware)
        {
            //ai.animator.SetBool("Aware", true);
            agent.speed = ai.chaseSpeed;
            return NodeState.FAILURE;
        }
        else
        {
            agent.speed = ai.wanderSpeed;
            Wander();
            ai.animator.SetBool("Aware", false);
        }
        
        return NodeState.SUCCESS;
    }

    public void SearchForPlayer()
    {
        if(Vector3.Angle(Vector3.forward, transform.InverseTransformPoint(player.transform.position))< 60f)
        {
            if(Vector3.Distance(player.transform.position, transform.position) < viewDistance)
            {
                RaycastHit hit;
                if(Physics.Linecast(transform.position, player.transform.position, out hit, floorMask))
                {
                    if(hit.transform.CompareTag("Player"))
                    {
                        OnAware(true);
                    }
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
