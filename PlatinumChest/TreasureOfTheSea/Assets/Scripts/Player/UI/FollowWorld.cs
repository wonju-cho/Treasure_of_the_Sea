using UnityEngine;

public class FollowWorld : MonoBehaviour
{
    private Camera mainCamera;
    public Transform TargetPosition;
    [SerializeField] private Vector3 offset;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;

        if (!mainCamera)
            Debug.Log("There is no camera in the playermanager script");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = mainCamera.WorldToScreenPoint(TargetPosition.position + offset);

        if (transform.position != pos)
            transform.position = pos;
    }
}
