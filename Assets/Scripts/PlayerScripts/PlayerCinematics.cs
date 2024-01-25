using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCinematics : MonoBehaviour
{
    [SerializeField] private PlayerMovement m_movement;
    [SerializeField] private PlayerLook m_look;
    [SerializeField] private InputManager m_input;

    [Header("First Cinematic")]
    [SerializeField] private float m_stillTime = 8f;
    [SerializeField] private float m_forwardTime = 2f;
    [SerializeField] private float m_stillTime2 = 2f;

    public void LaunchFirstCinematic()
    {
        StartCoroutine(FirstCinematic());
    }

    private IEnumerator FirstCinematic()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        yield return new WaitForSeconds(m_stillTime);
        m_movement.SetSlowSpeed();
        m_movement.ReceiveInput(Vector2.up);

        yield return new WaitForSeconds(m_forwardTime);
        m_movement.ReceiveInput(Vector2.zero);

        yield return new WaitForSeconds(m_stillTime2);
        m_movement.SetWalkingSpeed();
        m_input.SetGameplayInputs();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
