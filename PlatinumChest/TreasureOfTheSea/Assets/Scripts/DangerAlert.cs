using UnityEngine;
using TMPro;

public class DangerAlert : MonoBehaviour
{
    public AudioSource audioSource;
    public TextMeshProUGUI alertText0;
    public TextMeshProUGUI alertText1;
    float timer = 0f;
    float aTime = 5f;
    private bool start = false;
    public Animation anim;
    private bool once = false;

    // Start is called before the first frame update
    void Start()
    {
        alertText0.enabled = false;
        alertText1.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(start)
        {
            if(!once)
            {
                audioSource.Play();
                once = true;
            }
            alertText0.enabled = true;
            alertText1.enabled = true;
            anim.Play();
            timer += Time.deltaTime;
            if (timer > aTime)
            {
                audioSource.Stop();
                anim.Stop();
            }
        }    
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            start = true;
        }
    }
}
