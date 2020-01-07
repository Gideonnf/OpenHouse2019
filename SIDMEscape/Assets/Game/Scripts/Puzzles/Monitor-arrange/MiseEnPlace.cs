using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRControllables.Base;

public class MiseEnPlace : MonoBehaviour
{
    //Vector3[,] v3_placePos;

    [Header("MiseEnPlace Puzzle Settings")]
    [Tooltip("Reference object for the Monitor Screen required")]
    public GameObject go_Monitor;
    [Tooltip("List of objects that the player can grab and place around")]
    public List<GameObject> go_Objects;
   //[Tooltip("List of the places to place the item on")]
    List<GameObject> go_Places;

    // Start is called before the first frame update
    void Start()
    {
        go_Places = new List<GameObject>();
        // go_Objects = new List<GameObject>();

        // int ranIgnore = Random.Range(0, this.transform.childCount); //randomise a child id to use
        int ranIgnore = 0; //randomise a child id to use
        for (int i = 0; i < this.transform.childCount; ++i)
        {
            if (i == ranIgnore) //only 1 will be active
                continue;

            //set the rest false
            this.transform.GetChild(i).gameObject.SetActive(false);
        }

        //go through the active set to use those coasters
        for (int i = 0; i < this.transform.GetChild(ranIgnore).childCount; ++i)
        {
            go_Places.Add(this.transform.GetChild(ranIgnore).GetChild(i).gameObject);
        }
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
        for(int i = 0; i < go_Objects.Count; ++i)
        {
            if (go_Objects[i] == GrabbableObject.gameObject)
                tempObject = go_Objects[i];
        }

        // End the grabbing forcefully
        if (monitorMovableScript.grabbedBy)
            monitorMovableScript.grabbedBy.GrabEnd();

        float yOffset = GrabbableObject.transform.position.y - CoasterObject.transform.position.y;

        // New position of the object based on the coaster's x and z
        Vector3 newPosition = new Vector3(CoasterObject.transform.position.x,
                                                                CoasterObject.transform.position.y + yOffset,
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
