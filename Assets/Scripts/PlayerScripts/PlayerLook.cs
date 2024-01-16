using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private Transform playerCamera;
    [SerializeField] private float sensitivityX = 8f;
    [SerializeField] private float sensitivityY = 0.5f;
    private float xClamp = 85f;

    private float xRotation = 0f;
    private float lookX;
    private float lookY;

    private void Start()
    {
        //Confine and hide cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        transform.Rotate(Vector3.up, lookX * Time.deltaTime);

        xRotation -= lookY;
        xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp);

        Vector3 targetRotation = transform.eulerAngles;
        targetRotation.x = xRotation;
        playerCamera.eulerAngles = targetRotation;
    }
    public void ReceiveInput(Vector2 cameraInput)
    {
        lookX = cameraInput.x * sensitivityX;
        lookY = cameraInput.y * sensitivityY;
    }
}
