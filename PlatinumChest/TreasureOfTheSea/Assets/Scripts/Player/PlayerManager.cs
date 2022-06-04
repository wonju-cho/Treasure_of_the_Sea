using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//death, combat 

public class PlayerManager : MonoBehaviour
{
    public Animator animator;
    private CharacterController controller;
    private PlayerController PC;

    private bool isPlayerDead = false;
    private bool isPlayerDeadSea = false;
    private bool SeaTriggerOnce = false;

    Vector3 playerSpawnPosition;

    [SerializeField]
    int PlayerHP = 10;
    private int currentHP;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = PlayerHP;

        controller = GetComponent<CharacterController>();
        PC = GetComponent<PlayerController>();

        if (!controller)
            Debug.Log("There is no controller in the PlayerManager script");

        playerSpawnPosition = transform.position;
        currentHP = PlayerHP;

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(currentHP);

        if (Input.GetKeyDown(KeyCode.F1) && isPlayerDead)
        {
            PlayerRespawn();
            //GameObject ScenePlayer = GameObject.FindGameObjectWithTag("Player");
            //Destroy(ScenePlayer);
            //Instantiate(Player, playerSpawnPosition, Quaternion.identity);
        }

        if (currentHP <= 0)
        {
            isPlayerDead = true;
        }

        if (isPlayerDead)
        {
            PlayerDeath();
        }

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Sea"))
        {
            isPlayerDead = true;
            isPlayerDeadSea = true;
        }
    }

    bool isPlaying(Animator anim, string stateName)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(stateName) &&
                anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            return true;
        else
            return false;
    }

    void PlayerDeath()
    {
        PC.enabled = false;
        animator.SetBool("IsIdle", false);

        if (isPlayerDeadSea && SeaTriggerOnce == false)
        {
            SeaTriggerOnce = true;
            animator.ResetTrigger("Death");
            animator.SetTrigger("Death");
            Debug.Log(isPlaying(animator, "Death"));
        }
        else if (isPlayerDead && isPlayerDeadSea == false)
        {
            animator.ResetTrigger("Death");
            animator.SetTrigger("Death");
            Debug.Log(isPlaying(animator, "Death"));
        }
    }

    IEnumerator WaitForSec(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    void PlayerRespawn()
    {
        PC.enabled = true;
        
        animator.SetBool("IsIdle", true);

        controller.enabled = false;
        controller.transform.position = playerSpawnPosition;
        controller.enabled = true;

        isPlayerDead = false;
        isPlayerDeadSea = false;
        SeaTriggerOnce = false;

        Debug.Log(isPlaying(animator, "Death"));
    }
}
