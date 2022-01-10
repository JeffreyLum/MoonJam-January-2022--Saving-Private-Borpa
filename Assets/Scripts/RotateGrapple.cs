using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGrapple : MonoBehaviour
{

    public Grapple grapple;
    private Quaternion correctRotation;
    public float rotationSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!grapple.IsGrappling())
        {
            correctRotation = transform.parent.rotation;
        } else
        {
            correctRotation = Quaternion.LookRotation(grapple.GetGrapplePoint() - transform.position);
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, correctRotation, Time.deltaTime * rotationSpeed);
    }
}
