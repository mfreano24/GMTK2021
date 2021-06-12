using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidClimbSpotFinder : MonoBehaviour
{
    LayerMask climbingLayers = 0b1000000000000;
    BoxCollider2D playerCollider;

    public void Awake()
    {
        playerCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    public bool CheckPlayerPosition() 
    {
        Vector2 boxDimensions = (new Vector2(playerCollider.size.x * transform.localScale.x, playerCollider.size.y * transform.localScale.y));

        if (Physics2D.OverlapBox(playerCollider.gameObject.transform.position, boxDimensions, 0f, climbingLayers))
        {
            return true;
        }
        else 
        {
            return false;
        }
    }
    Vector3 gizmoSize;
    Vector3 gizmoPosition; 

    public bool CheckPlayerPosition(Vector2 offset)
    {
        Vector2 boxDimensions = (new Vector2(playerCollider.size.x * transform.localScale.x, playerCollider.size.y * transform.localScale.y));

        gizmoSize = boxDimensions;
        gizmoPosition = ((Vector2)playerCollider.gameObject.transform.position) + offset;

        if (Physics2D.OverlapBox(((Vector2)playerCollider.gameObject.transform.position) + offset, boxDimensions, 0f, climbingLayers))
        {
            return true;

        }
        else
        {
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(gizmoPosition, gizmoSize);
    }
}
