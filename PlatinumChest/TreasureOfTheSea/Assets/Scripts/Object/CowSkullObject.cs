using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowSkullObject : MonoBehaviour
{
    [Header("Ower Chest")]
    public GameObject chest;
    //public BoxCollider bx_chest;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.collider.tag == "Player")
    //    {
    //        if(chest.GetComponent<TriggerChest>().GetIsOpenChest() == true)
    //        {
    //            collision.collider.GetComponent<PlayerManager>().GetSkull();
    //            Destroy(this.gameObject);
    //        }
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(chest.GetComponent<TriggerChest>().GetIsOpenChest() == true)
            {
                Debug.Log("collision with player");
                other.GetComponent<PlayerManager>().GetSkull();
                Destroy(this.gameObject);
            }
        }
    }
}
