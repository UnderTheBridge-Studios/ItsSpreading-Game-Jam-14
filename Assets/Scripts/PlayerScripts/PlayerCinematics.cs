using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCinematics : MonoBehaviour
{
    [SerializeField] private PlayerMovement m_movement;
    [SerializeField] private PlayerLook m_look;
    [SerializeField] private InputManager m_input;

    [SerializeField] private Transform m_camera;

    [Header("First Cinematic")]
    [SerializeField] private float m_f_stillTime = 8f;
    [SerializeField] private float m_f_forwardTime = 2f;
    [SerializeField] private float m_f_stillTime2 = 2f;

    [Header("Death Cinematic")]
    [SerializeField] private float m_d_cameraPositionY = 0.5f;
    [SerializeField] private float m_d_cameraRotationZ = 60f;
    [SerializeField] private float m_d_moveDuration = 6f;
    [SerializeField] private float m_d_stillTime = 8f;
    [SerializeField] private float m_d_soundVolume = 0.2f;

    public void LaunchFirstCinematic()
    {
        m_input.SetCinematicInputs();
        HUBManager.instance.PointerActive(false);
        
        StartCoroutine(FirstCinematic());
    }

    public void LaunchDeathCinematic()
    {
        m_input.SetCinematicInputs();
        SoundManager.instance.PlayClip(6, m_d_soundVolume);
        HUBManager.instance.ResetHUB();
        HUBManager.instance.PointerActive(false);

        StartCoroutine(DeathCinematic());
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

    private IEnumerator DeathCinematic()
    {
        m_movement.ReceiveInput(Vector2.zero);
        m_look.ReceiveInput(Vector2.zero);

        Vector3 cameraRotation = new Vector3(m_camera.localRotation.x, m_camera.localRotation.y, m_d_cameraRotationZ);
        m_camera.DOLocalMoveY(m_d_cameraPositionY, m_d_moveDuration);
        m_camera.DOLocalRotate(cameraRotation, m_d_moveDuration);

        yield return new WaitForSeconds(m_d_stillTime);

        GameManager.instance.RestartGame(false);
    }
}
