using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidClimbSpotFinder : MonoBehaviour
{
    LayerMask climbingLayers = 0b1000000000000;
    BoxCollider2D playerCollider;

    public void Start()
    {
        playerCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    public  bool CheckPlayerPosition() 
    {
        Vector2 boxDimensions = new Vector2(playerCollider.size.x, playerCollider.size.y);

        if (Physics2D.OverlapBox(playerCollider.gameObject.transform.position, boxDimensions, 0f, climbingLayers))
        {
            return true;
        }
        else 
        {
            return false;
        }
    }
}
