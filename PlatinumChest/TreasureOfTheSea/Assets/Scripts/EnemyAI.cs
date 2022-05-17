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

    private Material material;
    private Transform bestCoverSpot;
    private NavMeshAgent agent;

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
        _currentHealth = startingHealth;
        ConstructBehaviorTree();
    }

    private void ConstructBehaviorTree()
    {
        IsCoveredAvaliableNode coveredAvaliableNode = new IsCoveredAvaliableNode(avaliableCovers, playerTransform, this);
        GoToCoverNode gotoCoverNode = new GoToCoverNode(agent, this);
        HealthNode healthNode = new HealthNode(this, lowHealthThreshold);
        IsCoveredNode isCoveredNode = new IsCoveredNode(playerTransform, transform);
        ChaseNode chaseNode = new ChaseNode(playerTransform, agent, this);
        RangeNode chasingRangeNode = new RangeNode(chasingRange, playerTransform, transform);
        ShootNode shootNode = new ShootNode(agent, this);
        RangeNode shootingRangeNode = new RangeNode(shootingRange, playerTransform, transform);

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
            SetColor(Color.red);
            agent.isStopped = true;
        }

        _currentHealth += Time.deltaTime * healthRestoreRate;
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
}
