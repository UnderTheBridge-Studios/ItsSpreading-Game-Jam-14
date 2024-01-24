using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoorTrigger : MonoBehaviour
{
    [SerializeField] private I08_Door m_door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            m_door.CloseDoor();

        gameObject.SetActive(false);
    }
}
