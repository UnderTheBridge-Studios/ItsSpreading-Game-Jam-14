using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScaling : MonoBehaviour
{
    private float currPos = 0;
    
    // Update is called once per frame
    void Update()
    {
        var parentGrowth = GetComponentInParent<MaterialBlockGrowth>();
        var parentTransform = GetComponentInParent<Transform>();
        currPos = parentGrowth.currentPosition;

        // Use Mathf.SmoothStep for smoother transitions
        float smoothPos = Mathf.SmoothStep(0f, 1f, currPos);

        parentTransform.transform.localScale = Vector3.Lerp(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1f, 1f, 1f), smoothPos);
    }
}
