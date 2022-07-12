using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChest : MonoBehaviour
{
    [Header("Chest Settings")]
    public int HP = 1;
    public GameObject topChest;
    public Transform openTransform;
    public float openTransformRotation;
    public float openSpeed;
    public Vector3 axisRotation;
    public Animator chestAnimator;

    [Header("Skull Settings")]
    public GameObject skullObject;
    public Transform skull;
    public Vector3 originalPos;
    public Vector3 upPos;
    public float upSpeed;

    [Header("Particle Settings")]
    public Transform particleTransform;
    public GameObject particleObject;
    public float destroyTime;

    bool isOpen = false;
    bool skullGoesUp = true;
    bool isNearPlayer = false;

    Vector3 skullPosition;
    float rotation_x = 0;
    bool startOnParticle = false;

    
    BoxCollider bx;


    // Start is called before the first frame update
    void Start()
    {
        rotation_x = 0;
        skullPosition = skull.position;
        bx = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(skullObject)
        {
            if (isOpen == true)
            {
                if (rotation_x > openTransformRotation)
                {
                    Debug.Log("is open");
                    topChest.transform.Rotate(axisRotation, openSpeed);
                    rotation_x += openSpeed;
                }
                else
                {
                    if (skullGoesUp)
                    {
                        if (upPos.y <= skull.position.y)
                        {
                            skullGoesUp = false;
                            return;
                        }

                        if(startOnParticle == false)
                        {
                            GameObject particle = Instantiate(particleObject, particleTransform.position, Quaternion.identity);
                            Destroy(particle, destroyTime);
                            startOnParticle = true;
                        }
                        skullPosition.y += upSpeed;
                        skull.position = skullPosition;
                    }
                    else
                    {
                        /*
                        if(originalPos.y >= skull.position.y)
                        {
                            skullGoesUp = true;
                            return;
                        }

                        skullPosition.y -= upSpeed;
                        skull.position = skullPosition;*/
                    }

                    if (isNearPlayer == true)
                    {
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().GetSkull();
                            Destroy(skullObject);
                        }
                    }
                }
            }
        
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isNearPlayer = false;
        }
    }

    public void OpenChest()
    {
        isOpen = true;
        bx.isTrigger = true;
        chestAnimator.SetTrigger("open");
    }

    public bool GetIsOpenChest()
    {
        return isOpen;
    }
}
