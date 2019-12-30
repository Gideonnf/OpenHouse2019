using UnityEngine;
using System.Collections;
using VRControllables.Base;

public class VideoTiles : MonoBehaviour
{
    public bool correctCoaster = false;

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
        if (other.tag == "Video Coasters")
        {
            CoasterManager refCoasterManager = other.GetComponentInParent<CoasterManager>();
            
            if (refCoasterManager != null)
            {
                refCoasterManager.SnapObject(this.gameObject, other);

                if (other.gameObject.name.StartsWith(this.gameObject.name) && other.gameObject.name.Contains("place"))
                {
                    correctCoaster = true;
                }
            }

        }
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
