using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Lighting : MonoBehaviour
{
    [SerializeField] Light2D light;
    private float minOuterRadius = 6f;

    public void FlickerRadiusDown()
    {
        light.pointLightOuterRadius = minOuterRadius;
    }

    public void FlickerRadiusMax()
    {
        light.intensity = Random.Range(1f, 1.1f);
        light.pointLightOuterRadius = light.pointLightInnerRadius + Random.Range(5f, 5.3f);
    }
}
