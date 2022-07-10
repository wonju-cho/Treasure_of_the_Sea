using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleItem : MonoBehaviour
{
    public int healingAmount = 20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            collision.collider.GetComponent<PlayerManager>().HealingHP(healingAmount);
        }
    }
}
