using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class CheatCode : MonoBehaviour
{
    private InventoryHolder ih;

    [Header("F2 Cheat code Settings")]
    public GameObject[] F2_melees;
    public GameObject[] F2_ranges;
    public GameObject[] F2_bridges;
    public Transform F2_playerPosition;

    [Header("F3 Cheat code Settings")]
    public GameObject[] F3_melees;
    public GameObject[] F3_ranges;
    public GameObject F3_bridge;
    public Transform F3_playerPosition;

    [Header("F4 Cheat code Settings")]
    public GameObject[] F4_melees;
    public GameObject[] F4_ranges;
    public GameObject F4_bridge;
    public GameObject player;
    public Transform F4_playerPosition;

    [Header("F8 Cheat code Settings")]
    public InventoryItemData plank;
    public InventoryItemData screw;

    private bool already_press_f2 = false;
    private bool already_press_f3 = false;
    private bool already_press_f4 = false;
    private bool already_press_f5 = false;

    public Crafting[] craftings;

    // Start is called before the first frame update
    void Start()
    {
        ih = GameObject.FindWithTag("Player").GetComponent<InventoryHolder>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            if (already_press_f2 == false)
            {
                CheatCodeF2();
                already_press_f2 = true;
            }

            DisableCraftingSignifier();

            gameObject.GetComponent<PlayerManager>().PlayerResapwn(F2_playerPosition);
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            if (already_press_f2 == false)
            {
                CheatCodeF2();
                already_press_f2 = true;
            }

            if (already_press_f3 == false)
            {
                CheatCodeF3();
                already_press_f3 = true;
            }

            DisableCraftingSignifier();

            gameObject.GetComponent<PlayerManager>().PlayerResapwn(F3_playerPosition);
        }

        if (Input.GetKeyDown(KeyCode.F4))
        {
            if (already_press_f2 == false)
            {
                CheatCodeF2();
                already_press_f2 = true;
            }

            if (already_press_f3 == false)
            {
                CheatCodeF3();
                already_press_f3 = true;
            }

            if (already_press_f4 == false)
            {
                CheatCodeF4();
                already_press_f4 = true;
                already_press_f5 = true;
            }

            DisableCraftingSignifier();

            gameObject.GetComponent<PlayerManager>().PlayerResapwn(F4_playerPosition);
            GameObject.FindGameObjectWithTag("TreasureBox").GetComponent<TreasureBox>().KillEveryZombie();
        }

        if (Input.GetKeyDown(KeyCode.F5))
        {
            if (already_press_f2 == false)
            {
                CheatCodeF2();
                already_press_f2 = true;
            }

            if (already_press_f3 == false)
            {
                CheatCodeF3();
                already_press_f3 = true;
            }

            if (already_press_f5 == false)
            {
                CheatCodeF5();
                already_press_f4 = true;
                already_press_f5 = true;
            }

            DisableCraftingSignifier();

            gameObject.GetComponent<PlayerManager>().PlayerResapwn(F4_playerPosition);
            GameObject.FindGameObjectWithTag("TreasureBox").GetComponent<TreasureBox>().KillEveryZombie();
        }

        //test
        if (Input.GetKeyDown(KeyCode.F8))
        {
            ih.InventorySystem.AddToInventory(plank, 50);
            ih.InventorySystem.AddToInventory(screw, 50);
        }

    }

    void DisableCraftingSignifier()
    {
        for (int i = 0; i < craftings.Length; i++)
        {
            craftings[i].SetIsNearTheCrafting(false);
        }
    }

    void CheatCodeF2()
    {
        if (F2_melees != null)
        {
            foreach (GameObject melee in F2_melees)
            {
                Destroy(melee);
                //melee.GetComponent<EnemyManage>().TakeDamage(200);
            }

        }
        if (F2_ranges != null)
        {
            foreach (GameObject range in F2_ranges)
            {
                Destroy(range);
                //range.GetComponent<RangeEnemyAIManage>().TakeDamage(200);
            }
        }

        if (F2_bridges != null)
        {
            foreach (GameObject bridge in F2_bridges)
            {
                bridge.GetComponent<Quest>().CheatCode();
                bridge.GetComponentInChildren<QuestTrigger>().CheatCode();
            }
        }
    }

    void CheatCodeF3()
    {
        if (F3_melees != null)
        {
            foreach (GameObject melee in F3_melees)
            {
                Destroy(melee);
                //melee.GetComponent<EnemyManage>().TakeDamage(200);
            }

        }
        if (F3_ranges != null)
        {
            foreach (GameObject range in F3_ranges)
            {

                Destroy(range);
                //range.GetComponent<RangeEnemyAIManage>().TakeDamage(200);
            }
        }

        if (F3_bridge != null)
        {
            F3_bridge.GetComponent<Quest>().CheatCode();
            F3_bridge.GetComponentInChildren<QuestTrigger>().CheatCode();
        }
    }

    void CheatCodeF4()
    {
        if (player != null)
        {
            player.GetComponent<PlayerManager>().CheatCodeForBossIsland();
        }

        if (F4_bridge != null)
        {
            F4_bridge.GetComponent<Quest>().CheatCode();
            F4_bridge.GetComponentInChildren<QuestTrigger>().CheatCode();
        }

        if (F4_ranges != null)
        {
            foreach (GameObject range in F4_ranges)
            {
                Destroy(range);
                //range.GetComponent<RangeEnemyAIManage>().MustKillEnemyForCheatCode();
            }
        }

        if (F4_melees != null)
        {
            foreach (GameObject melee in F4_melees)
            {
                Destroy(melee);
                //melee.GetComponent<EnemyManage>().MustKillEnemyForCheatCode();
            }
        }
    }

    void CheatCodeF5()
    {
        //kill all enemies;
        //if (player != null)
        //    player.GetComponent<PlayerManager>().CheatCodeForBossIslandF5();


        if (F4_melees != null)
        {
            foreach (GameObject melee in F4_melees)
            {
                Destroy(melee);
            }
        }

        if (F4_ranges != null)
        {
            foreach (GameObject range in F4_ranges)
            {
                Destroy(range);
            }
        }

        if (F4_bridge != null)
        {
            F4_bridge.GetComponent<Quest>().CheatCode();
            F4_bridge.GetComponentInChildren<QuestTrigger>().CheatCode();
        }
    }
}
