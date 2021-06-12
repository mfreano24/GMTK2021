using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionLayerSwitch : MonoBehaviour
{
    [Header("Layers")]
    public int ropeLayer = 8;
    public int ropeColliderSetA = 9;
    public int ropeColliderSetB = 10;
    public bool ignoreSetAFirst;

    bool isOnLayerA = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isOnLayerA = !isOnLayerA;
            SwitchLayers();
        }
    }

    private void SwitchLayers()
    {
        if (isOnLayerA)
        {
            SwitchToLayerA();
        }
        else
        {
            SwitchToLayerB();
        }
    }

    private void SwitchToLayerA()
    {
        Physics2D.IgnoreLayerCollision(ropeColliderSetB, ropeLayer, false);
        Physics2D.IgnoreLayerCollision(ropeColliderSetA, ropeLayer, true);
    }

    private void SwitchToLayerB()
    {
        Physics2D.IgnoreLayerCollision(ropeColliderSetA, ropeLayer, false);
        Physics2D.IgnoreLayerCollision(ropeColliderSetB, ropeLayer, true);
    }
}




