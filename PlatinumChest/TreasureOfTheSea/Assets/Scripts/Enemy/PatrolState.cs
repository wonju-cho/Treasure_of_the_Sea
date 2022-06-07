using UnityEngine.AI;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : StateMachineBehaviour
{
    float timer;
    //List<Transform> wayPoints = new List<Transform>();
    NavMeshAgent agent;
    Transform player;
    [SerializeField] float chaseRange;
    [SerializeField] float patrolSpeed;
    Transform[] wayPoints;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        agent = animator.GetComponent<NavMeshAgent>();
        agent.speed = patrolSpeed;

        timer = 0;
        //GameObject points = GameObject.FindGameObjectWithTag("WayPoints");
        
        //foreach(Transform t in points.transform)
        //{
        //    wayPoints.Add(t);
        //}

        //ai goes to random way points.
        if(animator.gameObject.tag == "MeleeEnemy")
        {
            EnemyManage melee = animator.gameObject.GetComponent<EnemyManage>();
            wayPoints = melee.GetWayPoints();
        }
        else if(animator.gameObject.tag == "RangeEnemy")
        {
            RangeEnemyAIManage range = animator.gameObject.GetComponent<RangeEnemyAIManage>();
            wayPoints = range.GetWayPoints();
        }
       
        agent.SetDestination(wayPoints[Random.Range(0, wayPoints.Length)].position);
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(wayPoints[Random.Range(0, wayPoints.Length)].position);
        }

        //update patrolling
        timer += Time.deltaTime;
        if (timer > 10f)
        {
            animator.SetBool("isPatrolling", false);
        }

        //chase state
        float distance = Vector3.Distance(player.position, animator.transform.position);
        if (distance < chaseRange)
        {
            animator.SetBool("isChasing", true);
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
