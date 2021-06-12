using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAnimations : MonoBehaviour
{

    SpriteRenderer spriteRenderer;
    private Material matWhite;
    private Material matDefault;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        matDefault = spriteRenderer.material;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Jump"))
        {
            TurnWhite();
        }
        else 
        {
            TurnNormal();
        }
    }

    void TurnWhite() 
    {
        spriteRenderer.material = matWhite;
    }

    void TurnNormal() 
    {
        spriteRenderer.material = matDefault;
    }
}
