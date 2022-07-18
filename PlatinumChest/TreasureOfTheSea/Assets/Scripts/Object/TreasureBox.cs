using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBox : MonoBehaviour
{
    public Animator chestAnim;

    private int numOfZombies;
    private bool hasEverySkull;

    // Start is called before the first frame update
    void Start()
    {
        numOfZombies = 0;
        hasEverySkull = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().CheckPlayerHasEverySkull();

        GameObject[] melees = GameObject.FindGameObjectsWithTag("MeleeEnemy");
        GameObject[] ranges = GameObject.FindGameObjectsWithTag("RangeEnemy");

        foreach (GameObject melee in melees)
        {
            if(melee.GetComponent<EnemyManage>().isInBossIsland == true)
            {
                numOfZombies++;
            }
        }

        foreach(GameObject range in ranges)
        {
            if(range.GetComponent<RangeEnemyAIManage>().isInBossIsland == true)
            {
                numOfZombies++;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(hasEverySkull)
        {
            if (numOfZombies == 0)
            {
                //open and the end of this game
                chestAnim.SetTrigger("open");
            }
        }
    }

    public void SetHasEverySkull(bool hasSkull)
    {
        hasEverySkull = hasSkull;
    }

    public void KillZombieEnemy()
    {
        --numOfZombies;
    }
    
}
