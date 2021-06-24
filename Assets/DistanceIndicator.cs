using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceIndicator : MonoBehaviour
{
    public float rotationSpeed;
    public float rotationSpeedBonus;
    public float offsetAngle;
    public float radius;

    [Range(0.0f, 1f)]
    public float dangerLevel;
    public int dotCount;
    public Gradient dangerGradient;
    public GameObject radialDot;
    public GameObject target;

    GameObject radialController;
    SpriteRenderer[] spriteRenderers;

    // Start is called before the first frame update
    void Awake()
    {
        radialController = new GameObject("distanceIndicator");
        radialController.transform.parent = transform;
        radialController.transform.localPosition = Vector3.zero;
        
        InstantateRadialDots(dotCount);

    }

    private void InstantateRadialDots(int dotCount)
    {
        spriteRenderers = new SpriteRenderer[dotCount];
        for (float i = 0; i < dotCount; i++)
        {
            GameObject instantiatedDot = Instantiate(radialDot, radialController.transform);
            Vector2 dotPosition = PolarToCartesian(radius, 2 * i * Mathf.PI / (float)dotCount);
            instantiatedDot.transform.localPosition = (Vector3)dotPosition;
            instantiatedDot.transform.localRotation = Quaternion.Euler(0, 0, (offsetAngle + (2 * i * Mathf.PI / (float)dotCount) * Mathf.Rad2Deg) % 360);
            spriteRenderers[(int)i] = instantiatedDot.GetComponent<SpriteRenderer>(); 
        }
     }

    private Vector2 PolarToCartesian(float radius, float phase)
    {
    
        float xCoord = Mathf.Cos(phase) * radius;
        float yCoord = Mathf.Sin(phase) * radius;
        return new Vector2(xCoord, yCoord);
    }
    private void Update()
    {
        dangerLevel = 1 - ((transform.position - target.transform.position).magnitude / 5f);//Mathf.Abs(Mathf.Sin(Time.realtimeSinceStartup));
        radialController.transform.Rotate(new Vector3(0, 0, (rotationSpeed + (rotationSpeedBonus * Mathf.Max(0f, ((dangerLevel - 0.5f) / 2.5f)))) * Time.deltaTime ));

        for(int i = 0; i < dotCount; i++) 
        {
            spriteRenderers[i].material.SetFloat("_Fade", 0.24f + (Mathf.Clamp(dangerLevel -0.6f , 0.0f, 0.38f)*4f));
            Reposition(radialController.transform.GetChild(i), Mathf.Max(0f, ((dangerLevel - 0.5f) / 1.8f)));
            spriteRenderers[i].color = dangerGradient.Evaluate(dangerLevel);
        }

    }

    private void Reposition(Transform dot, float dangerLevel)
    {
        dot.localPosition = dot.localPosition.normalized * (radius + dangerLevel );
    }
}

