using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject : MonoBehaviour
{
    public string name;
    public int hitCount;
    public Loot[] loots;

    public Transform dropItemPosition;

    [Header("Particle Settings")]
    public Transform particlePosition;
    public GameObject particleObject;
    public float destroyTime;
    
    public void TakeDamage()
    {
        --hitCount;

        if(hitCount <= 0)
        {
            //add visual effect

            //destroy game object
            Destroy(gameObject);

            //drop item
            //DropItemWhenPlayerHit();
            DropItem();
        }
        else
        {
            //add visual effect
            //DropItemWhenObjectDestroy();
            DropItem();
        }
    }

    public void DropItemWhenPlayerHit()
    {
        Instantiate(loots[0].item, transform.position, Quaternion.identity);
    }

    public void DropItemWhenObjectDestroy()
    {
        Instantiate(loots[1].item, transform.position, Quaternion.identity);
    }

    public void DropItem()
    {
        bool dropSuccess = false;
        Vector3 way_drop_item;

        foreach(Loot loot in loots)
        {
            float spawnPercentage = Random.Range(-0.01f, 100f);

            if(spawnPercentage <= loot.dropRate)
            {
                dropSuccess = true;
                GameObject item = Instantiate(loot.item, dropItemPosition.position, Quaternion.identity);
                Rigidbody rb_item = item.GetComponent<Rigidbody>();

                //rb_item.AddForce(dropItemPosition.up * 5f, ForceMode.VelocityChange);
                way_drop_item = Quaternion.Euler(0, Random.Range(0, 360), 0) * dropItemPosition.forward;
                rb_item.AddForce(way_drop_item * 5f, ForceMode.VelocityChange);

                if(particleObject)
                {
                    GameObject particle = Instantiate(particleObject, particlePosition.position, Quaternion.identity);
                    Destroy(particle, destroyTime);
                }
                

                break;
            }

        }

        if(dropSuccess == false)
        {
            GameObject item = Instantiate(loots[0].item, dropItemPosition.position, Quaternion.identity);
            Rigidbody rb_item = item.GetComponent<Rigidbody>();
            way_drop_item = Quaternion.Euler(0, Random.Range(0, 360), 0) * dropItemPosition.forward;
            rb_item.AddForce(way_drop_item * 5f, ForceMode.VelocityChange);
        }
    }

}
