using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionLayerSwitch : MonoBehaviour
{
    [Header("Layers")]
    public LayerMask ropeLayer;
    public LayerMask ropeColliderSetA;
    public LayerMask ropeColliderSetB;
    public bool ignoreSetAFirst;

    bool isOnLayerA = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
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




