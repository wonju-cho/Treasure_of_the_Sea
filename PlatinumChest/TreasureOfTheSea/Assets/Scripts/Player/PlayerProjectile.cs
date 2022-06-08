using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    //public GameObject impactEffect;
    public float radius = 1;
    public int damageAmount = 15;


    private void Update()
    {
        Destroy(this.gameObject, 5f);

    }

    private void OnCollisionEnter(Collision collision)
    {
        //GameObject impact = Instantiate(impactEffect, transform.position, Quaternion.identity);
        //Destroy(impact, 2);
        if (collision.collider.tag == "MeleeEnemy")
        {
            collision.collider.GetComponent<EnemyManage>().TakeDamage(20); 
            Destroy(this.gameObject);
        }
        else if(collision.collider.tag == "RangeEnemy")
        {
            collision.collider.GetComponent<RangeEnemyAIManage>().TakeDamage(30);
            Destroy(this.gameObject);
        }
        else if(collision.collider.tag == "Plane")
        {
            Destroy(this.gameObject);
        }
        else if(collision.collider.tag == "InteractableObject")
        {
            collision.collider.GetComponent<InteractObject>().TakeDamage();
            Destroy(this.gameObject);
        }

    }
}
