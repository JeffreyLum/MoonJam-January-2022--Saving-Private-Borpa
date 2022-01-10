using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{

    private LineRenderer lr;

    private Vector3 grapplePoint;
    public LayerMask ObjectsYouCanGrappleTo;
    public Transform grappleTip, playerCamera, player;
    public float maxGrappleDistance = 100f;
    private Vector3 currentGrapplePosition;
    private SpringJoint joint;
    
    public float jointSpring = 5f;
    public float jointDamper = 10f;
    public float jointMassScale = 4.5f;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {

            StartGrapple();
            Debug.Log("Grapple started");
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            StopGrapple();
            Debug.Log("Grapple Released");

        }
    }

    private void LateUpdate()
    {
        DrawRope();

    }
    void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, maxGrappleDistance, ObjectsYouCanGrappleTo))
        {
            FindObjectOfType<Audio>().Play("borpa");

            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            joint.spring = jointSpring;
            joint.damper = jointDamper;
            joint.massScale = jointMassScale;
            lr.positionCount = 2;
            currentGrapplePosition = grappleTip.position;
        }
    }

    void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
    }

    void DrawRope()
    {
        if (!joint) 
            return; // Will not draw the rope if not grappling
        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);
        lr.SetPosition(0, grappleTip.position);
        lr.SetPosition(1, grapplePoint);
    }

    public bool IsGrappling()
    {
        return joint != null;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }
}
