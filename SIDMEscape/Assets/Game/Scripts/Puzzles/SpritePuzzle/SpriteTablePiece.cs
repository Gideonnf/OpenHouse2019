using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRControllables.Base;

public class SpriteTablePiece : MonoBehaviour
{
    [Tooltip("Reference to the correct gameobject")]
    public GameObject correctObject;
    [Tooltip("Track whether the object is in a correct position")]
    public bool correctPos;

    MeshRenderer puzzlePieceRenderer;
    SpritePuzzle puzzleManager;

    bool spriteMoved = false;
    GameObject previousObject = null;

    float elapsedTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        puzzleManager = GetComponentInParent<SpritePuzzle>();
        puzzlePieceRenderer = GetComponentInChildren<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spriteMoved == true && elapsedTime > 0.0f)
        {
            elapsedTime -= Time.deltaTime;
           // spriteMoved = true;
        }
        else if (elapsedTime < 0.0f)
        {
            if(previousObject)
                previousObject = null;
            spriteMoved = false;
            elapsedTime = 0.0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided with " + other.name + ", " + other.tag);

        if (other.tag != "SpritePuzzle")
            return;

        if (previousObject == other.gameObject)
            return;

        if (spriteMoved == true)
            return;

        GameObject refObject = other.gameObject;

        Controllable_Movables spriteControl = other.gameObject.GetComponent<Controllable_Movables>();
        SpritePiece spritePiece = other.gameObject.GetComponent<SpritePiece>();

        if (spriteControl.grabbedBy)
            spriteControl.grabbedBy.GrabEnd();


        this.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;

        // this.transform.GetChild(0).gameObject.SetActive(false);
        //GetComponentInChildren<MeshRenderer>().enabled = false;
        //puzzlePieceRenderer.enabled = false;
        previousObject = other.gameObject;


        if (other.gameObject == correctObject)
        {
            correctPos = true;
            puzzleManager.spritePieceList.Add(spritePiece);
            Destroy(other.gameObject.GetComponent<Controllable_Movables>());
            this.transform.GetChild(0).gameObject.SetActive(false);
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            other.gameObject.GetComponent<Rigidbody>().useGravity = false;
            //other.gameObject.layer = 12;
            //other.gameObject.GetComponent<BoxCollider>().enabled = false;
            //SoundManager.instance.playAudio("Correct");
        }

        other.gameObject.transform.position = this.transform.position;
        other.gameObject.transform.rotation = this.transform.rotation;


        if (puzzleManager.CheckPuzzleStatus() == false)
        {
            puzzleManager.CheckPuzzlePiece();
        }
        else
        {
            puzzleManager.CompletedPuzzle();
        }
    }

    private void OnTriggerExit(Collider other)
    {

        // if (other.gameObject == correctObject)
        //  {
        this.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = true;
        //GetComponentInChildren<MeshRenderer>().enabled = true;
       // puzzlePieceRenderer.gameObject.SetActive(true);
        SpritePiece spritePiece = other.GetComponent<SpritePiece>();
        elapsedTime = 1f;
        spriteMoved = true;
        //puzzleManager.spritePieceList.Remove(spritePiece);
    //    }
    }
}
