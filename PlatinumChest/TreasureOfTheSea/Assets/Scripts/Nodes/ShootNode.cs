using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ShootNode : Node
{
    private NavMeshAgent agent;
    private EnemyAI ai;

    public ShootNode(NavMeshAgent agent, EnemyAI ai)
    {
        this.agent = agent;
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        //agent.isStopped = true;
        Debug.Log("animation of player is attacking");

        ai.animator.SetTrigger("TrigerAttacking");
        ai.animator.SetBool("IsAttacking", true);
        ai.animator.SetBool("IsWalking", false);
        return NodeState.SUCCESS;
    }

}
