using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeVisual : MonoBehaviour
{
    public Transform playerA;
    public Transform playerB;
    public Gradient ropeTensionGradient;
    LineRenderer ropeLine;

    Vector3 basePos = new Vector3(0,0,-1);
    PlayerMovement playerAScript;
    PlayerMovement playerBScript;

    float distance;

    AudioSource ropeStretch;
    private void Awake()
    {
        ropeStretch = GetComponent<AudioSource>();
        ropeStretch.volume = 0.0f;
        ropeStretch.Play();
    }

    private void Start()
    {
        ropeLine = GetComponent<LineRenderer>();
        playerAScript = playerA.GetComponent<PlayerMovement>();
        playerBScript = playerB.GetComponent<PlayerMovement>();
        
    }

    private void Update()
    {
        ropeLine.SetPosition(0, playerA.position + basePos);
        ropeLine.SetPosition(1, playerB.position + basePos);

        //color the rope (ideally only color the rope to represent "tension" if at least one climber is not attached
        distance = Vector2.Distance(playerA.position, playerB.position);
        if(!playerAScript.isAttached || !playerBScript.isAttached)
        {
            ropeLine.material.color = ropeTensionGradient.Evaluate(distance / 3.5f);
            if (distance >= 3.45f)
            {
                ropeStretch.volume = 0.1f;
            }
            else
            {
                ropeStretch.volume = 0.0f;
            }
            
        }
        else
        {
            ropeLine.material.color = ropeTensionGradient.Evaluate(0.0f);
            ropeStretch.volume = 0.0f;
        }
    }
}
