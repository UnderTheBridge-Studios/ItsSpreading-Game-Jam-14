using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testscript : MonoBehaviour
{

    [ContextMenu("Restart")]
    void restart()
    {
        GameManager.instance.RestartGame(true);
    }

}
