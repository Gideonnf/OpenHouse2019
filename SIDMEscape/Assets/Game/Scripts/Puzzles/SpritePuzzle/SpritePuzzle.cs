using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRControllables.Base;

public class SpritePuzzle : MonoBehaviour
{
    [Header("Puzzle settings")]
    [Tooltip("Store the 6 slots to put the sprite objects in")]
    public Transform[] SpritePositions = new Transform[6];
    [Tooltip("Slot Rotation")]
    public Quaternion SpriteRotation;

    List<SpritePiece> spritePieceList = new List<SpritePiece>();
    bool FullPuzzle = false;

    bool[] slotArray = new bool[6];

    // Start is called before the first frame update
    void Start()
    {
        // set it to false
        for(int i = 0; i < slotArray.Length; ++i)
        {
            slotArray[i] = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        FullPuzzle = CheckIfFull();

        if (FullPuzzle == false)
        {

        }
    }

    bool CheckPuzzleStatus()
    {
        return false;
    }

    /// <summary>
    /// Checks if the puzzle is full already
    /// If it is, then we'll go on to checking the puzzle
    /// </summary>
    /// <returns></returns>
    bool CheckIfFull()
    {
        for (int i = 0; i < slotArray.Length; ++i)
        {
            // If any of them are still empty(false)
            //return false
            if (slotArray[i] == false)
                return false;
        }
        // If it reaches here means that its full

        return true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<SpritePiece>() == null)
            return;

        Controllable_Movables spriteControl = other.gameObject.GetComponent<Controllable_Movables>();
        SpritePiece spritePiece = other.gameObject.GetComponent<SpritePiece>();
        // Loop through the bool array
        // Check for empty slots
        for(int i = 0; i < slotArray.Length; ++i)
        {
            if (slotArray[i] == true)
                continue;
            //End the grab forcefully
            if(spriteControl.grabbedBy)
              spriteControl.grabbedBy.GrabEnd();

            //Set the new position and rotation
            other.gameObject.transform.position = SpritePositions[i].position;
           // other.transform.parent = this.gameObject.transform;
            other.gameObject.transform.rotation = SpritePositions[i].rotation;

            spritePieceList.Add(spritePiece);

            //Set up the sprite
            spritePiece.PlacedInSlot(i);

            slotArray[i] = true;
            break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        SpritePiece spritePiece = other.GetComponent<SpritePiece>();
        if (spritePiece == null)
            return;

        // Set the slot to empty
        slotArray[spritePiece.GetSlodID()] = false;
        spritePieceList.Remove(spritePiece);
    }
}
