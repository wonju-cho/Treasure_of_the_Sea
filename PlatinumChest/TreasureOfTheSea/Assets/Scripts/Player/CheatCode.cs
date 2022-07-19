using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCode : MonoBehaviour
{
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

    private bool already_press_f2 = false;
    private bool already_press_f3 = false;
    private bool already_press_f4 = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F2))
        {
            if(already_press_f2 == false)
            {
                CheatCodeF2();
                already_press_f2 = true;
            }

            gameObject.GetComponent<PlayerManager>().PlayerResapwn(F2_playerPosition);
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            if(already_press_f2 == false)
            {
                CheatCodeF2();
                already_press_f2 = true;
            }

            if (already_press_f3 == false)
            {
                CheatCodeF3();
                already_press_f3 = true;
            }

            gameObject.GetComponent<PlayerManager>().PlayerResapwn(F3_playerPosition);
        }

        if (Input.GetKeyDown(KeyCode.F4))
        {
            if(already_press_f2 == false)
            {
                CheatCodeF2();
                already_press_f2 = true;
            }

            if(already_press_f3 == false)
            {
                CheatCodeF3();
                already_press_f3 = true;
            }

            if (already_press_f4 == false)
            {
                CheatCodeF4();
                already_press_f4 = true;
            }

            gameObject.GetComponent<PlayerManager>().PlayerResapwn(F4_playerPosition);
        }
        
    }

    void CheatCodeF2()
    {
        if (F2_melees != null)
        {
            foreach (GameObject melee in F2_melees)
            {
                melee.GetComponent<EnemyManage>().TakeDamage(200);
            }

        }
        if (F2_ranges != null)
        {
            foreach (GameObject range in F2_ranges)
            {
                range.GetComponent<RangeEnemyAIManage>().TakeDamage(200);
            }
        }

        if (F2_bridges != null)
        {
            foreach (GameObject bridge in F2_bridges)
            {
                bridge.GetComponent<Quest>().CheatCode();
            }
        }
    }

    void CheatCodeF3()
    {
        if (F3_melees != null)
        {
            foreach (GameObject melee in F3_melees)
            {
                melee.GetComponent<EnemyManage>().TakeDamage(200);
            }

        }
        if (F3_ranges != null)
        {
            foreach (GameObject range in F3_ranges)
            {
                range.GetComponent<RangeEnemyAIManage>().TakeDamage(200);
            }
        }

        if (F3_bridge != null)
        {
            F3_bridge.GetComponent<Quest>().CheatCode();
        }
    }

    void CheatCodeF4()
    {
        if (player != null)
        {
            player.GetComponent<PlayerManager>().CheatCodeForBossIsland();
        }

        if (F4_melees != null)
        {
            foreach (GameObject melee in F4_melees)
            {
                melee.GetComponent<EnemyManage>().TakeDamage(200);
            }

        }
        if (F4_ranges != null)
        {
            foreach (GameObject range in F4_ranges)
            {
                range.GetComponent<RangeEnemyAIManage>().TakeDamage(200);
            }
        }

        if (F4_bridge != null)
        {
            F4_bridge.GetComponent<Quest>().CheatCode();
        }

        
    }
}
