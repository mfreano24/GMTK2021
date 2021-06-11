using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSpawner : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public int jointCount;
    public float spacer;
    public GameObject ropeLink;
    public GameObject ropeParent;

    // Start is called before the first frame update
    void Start()
    {
        //ropeParent = Instantiate(new GameObject());
        Physics2D.IgnoreLayerCollision(8, 11);
        GameObject pastLink = null;
        Vector3 distanceBetweenSpawnPoints = pointB.position - pointA.position;
        for (int i = 0; i < jointCount; i++) 
        {
            GameObject newLink = Instantiate(ropeLink, ropeParent.transform);


            newLink.transform.position = (pointA.position + (distanceBetweenSpawnPoints / jointCount) * i); 
            if (i == 0)
            {
                newLink.GetComponent<HingeJoint2D>().connectedBody = pointA.GetComponent<Rigidbody2D>();
                newLink.GetComponent<DistanceJoint2D>().connectedBody = pointA.GetComponent<Rigidbody2D>();
            }
            else
            {
                newLink.GetComponent<HingeJoint2D>().connectedBody = pastLink.GetComponent<Rigidbody2D>();
                newLink.GetComponent<DistanceJoint2D>().connectedBody = pastLink.GetComponent<Rigidbody2D>();
            }
            pastLink = newLink;
        }
      
        pastLink.GetComponent<HingeJoint2D>().connectedBody = pointB.GetComponent<Rigidbody2D>();
        pastLink.GetComponent<DistanceJoint2D>().connectedBody = pointB.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
