using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class TestCam01 : MonoBehaviour
{
    private void Start()
    {
        //Confine and hide cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
 
    float rotX = 0f;
    float rotY = 0f;

    //General sensitivity
    [Range(0.0f, 15.0f)]
    public float Sensitivity = 5f;
    [Range(0.1f, 5.0f)]
    //Y axis sensitivity multiplier
    public float YCursorSens = 0.5f;
    //Camera move speed
    [Range(0.0f, 50.0f)]
    public float MoveSpeed = 10f;
    //Camera move speed modifier
    public float SpeedMod = 0.1f;
    //Y axis move up/down
    public float UpDown = 0.1f;

    void Update()
    {
        //Camera paning with mouse
        rotY += Input.GetAxis("Mouse X") * Sensitivity;
        rotX += Input.GetAxis("Mouse Y") * -1 * Sensitivity * YCursorSens;
        transform.localEulerAngles = new Vector3(rotX, rotY, 0);
        //Move camera with WASD
        float xAxisValue = Input.GetAxis("Horizontal") * MoveSpeed;
        float zAxisValue = Input.GetAxis("Vertical") * MoveSpeed;
        float yValue = 0.0f;

        if (Input.GetKey(KeyCode.Space))
        {
            yValue = yValue + MoveSpeed * UpDown;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            yValue = yValue - MoveSpeed * UpDown;
        }

        transform.position = new Vector3(transform.position.x + xAxisValue, transform.position.y + yValue, transform.position.z + zAxisValue);


        //
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && MoveSpeed > SpeedMod)
        {
            MoveSpeed = MoveSpeed - SpeedMod;
        } 
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            MoveSpeed = MoveSpeed + SpeedMod;
        }

        //Quit with esc
        if (Input.GetKey("escape"))
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }
}
