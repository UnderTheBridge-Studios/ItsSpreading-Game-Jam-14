using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCinematics : MonoBehaviour
{
    [SerializeField] private PlayerMovement m_movement;
    [SerializeField] private PlayerLook m_look;

    [SerializeField] private Transform m_camera;

    [Header("Death Cinematic")]
    [SerializeField] private float m_d_cameraPositionY = 0.5f;
    [SerializeField] private float m_d_cameraRotationZ = 60f;
    [SerializeField] private float m_d_moveDuration = 6f;
    [SerializeField] private float m_d_stillTime = 8f;
    [SerializeField] private float m_d_soundVolume = 0.2f;

    public void LaunchDeathCinematic()
    {
        GameManager.instance.InputManager.SetGameplayInput(false);
        SoundManager.instance.PlayClip(6, m_d_soundVolume);
        GameManager.instance.CanvasManager.ResetHUD();
        GameManager.instance.CanvasManager.PointerActive(true);

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
