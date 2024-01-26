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

    [Header("Death Cinematic")]
    [SerializeField] private float m_d_cameraPositionY = 0.5f;
    [SerializeField] private float m_d_cameraRotationZ = 60f;
    [SerializeField] private float m_d_moveDuration = 3f;
    [SerializeField] private float m_d_stillTime = 4f;

    public void LaunchDeathCinematic()
    {
        m_input.SetCinematicInputs();
        HUBManager.instance.ResetHUB();
        HUBManager.instance.PointerActive(false);

        StartCoroutine(DeathCinematic());
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
