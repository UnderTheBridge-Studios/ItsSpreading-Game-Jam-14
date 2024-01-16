using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private Transform m_playerCamera;
    [SerializeField] private float m_BounceY = 3f;
    [SerializeField] private float m_BounceTime = 0.25f;
    [SerializeField] private float m_sensitivityX = 15f;
    [SerializeField] private float m_sensitivityY = 0.5f;
    private float m_xClamp = 85f;

    private float m_xRotation = 0f;
    private float m_lookX;
    private float m_lookY;
    private Tween m_tween;

    private void Start()
    {
        //Confine and hide cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, m_lookX * Time.deltaTime);

        m_xRotation -= m_lookY;
        m_xRotation = Mathf.Clamp(m_xRotation, -m_xClamp, m_xClamp);

        Vector3 targetRotation = transform.eulerAngles;
        targetRotation.x = m_xRotation;
        m_playerCamera.eulerAngles = targetRotation;
    }

    public void ReceiveInput(Vector2 cameraInput)
    {
        m_lookX = cameraInput.x * m_sensitivityX;
        m_lookY = cameraInput.y * m_sensitivityY;
    }

    public void CameraBounce()
    {
        if (m_tween == null)
            m_tween = m_playerCamera.DOLocalMoveY(m_BounceY, m_BounceTime).SetLoops(2, LoopType.Yoyo);
        
        m_tween.OnComplete(() => m_tween.Restart()).SetAutoKill(false);
    }

    public void StopCameraBounce()
    {
        m_tween.OnComplete(NullifyTween);
    }

    private void NullifyTween()
    {
        m_tween.SetAutoKill(true);
        m_tween = null;
    }
}
