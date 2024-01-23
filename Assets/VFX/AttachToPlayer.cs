using UnityEngine;

public class AttachToPlayer : MonoBehaviour
{
    [SerializeField] private GameObject m_player;

    void Update()
    {
        transform.position = m_player.transform.position;
    }
}
