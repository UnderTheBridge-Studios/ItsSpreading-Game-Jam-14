using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienGroupTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            return;
            
        GetComponentInParent<AlienGroup>().StartSpreading();
        gameObject.SetActive(false);
    }
}
