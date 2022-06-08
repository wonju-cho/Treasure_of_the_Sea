using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManage : MonoBehaviour
{
    public Loot[] loots;

    public int HP = 100;
    public Animator animator;

    [SerializeField] Transform[] wayPoints;

    public Transform[] GetWayPoints()
    {
        return wayPoints;
    }

    //Add this function to player's slingshot
    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;
        
        if(HP <= 0)
        {
            //Play Death Animation
            animator.SetTrigger("Death");
            StartCoroutine(DelayedDead(animator.GetCurrentAnimatorStateInfo(0).length));
        }
        else
        {
            //Play Damage Animation
            animator.SetTrigger("Damage");
        }
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
