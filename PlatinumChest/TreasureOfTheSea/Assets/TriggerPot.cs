using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPot : MonoBehaviour
{
    [Header("Pot Settings")]
    public int HP = 1;

    BoxCollider bx;
    PlayerManager player;
    
    // Start is called before the first frame update
    void Start()
    {
        bx = GetComponent<BoxCollider>();
        //player = GameObject.FindGameObjectWithTag("Player").
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyPot()
    {
        Destroy(this.gameObject);

    }

}
