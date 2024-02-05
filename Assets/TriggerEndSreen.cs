using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEndSreen : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            return;

        HUBManager.instance.ShowEndSream();
        StartCoroutine(Close());
    }


    private IEnumerator Close()
    {
        yield return new WaitForSecondsRealtime(1f);
        GameManager.instance.InputManager.SetEndScreenInput();
    }
}
