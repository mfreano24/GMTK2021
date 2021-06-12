using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeParts : MonoBehaviour
{
    public HingeJoint2D hingeOne;
    public HingeJoint2D hingeTwo;


    public void SetHingeJointOneBody(Rigidbody2D targetBody) 
    {
        hingeOne.connectedBody = targetBody;
    }


    public void SetHingeJointTwoBody(Rigidbody2D targetBody)
    {
        hingeTwo.connectedBody = targetBody;
    }

}
