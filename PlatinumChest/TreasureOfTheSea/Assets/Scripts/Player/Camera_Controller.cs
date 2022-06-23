using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Camera_Controller : MonoBehaviour
{
    [System.Serializable]
    public class CameraSettings
    {
        [Header("Camera Move Settings")]
        public float zoomSpeed = 5;
        public float moveSpeed = 5;
        public float rotationSpeed = 5;
        public float originalFieldOfView = 70;
        public float zoomFieldOfView = 20;
        public float MouseX_Sensitivity = 5;
        public float MouseY_Sensitivity = 5f;
        public float MaxClampAngle = 90;
        public float MinClampAngle = -30;


        [Header("Camera Collision")]
        public Transform camPosition;
        public LayerMask camCollisionLayer;
    }

    [System.Serializable]
    public class CameraInputSettings
    {
        public string mouseX = "Mouse X";
        public string mouseY = "Mouse Y";
        public string AimingInput = "Fire1";
    }

    [SerializeField]
    public CameraSettings cameraSettings;
    [SerializeField]
    public CameraInputSettings inputSettings;

    Transform center;
    Transform target;

    float cameraX_rotation = 0;
    float cameraY_rotation = 0;

    Vector3 InitialCamPos;
    RaycastHit hit;

    Camera mainCam;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        center = transform.GetChild(0);
        InitialCamPos = mainCam.transform.localPosition;
        FindPlayer();

    }

    void Update()
    {
        if(!target)
        {
            return;
        }

        // for blocking the crash
        if (!Application.isPlaying)
            return;

        RotateCamera();
        ZoomCamera();
        HandleCamCollision();
    }

    private void LateUpdate()
    {
        if (!target)
        {
            FindPlayer();
        }
        else
            FollowPlayer();

    }

    void FindPlayer()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FollowPlayer()
    {
        Vector3 moveVector = Vector3.Lerp(transform.position, target.transform.position, cameraSettings.moveSpeed * Time.deltaTime);
        transform.position = moveVector;

    }

    void RotateCamera()
    {
        //mouse x -> rotating along y axis
        //mouse y => rotating along x axis
        //so have to get each of rotation from different axis,, Tlqkf,,

        cameraX_rotation += Input.GetAxis(inputSettings.mouseY) * cameraSettings.MouseY_Sensitivity;
        cameraY_rotation += Input.GetAxis(inputSettings.mouseX) * cameraSettings.MouseX_Sensitivity;

        //for not overlapping the rotation value
        //x -> À§ ¾Æ·¡
        //y -> can rotate 360, want to see backside of player
        cameraX_rotation = Mathf.Clamp(cameraX_rotation, cameraSettings.MinClampAngle, cameraSettings.MaxClampAngle);
        cameraY_rotation = Mathf.Repeat(cameraY_rotation, 360);

        //Vector3 rotatingAngle = new Vector3(0, cameraY_rotation, 0);
        Vector3 rotatingAngle = new Vector3(cameraX_rotation, cameraY_rotation, 0);
        Quaternion rotation = Quaternion.Slerp(center.transform.localRotation, Quaternion.Euler(rotatingAngle), cameraSettings.rotationSpeed * Time.deltaTime);
        center.transform.localRotation = rotation;
    }


    void ZoomCamera()
    {
        if(Input.GetButton(inputSettings.AimingInput))
        {
            Debug.Log("zoom in");
            mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, cameraSettings.zoomFieldOfView, cameraSettings.zoomSpeed * Time.deltaTime);

        }
        else
        {
            mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, cameraSettings.originalFieldOfView, cameraSettings.zoomSpeed * Time.deltaTime);
        }
    }

    void HandleCamCollision()
    {
        if (!Application.isPlaying)
            return;


        if(Physics.Linecast(target.transform.position + target.transform.up, cameraSettings.camPosition.position, out hit, cameraSettings.camCollisionLayer))
        {
            Vector3 newCamPos = new Vector3(hit.point.x + hit.normal.x * 0.2f, hit.point.y + hit.normal.y * 0.8f, hit.point.z + hit.normal.z * 0.2f);
            mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, newCamPos, Time.deltaTime * cameraSettings.moveSpeed);

        }
        else
        {
            mainCam.transform.localPosition = Vector3.Lerp(mainCam.transform.localPosition, InitialCamPos, Time.deltaTime * cameraSettings.moveSpeed);
        }

    }
}
