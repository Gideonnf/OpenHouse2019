using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRControllables.Base;

public class HousePiece : MonoBehaviour
{
    [Header("Building Piece Settings")]
    [Tooltip("Linked Game Object")]
    public GameObject linkedObject;
    [Tooltip("Boolean to keep track if it is correct")]
    public bool correctObject = false;
    [Tooltip("Materal to Change to")]
    public Material changeableMaterial;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if ((this.transform.localPosition.x < 1 && this.transform.localPosition.y < 1 && this.transform.localPosition.z < 1) &&
        //    (this.transform.localPosition.x > -1 && this.transform.localPosition.y > -1 && this.transform.localPosition.z > -1))
        //{
        //    if ((this.transform.localEulerAngles.x < 5 && this.transform.localPosition.y < 5 && this.transform.localPosition.z < 5) &&
        //    (this.transform.localPosition.x > 355 && this.transform.localPosition.y > 355 && this.transform.localPosition.z > 355))
        //    {
        //        this.transform.localPosition = Vector3.zero;
        //        this.transform.localEulerAngles = Vector3.zero;
        //    }
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other == null)
        //    return;
        //if (other.gameObject == null)
        //    return;
        if (other.tag != "HousePuzzle")
            return;

        if (other.transform.parent.gameObject == linkedObject)
        {
            GameObject refObject = other.transform.parent.gameObject;

            // If the light isnt active, means the puzzle isnt working yet
            if (refObject.GetComponent<HousePuzzle>().puzzleLight.activeInHierarchy == false)
                return;

            // Change the material
            this.gameObject.GetComponentInChildren<MeshRenderer>().material = changeableMaterial;

            // If its being grabbed
            if(refObject.GetComponent<VRControllables.Base.Controllable_Movables>().grabbedBy)
            {
                //Release it first
                refObject.GetComponent<VRControllables.Base.Controllable_Movables>().grabbedBy.GrabEnd();
            }

            // Set the flag to true
            correctObject = true;

            // Play Audio
            SoundManager.instance.playAudio("Correct");

            // Then Destroy
            //Destroy the grabbable one
            Destroy(refObject);
        }
    }
}
