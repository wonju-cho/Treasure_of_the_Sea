using UnityEngine;
public class ItemPickUp : MonoBehaviour
{
    public float PickUpRadius = 1f;
    public InventoryItemData itemData;

    private CapsuleCollider myCollider;
    private Rigidbody rigidBody;

    private void Awake()
    {
        myCollider = GetComponent<CapsuleCollider>();
        rigidBody = GetComponent<Rigidbody>();

        myCollider.isTrigger = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            var inventory = other.transform.GetComponent<InventoryHolder>();

            if (!inventory)
                return;

            if (inventory.InventorySystem.AddToInventory(itemData, 1))
            {
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
            InventoryHolder inventory = collision.collider.transform.GetComponent<InventoryHolder>();
            
            if(!inventory)
            {
                return;
            }

            if(inventory.InventorySystem.AddToInventory(itemData, 1))
            {
                Destroy(this.gameObject);
            }
        }
        else if(collider.tag == "Plane")
        {
            Destroy(gameObject.GetComponent<Rigidbody>());
            myCollider.isTrigger = true;
        }
    }

    
}
