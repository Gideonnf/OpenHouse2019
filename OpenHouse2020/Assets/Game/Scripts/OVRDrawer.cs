using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVRDrawer : OVRGrabbable
{
    Vector3 startingPosition;
    Transform handlePosition;
    Quaternion startingRot;
    Rigidbody cubeRB;

    public GameObject drawerObject;
    public float maxDisplacement = 0.7f;

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        // Store the starting y position of the object
        startingPosition = transform.position;
        startingRot = transform.rotation;

        cubeRB = GetComponent<Rigidbody>();
    }

    override public void updateFixedPosition(Vector3 grabbablePosition)
    {
        // thrs no base to start anyway lmao get nae ned
        base.Start();

        // Calculate the distance travelled
        float distanceTravelled = (this.transform.position - startingPosition).magnitude;
        DebugManager.Instance.setDebugColor(Color.magenta);

        if (cubeRB.isKinematic)
        {
            cubeRB.isKinematic = false;
        }


        DebugManager.Instance.setDebugText("Grabbed position" + drawerObject.transform.InverseTransformPoint(grabbablePosition).ToString() + "\n" + "current pos" + drawerObject.transform.InverseTransformPoint(transform.position).ToString());
      //  DebugManager.Instance.setDebugText("Cur:" + this.transform.position.ToString() + ", Init:" + startingPosition.ToString() + "\n" + "distance :" + distanceTravelled.ToString());
        // if the distance travelled reaches the max
        // it cant move anymore
        if (distanceTravelled > maxDisplacement)
        {
            //DebugManager.Instance.setDebugColor(Color.green);

            return;
        }
        else
        {
            //cubeRB.AddForce(transform.forward * 2);
            //transform.Translate(transform.forward * Time.deltaTime);
            DebugManager.Instance.setDebugColor(Color.cyan);

        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = startingRot;
       // float distance = (m_grabbedBy.transform.position - handlePosition.position).magnitude;
        //Debug.Log(distance);
        //transform.position.y = startingYPos;
    }

    public void OnCollisionEnter(Collision collision)
    {
        DebugManager.Instance.setDebugColor(Color.blue);

        DebugManager.Instance.setDebugText("Enter Collision");
    }

    public void OnTriggerEnter(Collider other)
    {
        DebugManager.Instance.setDebugColor(Color.blue);

        DebugManager.Instance.setDebugText("Enter Trigger");
    }

    public void OnCollisionStay(Collision collision)
    {
        DebugManager.Instance.setDebugColor(Color.green);

        //if (m_grabbedBy)
            DebugManager.Instance.setDebugText("Is Colliding");
    }

    public void OnTriggerStay(Collider other)
    {
        DebugManager.Instance.setDebugColor(Color.green);

        if (m_grabbedBy)
        {
            DebugManager.Instance.setDebugText("Is Triggering");
            //cubeRB.AddForce(-transform.right * 2);
        }
    }


    public void OnCollisionExit(Collision collision)
    {
        DebugManager.Instance.setDebugColor(Color.grey);

        DebugManager.Instance.setDebugText("Left Collision");
    }

    public void OnTriggerExit(Collider other)
    {
        DebugManager.Instance.setDebugColor(Color.grey);

        DebugManager.Instance.setDebugText("Left Trigger");
    }
}
