using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    [SerializeField] private float lowHealthThreshold;
    [SerializeField] private float healthRestoreRate;

    [SerializeField] private float chasingRange;
    [SerializeField] private float shootingRange;
    
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Cover[] avaliableCovers;
    [SerializeField] private int enemyType; // 0 melee, 1 range, 2 boss
    [SerializeField] public Transform[] wayPoints;

    //[SerializeField] public WeightedRandom<Transform> randomItemTable;
    [SerializeField] public List<Transform> randomItemTable;
   
    public int wayPointIndex = 0;
    public float wanderSpeed = 4f;
    public float chaseSpeed = 7f;

    private Material material;
    private Transform bestCoverSpot;
    private NavMeshAgent agent;
    public Animator animator;

    private Node topNode;
    private float _currentHealth;

    public float CurrentHealth
    {
        get { return _currentHealth; }
        set { _currentHealth = Mathf.Clamp(value, 0, startingHealth); }
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        material = GetComponentInChildren<MeshRenderer>().material;
    }

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        _currentHealth = startingHealth;
        ConstructBehaviorTree();
    }

    private void ConstructBehaviorTree()
    {
        WanderNode wanderNode = new WanderNode(transform, agent, this, WanderNode.WanderType.WayPoint);
        IsCoveredAvaliableNode coveredAvaliableNode = new IsCoveredAvaliableNode(avaliableCovers, playerTransform, this);
        GoToCoverNode gotoCoverNode = new GoToCoverNode(agent, this);
        HealthNode healthNode = new HealthNode(this, lowHealthThreshold);
        IsCoveredNode isCoveredNode = new IsCoveredNode(playerTransform, transform);
        ChaseNode chaseNode = new ChaseNode(playerTransform, agent, this);
        RangeNode chasingRangeNode = new RangeNode(chasingRange, playerTransform, transform);
        ShootNode shootNode = new ShootNode(agent, this);
        RangeNode shootingRangeNode = new RangeNode(shootingRange, playerTransform, transform);

        Sequence wanderSequence = new Sequence(new List<Node> { wanderNode });
        Sequence chaseSequence = new Sequence(new List<Node> { chasingRangeNode, chaseNode });
        Sequence shootSequence = new Sequence(new List<Node> { shootingRangeNode, shootNode });
        Sequence goToCoverSequence = new Sequence(new List<Node> { coveredAvaliableNode, gotoCoverNode });

        Selector findCoverSelector = new Selector(new List<Node> { goToCoverSequence, chaseSequence });
        Selector tryToTakeCoverSelector = new Selector(new List<Node> { isCoveredNode, findCoverSelector });

        Sequence mainCoverSequence = new Sequence(new List<Node> { healthNode, tryToTakeCoverSelector });

        topNode = new Selector(new List<Node> { mainCoverSequence, shootSequence, chaseSequence });
    }

    private void Update()
    {
        topNode.Evaluate();

        if(topNode.nodeState == NodeState.FAILURE)
        {
            //SetColor(Color.red);
            agent.isStopped = true;
        }

        if(_currentHealth <= 0)
        {
            Destroy(gameObject);

        }
        //_currentHealth += Time.deltaTime * healthRestoreRate;
    }

    private void OnMouseDown()
    {
        CurrentHealth -= 10f;
    }

    public void SetColor(Color c)
    {
        material.color = c;
    }

    public void SetBestCoverSpot(Transform bestCoverSpot)
    {
        this.bestCoverSpot = bestCoverSpot;
    }

    public Transform GetBestCoverSpot()
    {
        return bestCoverSpot;
    }

    public Transform GetWayPointsTransform(int index)
    {
        return wayPoints[index];
    }
    
    public void SetWayPointIndex(int index)
    {
        if(index == wayPoints.Length -1)
        {
            index = 0;
        }
        wayPointIndex = index;
    }

    public int GetWayPointIndex()
    {
        return wayPointIndex;
    }

}
