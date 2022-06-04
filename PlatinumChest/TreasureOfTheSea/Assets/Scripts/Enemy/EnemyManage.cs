using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManage : MonoBehaviour
{
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
    }
}
