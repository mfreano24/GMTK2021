using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidClimbSpotFinder : MonoBehaviour
{
    public LayerMask climbingLayers;
    public BoxCollider2D playerCollider;

    public bool CheckPlayerPosition() 
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
