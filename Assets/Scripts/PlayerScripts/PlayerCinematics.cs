using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCinematics : MonoBehaviour
{
    [SerializeField] private PlayerMovement m_movement;
    [SerializeField] private PlayerLook m_look;
    [SerializeField] private InputManager m_input;

    [Header("First Cinematic")]
    [SerializeField] private float m_stillTime;

    private Coroutine m_coroutine;
    private bool m_isCinematicOver;
}
