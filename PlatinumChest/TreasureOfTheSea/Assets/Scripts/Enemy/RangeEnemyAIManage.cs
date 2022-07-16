using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Loot
{
    public GameObject item;
    [Range(0.01f, 100f)]
    public float dropRate;
    //public int minQuantity;
    //public int maxQuantity;
}

public class RangeEnemyAIManage : MonoBehaviour
{

    [Header("Range Enemy Settings")]
    public Loot[] loots;
    public int HP = 100;
    public Animator animator;
    public bool isInBossIsland;
    

    public GameObject projectile;
    public Transform projectilePoint; // have to change from enemy transform to weapon transform.

    [SerializeField] Transform[] wayPoints;

    private int enemyHP;

    private void Start()
    {
        enemyHP = HP;
    }

    public Transform[] GetWayPoints()
    {
        return wayPoints;   
    }

    public void TakeDamage(int damageAmount)
    {
        enemyHP -= damageAmount;

        if(enemyHP <= 0)
        {
            
            animator.SetTrigger("Death");

            if(isInBossIsland == true)
            {
                if(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().CheckPlayerHasEverySkull())
                {
                    //die
                    GetComponent<CapsuleCollider>().enabled = false;
                    StartCoroutine(DelayedDead(animator.GetCurrentAnimatorStateInfo(0).length));

                    GameObject.FindGameObjectWithTag("TreasureBox").GetComponent<TreasureBox>().KillZombieEnemy();
                    isInBossIsland = false;
                    return;
                }

                //rebirth
                StartCoroutine(DelayedAnimation(animator.GetCurrentAnimatorStateInfo(0).length));
                animator.SetTrigger("Rebirth");
                enemyHP = HP;

            }
            else
            {
                //die
                GetComponent<CapsuleCollider>().enabled = false;
                StartCoroutine(DelayedDead(animator.GetCurrentAnimatorStateInfo(0).length));
            }
            
        }
        else
        {
            animator.SetTrigger("Damage");
        }
    }

    public void Shoot()
    {
        Rigidbody rb =  Instantiate(projectile, projectilePoint.position, Quaternion.identity).GetComponent<Rigidbody>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 hit_dir = player.transform.position - transform.position;
        hit_dir = Vector3.Normalize(hit_dir);

        rb.AddForce(hit_dir * 25f, ForceMode.Impulse);
        rb.AddForce(transform.up * 5f, ForceMode.Impulse);


        //rb.AddForce(transform.forward * 30f, ForceMode.Impulse);
        //rb.AddForce(transform.up * 5f, ForceMode.Impulse);


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

        foreach(Loot loot in loots)
        {
            float spawnPercentage = Random.Range(-0.01f, 100f);

            if(spawnPercentage <= loot.dropRate)
            {
                dropSuccess = true;
                GameObject item = Instantiate(loot.item, transform.position, Quaternion.identity);
                
                break;
            }
        }

        if(dropSuccess == false)
        {
            Instantiate(loots[0].item, transform.position, Quaternion.identity);
        }

    }

}
