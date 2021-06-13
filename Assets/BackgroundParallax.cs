using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    public float scrollFactor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float scrollSpeed = (Mathf.Sqrt((GameManager.Instance.climbRate / 200f)) / 200f) * scrollFactor;
        transform.localPosition -= Vector3.up * scrollSpeed;
    }
}
