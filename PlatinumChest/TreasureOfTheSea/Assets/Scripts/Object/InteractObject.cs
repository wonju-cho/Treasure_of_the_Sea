using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject : MonoBehaviour
{
    public string name;
    public int hitCount;
    public Loot[] loots;//first item for hitting, second item for destroying object
    
    public void TakeDamage()
    {
        --hitCount;

        if(hitCount <= 0)
        {
            //add visual effect

            //destroy game object
            Destroy(gameObject);

            //drop item
            DropItemWhenPlayerHit();
        }
        else
        {
            //add visual effect
            DropItemWhenObjectDestroy();
        }
    }

    public void DropItemWhenPlayerHit()
    {
        //Instantiate(loots[0].item, transform.position, Quaternion.identity);
    }

    public void DropItemWhenObjectDestroy()
    {
        //Instantiate(loots[1].item, transform.position, Quaternion.identity);
    }

}
