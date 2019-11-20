using UnityEngine;
using System.Collections;

public class MonitorObject : OVRGrabbable
{
    protected Vector3 originalPosition;
    protected Quaternion originalRotation;

    private void Awake()
    {
        originalPosition = transform.localPosition;
        originalRotation = transform.localRotation;
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
    /// Returns the original position oif the object
    /// </summary>
    public Vector3 getOriginalPos()
    {
        return originalPosition;
    }

    /// <summary>
    /// Returns the original rotation of the object
    /// </summary>
    /// <returns></returns>
    public Quaternion getOriginalRot()
    {
        return originalRotation;
    }


}
