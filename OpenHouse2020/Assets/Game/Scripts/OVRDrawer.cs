﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVRDrawer : OVRGrabbable
{
    Transform startingTransform;
    Transform handlePosition;
    Quaternion startingRot;
    Rigidbody cubeRB;

    public float maxDisplacement = 0.7f;

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        // Store the starting y position of the object
        startingTransform = transform;
        startingRot = transform.rotation;

        cubeRB = GetComponent<Rigidbody>();
    }

    override public void updateFixedPosition(Vector3 grabbablePosition)
    {
        // thrs no base to start anyway lmao get nae ned
        base.Start();
        DebugManager.Instance.setDebugColor(Color.magenta);

        // Calculate the distance travelled
        float distanceTravelled = (this.transform.position - startingTransform.position).magnitude;

        DebugManager.Instance.setDebugText(distanceTravelled.ToString());
        // if the distance travelled reaches the max
        // it cant move anymore
        if (distanceTravelled > maxDisplacement)
        {
            DebugManager.Instance.setDebugColor(Color.green);

            return;
        }
        else
        {
            //cubeRB.AddForce(transform.forward * 2);
            transform.Translate(transform.forward * Time.deltaTime);
            DebugManager.Instance.setDebugColor(Color.cyan);

        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = startingRot;
        float distance = (m_grabbedBy.transform.position - handlePosition.position).magnitude;
        //Debug.Log(distance);
        //transform.position.y = startingYPos;
    }
}
