using UnityEngine;
public class ItemPickUp : MonoBehaviour
{
    public float PickUpRadius = 1f;
    public InventoryItemData itemData;

    private CapsuleCollider myCollider;
    private Rigidbody rigidBody;
    PlayerManager pm;

    private void Awake()
    {
        myCollider = GetComponent<CapsuleCollider>();
        rigidBody = GetComponent<Rigidbody>();
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();

        if (!pm)
            Debug.Log("there is no pm");

        myCollider.isTrigger = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(this.name != "Fruit(Clone)")
            {
                var inventory = other.transform.GetComponent<InventoryHolder>();

                if (!inventory)
                    return;

                if (inventory.InventorySystem.AddToInventory(itemData, 1))
                {
                    Destroy(this.gameObject);
                }
            }
            else
            {
                pm.HealingHP(20);
                Destroy(this.gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("detect collision item with " + collision.gameObject.name);
        Collider collider = collision.collider;
        if (collider.tag == "Player")
        {
            if (this.name != "Fruit(Clone)")
            {
                InventoryHolder inventory = collision.collider.transform.GetComponent<InventoryHolder>();

                if (!inventory)
                {
                    return;
                }

                if (inventory.InventorySystem.AddToInventory(itemData, 1))
                {
                    Destroy(this.gameObject);
                }

            }
            else
            {
                pm.HealingHP(20);
                Destroy(this.gameObject);

            }
        }
        else if (collider.tag == "Plane")
        {
            Destroy(gameObject.GetComponent<Rigidbody>());
            myCollider.isTrigger = true;
        }
    }

    
}
