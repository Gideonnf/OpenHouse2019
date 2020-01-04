using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRControllables.Base;

public class CoasterManager : MonoBehaviour
{
    [Tooltip("List of objects that the player can grab and place around")]
    public GameObject[] go_Objects;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    /// <summary>
    /// Trigger checking for all 3 coasters in parent doesnt work
    /// instead, they'll call this function in their own trigger check
    /// This will handle the locking of the object
    /// </summary>
    /// <param name="index"> The index of which coaster it is calling the function</param>
    /// <param name="collidedObject"> The object collided with (i.e the objects the player needs to find)</param>
    public void SnapObject(GameObject GrabbableObject, Collider CoasterObject)
    {
        Debug.Log("Collided on Enter");
        // Store the grabbable object
        //OVRGrabbable grabbableObject = collidedObject.gameObject.GetComponent<OVRGrabbable>();
        MonitorObject monitorObjectScript = GrabbableObject.gameObject.GetComponent<MonitorObject>();
        Controllable_Movables monitorMovableScript = GrabbableObject.gameObject.GetComponent<Controllable_Movables>();
        // Find which object it is
        GameObject tempObject = null;
        for(int i = 0; i < go_Objects.Length; ++i)
        {
            if (go_Objects[i] == GrabbableObject.gameObject)
                tempObject = go_Objects[i];
        }

        // End the grabbing forcefully
        monitorMovableScript.grabbedBy.GrabEnd();

        // New position of the object based on the coaster's x and z
        Vector3 newPosition = new Vector3(CoasterObject.transform.position.x,
                                                                monitorObjectScript.getOriginalPos(false).y,
                                                                CoasterObject.transform.position.z);
        Quaternion newRot = monitorObjectScript.getOriginalRot(false);

        if (tempObject != null)
        {
            tempObject.transform.rotation = newRot;
            tempObject.transform.position = newPosition;
        }
        else
        {
            Debug.LogError("The object doesn't exist in the list. Make sure you add it into the inspector of the table puzzle!");
        }
    }

}
