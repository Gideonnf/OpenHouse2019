using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRControllables.Base;

public class MiseEnPlace : MonoBehaviour
{
    Vector3[,] v3_placePos;

    [Header("MiseEnPlace Puzzle Settings")]
    [Tooltip("Reference object for the Monitor Screen required")]
    public GameObject go_Monitor;
    [Tooltip("List of objects that the player can grab and place around")]
    public GameObject[] go_Objects;
   [Tooltip("List of the places to place the item on")]
    public GameObject[] go_Places;

    // Start is called before the first frame update
    void Start()
    {
        v3_placePos = new Vector3[,] { {new Vector3(1.391f, -0.156f, -2.37f), new Vector3(0.436f, -0.156f, -2.464f), new Vector3(0.782f, -0.156f, -2.623f) },
                                                               {new Vector3(1.652f, -0.156f, -2.526f), new Vector3(0.975f, -0.156f, -2.464f), new Vector3(0.545f, -0.156f, -2.392f) },
                                                               {new Vector3(0.545f, -0.156f, -2.358f), new Vector3(0.973f, -0.156f, -2.589f), new Vector3(1.56f, -0.156f, -2.352f) }};

        for (int j = 0; j < go_Places.Length; ++j)
        {
            if ((int)go_Monitor.GetComponent<MonitorRandomiser>().n_monitorStates == 0)
                break;

            go_Places[j].transform.localPosition = v3_placePos[(int)go_Monitor.GetComponent<MonitorRandomiser>().n_monitorStates - 1, j];
        }

        //for (int i = 0; i < go_Objects.Length; ++i)
        //{
        //    go_Objects[i].transform.localPosition = new Vector3(Random.Range(0.24f, 1.8f), go_Objects[i].transform.localPosition.y, Random.Range(-2.707f, -2.3f));
        //}
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
