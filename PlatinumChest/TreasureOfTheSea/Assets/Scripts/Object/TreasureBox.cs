using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBox : MonoBehaviour
{
    public Animator chestAnim;
    public AudioSource sfx;

    private int numOfZombies;
    private bool hasEverySkull;

    private bool already_openned = false;

    public GameObject gameGoalUI;
    public CreditUI creditUI;
    public bool gameEnd = false;

    private Bow bow;
    private PlayerManager pm;
    private PlayerController pc;
    public Texture2D cursorTexture;

    bool once = false;

    private bool is_near_player = false;

    // Start is called before the first frame update
    void Start()
    {
        gameGoalUI.SetActive(false);
 
        bow = GameObject.FindWithTag("Bow").GetComponent<Bow>();
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        pm = GameObject.FindWithTag("Player").GetComponent<PlayerManager>();


        numOfZombies = 0;
        hasEverySkull = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().CheckPlayerHasEverySkull();

        GameObject[] melees = GameObject.FindGameObjectsWithTag("MeleeEnemy");
        GameObject[] ranges = GameObject.FindGameObjectsWithTag("RangeEnemy");

        foreach (GameObject melee in melees)
        {
            if(melee.GetComponent<EnemyManage>().isInBossIsland == true)
            {
                numOfZombies++;
            }
        }

        foreach(GameObject range in ranges)
        {
            if(range.GetComponent<RangeEnemyAIManage>().isInBossIsland == true)
            {
                numOfZombies++;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(is_near_player == true)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                hasEverySkull = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().CheckPlayerHasEverySkulls();

                if (hasEverySkull)
                {
                    if (numOfZombies == 0)
                    {
                        //open and the end of this game

                        if (already_openned == false)
                        {
                            sfx.Play();
                            chestAnim.SetTrigger("open");
                            already_openned = true;
                        }

                        gameEnd = true;
                        if (!once)
                        {
                            chestAnim.SetTrigger("open");
                            gameGoalUI.SetActive(true);

                            once = true;

                        }

                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.ForceSoftware);

                        pc.enabled = false;
                        pm.enabled = false;
                        bow.enabled = false;

                    }

                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            is_near_player = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            is_near_player = false;
        }
    }

    public void SetHasEverySkull(bool hasSkull)
    {
        hasEverySkull = hasSkull;
    }

    public void KillZombieEnemy()
    {
        --numOfZombies;
    }

    public void KillEveryZombie()
    {
        numOfZombies = 0;

    }
    
}
