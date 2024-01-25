using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoorTrigger : MonoBehaviour
{
    [SerializeField] private I08_Door m_door;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag != "Player")
            return;

        m_door.CloseDoor();

        if (!m_door.CanReOpen)
            gameObject.SetActive(false);
    }
}
