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
            isOnLayerA = true;
        }
    }

    private void SwitchToLayerA()
    {
        Physics.IgnoreLayerCollision(ropeColliderSetB, ropeLayer, false);
        Physics.IgnoreLayerCollision(ropeColliderSetA, ropeLayer, true);
    }

    private void SwitchToLayerB()
    {
        Physics.IgnoreLayerCollision(ropeColliderSetA, ropeLayer, false);
        Physics.IgnoreLayerCollision(ropeColliderSetB, ropeLayer, true);
    }
}




