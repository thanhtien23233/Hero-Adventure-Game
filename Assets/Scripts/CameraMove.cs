using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraMove : MonoBehaviour{
    public static CameraMove Instance { get; private set; }

    public float damping = 1.5f;
    public Transform _target;
    public CinemachineVirtualCamera cinemachineVirtualCamera;
    public CinemachineConfiner2D cinemachineConfiner2d;
    public Vector2 offset = new Vector2(2f, 1f);
    private bool faceLeft;
    private int lastX;
    private float dynamicSpeed;
    private Camera _cam;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); 
            return;
        }
    }
    void Start()
    {
        transform.position = new Vector3(_target.position.x, _target.position.y, _target.position.z);
        offset = new Vector2(Mathf.Abs(offset.x), offset.y);
        FindPlayer();
        _cam = gameObject.GetComponent<Camera>();

    }
    public void SetPlayerCameraFollow()
    {
        Debug.Log("Set Camera Followed");
        cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        cinemachineVirtualCamera.Follow = PlayerController.Instance.transform;
    }
    public void AssignCameraBounds()
    {
        GameObject cameraBoundsObject = GameObject.Find("CameraBounds");
        if (cameraBoundsObject != null)
        {
            Collider2D boundsCollider = cameraBoundsObject.GetComponent<Collider2D>();
            if (boundsCollider != null)
            {
                // Assign the collider to the Cinemachine Confiner
                cinemachineConfiner2d = cinemachineVirtualCamera.GetComponent<CinemachineConfiner2D>();
                if (cinemachineConfiner2d != null)
                {
                    cinemachineConfiner2d.m_BoundingShape2D = boundsCollider;
                    Debug.Log("CameraBounds assigned successfully!");
                }
                else
                {
                    Debug.LogError("CinemachineConfiner2D component not found on the virtual camera!");
                }
            }
            else
            {
                Debug.LogError("Collider2D not found on CameraBounds object!");
            }
        }
        else
        {
            Debug.LogError("CameraBounds object not found in the scene!");
        }
    }

    public void FindPlayer()
    {
        lastX = Mathf.RoundToInt(_target.position.x);
        transform.position = new Vector3(_target.position.x + offset.x, _target.position.y + offset.y, transform.position.z);
    }

    void FixedUpdate()
    {
        if (_target)
        {
            int currentX = Mathf.RoundToInt(_target.position.x);
            if (currentX > lastX) faceLeft = false; else if (currentX < lastX) faceLeft = true;
            lastX = Mathf.RoundToInt(_target.position.x);

            Vector3 target;
            if (faceLeft)
            {
                target = new Vector3(_target.position.x - offset.x, _target.position.y + offset.y+dynamicSpeed, transform.position.z);
            }
            else
            {
                target = new Vector3(_target.position.x + offset.x, _target.position.y + offset.y+dynamicSpeed, transform.position.z);
            }
            Vector3 currentPosition = Vector3.Lerp(transform.position, target, damping * Time.deltaTime);
            transform.position = currentPosition;
        }
    }
}
