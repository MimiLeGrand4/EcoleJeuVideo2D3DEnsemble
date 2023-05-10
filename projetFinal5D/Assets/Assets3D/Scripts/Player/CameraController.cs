using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject firstPersonCamera;
    public GameObject thirdPersonCamera;
    public Transform player;
    public KeyCode switchKey = KeyCode.C;
    public float distanceFromPlayer = 3.0f;

    private bool isFirstPerson = true;
    public float rotationSpeed = 3.0f;

    void Start()
    {
        firstPersonCamera.SetActive(true);
        thirdPersonCamera.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(switchKey))
        {
            isFirstPerson = !isFirstPerson;
            firstPersonCamera.SetActive(isFirstPerson);
            thirdPersonCamera.SetActive(!isFirstPerson);
        }
        if (isFirstPerson)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            thirdPersonCamera.transform.position = player.position - player.forward * distanceFromPlayer;
            thirdPersonCamera.transform.LookAt(player);

            float horizontal = Input.GetAxis("Mouse X") * rotationSpeed;
            player.Rotate(0, horizontal, 0);
        }

        
    }
    
}
