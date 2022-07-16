using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManage : MonoBehaviour
{
    [Header("Melee Enemy Settings")]
    public Loot[] loots;
    public int HP = 100;
    public Animator animator;
    public bool isInBossIsland;

    [SerializeField] Transform[] wayPoints;

    private int enemyHP;

    private void Start()
    {
        enemyHP = HP;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("melee enemy collision: " + collision.collider.name);
    }

    public Transform[] GetWayPoints()
    {
        return wayPoints;
    }

    //Add this function to player's slingshot
    public void TakeDamage(int damageAmount)
    {
        enemyHP -= damageAmount;
        
        if(enemyHP <= 0)
        {
            //Play Death Animation

            animator.SetTrigger("Death");
            
            if(isInBossIsland == true)
            {
                if(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().CheckPlayerHasEverySkull())
                {
                    //die
                    StartCoroutine(DelayedDead(animator.GetCurrentAnimatorStateInfo(0).length));
                    isInBossIsland = false;
                    GameObject.FindGameObjectWithTag("TreasureBox").GetComponent<TreasureBox>().KillZombieEnemy();
                    return;
                }

                StartCoroutine(DelayedAnimation(animator.GetCurrentAnimatorStateInfo(0).length));
                animator.SetTrigger("Rebirth");
                enemyHP = HP;
            }
            else
            {
                //die
                StartCoroutine(DelayedDead(animator.GetCurrentAnimatorStateInfo(0).length));
            }
        }
        else
        {
            //Play Damage Animation
            animator.SetTrigger("Damage");
        }
    }

    IEnumerator DelayedAnimation(float delay = 0)
    {
        yield return new WaitForSeconds(delay);
    }

    IEnumerator DelayedDead(float delay = 0)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
        DropItem();
    }

    public void DropItem()
    {
        bool dropSuccess = false;

        foreach (Loot loot in loots)
        {
            float spawnPercentage = Random.Range(-0.01f, 100f);

            if (spawnPercentage <= loot.dropRate)
            {
                dropSuccess = true;
                Instantiate(loot.item, transform.position, Quaternion.identity);
                break;
            }
        }

        if(dropSuccess == false)
        {
            Instantiate(loots[0].item, transform.position, Quaternion.identity);
        }

    }
}
