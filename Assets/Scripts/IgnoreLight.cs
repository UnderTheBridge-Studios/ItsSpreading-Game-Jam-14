using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreLight : MonoBehaviour
{
    [SerializeField] private List<Light> limelight = new List<Light>();

    private void OnPreCull()
    {
        foreach (Light light in limelight)
        {
            light.enabled = false;
        }
    }
    private void OnPreRender()
    {
        foreach (Light light in limelight)
        {
            light.enabled = false;
        }
    }
    private void OnPostRender()
    {
        foreach (Light light in limelight)
        {
            light.enabled = true;
        }
    }
}
