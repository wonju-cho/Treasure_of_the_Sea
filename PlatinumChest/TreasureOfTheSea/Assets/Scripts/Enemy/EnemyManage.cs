using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManage : MonoBehaviour
{
    public Loot[] loots;

    public int HP = 100;
    public Animator animator;

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
        foreach (Loot loot in loots)
        {
            float spawnPercentage = Random.Range(-0.01f, 100f);

            if (spawnPercentage <= loot.dropRate)
            {
                Instantiate(loot.item, transform.position, Quaternion.identity);
            }
        }

    }
}
