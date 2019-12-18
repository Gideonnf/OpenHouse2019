﻿using UnityEngine;
using System.Collections;

public class MonitorObject : OVRGrabbable
{
    protected Vector3 originalLocPosition;
    protected Vector3 originalWorldPosition;
    protected Quaternion originalWorldRot;
    protected Quaternion originalLocRotation;

    private void Awake()
    {
        originalLocPosition = transform.localPosition;
        originalLocRotation = transform.localRotation;
        originalWorldPosition = transform.position;
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.tag == "TablePuzzle")
        {
            MiseEnPlace refMiseEnPlace = other.GetComponentInParent<MiseEnPlace>();
            if (refMiseEnPlace != null)
                refMiseEnPlace.SnapObject(this.gameObject, other);

        }
        //if (other.gameObject.name.StartsWith(this.gameObject.name) && other.gameObject.name.Contains("place"))
        //{
        //    this.gameObject.GetComponent<OVRGrabbable>().GrabEnd(Vector3.zero, Vector3.zero);
        //    this.transform.position = new Vector3(other.transform.position.x, this.transform.position.y, other.transform.position.z);
        //}
    }

    /// <summary>
    /// Returns the original position of the object
    /// </summary>
    public Vector3 getOriginalPos(bool local)
    {
        if(local)
        {
            return originalLocPosition;
        }
        else
        {
            return originalWorldPosition;
        }
    }

    /// <summary>
    /// Returns the original rotation of the object
    /// </summary>
    /// <returns></returns>
    public Quaternion getOriginalRot(bool local)
    {
        if (local)
        {
            return originalLocRotation;
        }
        else
        {
            return originalWorldRot;
        }
    }


}
