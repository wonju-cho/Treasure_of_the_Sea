using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseNode : Node
{
    private Transform target;
    private NavMeshAgent agent;
    private EnemyAI ai;

    public ChaseNode(Transform target, NavMeshAgent agent, EnemyAI ai)
    {
        this.target = target;
        this.agent = agent;
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        ai.SetColor(Color.yellow);

        float distance = Vector3.Distance(target.position, agent.transform.position);
        //Debug.Log("distance between enemy and player: " + distance);
        if(distance > ai.shootingRange)
        {
            //agent.isStopped = false;
            //Debug.Log("enemy is still chasing");

            ai.animator.SetBool("IsWalking", true);
            ai.animator.SetBool("IsAttacking", false);

            agent.SetDestination(target.position);
            return NodeState.RUNNING;
        }
        else
        {
            //Debug.Log("chasing player is success");
            //agent.isStopped = true;
            return NodeState.FAILURE;
        }
    }
}
