using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCinematics : MonoBehaviour
{
    [SerializeField] private PlayerMovement m_movement;
    [SerializeField] private PlayerLook m_look;
    [SerializeField] private InputManager m_input;

    [Header("First Cinematic")]
    [SerializeField] private float m_f_stillTime = 8f;
    [SerializeField] private float m_f_forwardTime = 2f;
    [SerializeField] private float m_f_stillTime2 = 2f;

    public void LaunchFirstCinematic()
    {
        m_input.SetCinematicInputs();
        HUBManager.instance.PointerActive(false);
        
        StartCoroutine(FirstCinematic());
    }

    private IEnumerator FirstCinematic()
    {
        yield return new WaitForSeconds(m_f_stillTime);
        
        m_movement.SetSlowSpeed();
        m_movement.ReceiveInput(Vector2.up);
        yield return new WaitForSeconds(m_f_forwardTime);
        
        m_movement.ReceiveInput(Vector2.zero);
        yield return new WaitForSeconds(m_f_stillTime2);
        
        m_movement.SetWalkingSpeed();
        m_input.SetGameplayInputs();
        HUBManager.instance.PointerActive(true);
    }
}
