using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemyAIManage : MonoBehaviour
{
    public int HP = 100;
    public Animator animator;

    public GameObject projectile;
    public Transform projectilePoint; // have to change from enemy transform to weapon transform.

    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;

        if(HP <= 0)
        {
            animator.SetTrigger("Death");
            GetComponent<CapsuleCollider>().enabled = false;
            StartCoroutine(DelayedDead(animator.GetCurrentAnimatorStateInfo(0).length));
        }
        else
        {
            animator.SetTrigger("Damage");
        }
    }

    public void Shoot()
    {
        Rigidbody rb =  Instantiate(projectile, projectilePoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 30f, ForceMode.Impulse);
        rb.AddForce(transform.up * 7f, ForceMode.Impulse);

    }

    IEnumerator DelayedDead(float delay = 0)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
