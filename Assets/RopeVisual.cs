using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeVisual : MonoBehaviour
{
    public Transform playerA;
    public Transform playerB;
    LineRenderer ropeLine;

    Vector3 basePos = new Vector3(0,0,1);

    private void Start()
    {
        ropeLine = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        ropeLine.SetPosition(0, playerA.position + basePos);
        ropeLine.SetPosition(1, playerB.position + basePos);
    }
}
