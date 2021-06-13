using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    public GameObject mountain;
    public List<GameObject> stars;
    public float scrollFactor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float scrollSpeed = (Mathf.Sqrt((GameManager.Instance.climbRate / (200f * 4f))) / 200f) * scrollFactor;
        mountain.transform.localPosition -= Vector3.up * scrollSpeed;

        foreach (GameObject star in stars) 
        {
            star.transform.localPosition -= Vector3.up * scrollSpeed;
            if (star.transform.localPosition.y < -7.96f) 
            {
                star.transform.localPosition += Vector3.up * 20.48f * 3f * star.transform.localScale.y;
                star.transform.localPosition -= Vector3.up * scrollSpeed;
            }
        }
    }
}
