using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRControllables.Base;

public class SpritePuzzle : MonoBehaviour
{

    //public enum SpriteAnimID
    //{
    //    NOANIM = 0,
    //    FIRSTANIM,
    //    SECONDANIM,
    //    THIRDANIM,
    //    FOURTHANIM,
    //    FIFTHANIM,
    //    SIXTHANIM
    //}

    [Header("Puzzle settings")]
    [Tooltip("Store the 6 slots to put the sprite objects in")]
    public Transform[] SpritePositions = new Transform[6];
   // [Tooltip("Slot Rotation")]
    //public Quaternion SpriteRotation;
    [Tooltip("ID Array Order. Stores the correct order of sprite IDs")]
    public int[] FinalArrangement = new int[6];
    [Tooltip("Reference to the sprite puzzle animator")]
    public Animator puzzleAnimController;

    List<SpritePiece> spritePieceList = new List<SpritePiece>();
    bool Completed = false;

    bool[] slotArray = new bool[6];

    bool[] animStates = new bool[6];
    bool[] animIsPlaying = new bool[6];

    // Start is called before the first frame update
    void Start()
    {
        // set it to false
        for(int i = 0; i < slotArray.Length; ++i)
        {
            slotArray[i] = false;
            animStates[i] = false;
            animIsPlaying[i] = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //puzzleAnimController.SetBool("Completed", Completed);
        if(Completed)
        {
            if (animStates[0] == false)
            {
                if (animIsPlaying[0] == false)
                {
                    // Set the frame state
                    spritePieceList[0].spriteAnim.Play("FirstFrameAnim", 0, 0);
                    animIsPlaying[0] = true;
                }
                Debug.Log("0th time : " + spritePieceList[0].spriteAnim.GetCurrentAnimatorStateInfo(0).normalizedTime);

                // if it reaches near the end
                if (spritePieceList[0].spriteAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.95f)
                {
                    // Set the speed to 0 so it will stop
                    spritePieceList[0].spriteAnim.enabled = false;
                    // Set the state to complete
                    animStates[0] = true;
                }
            }
            else if (animStates[1] == false)
            {
                if (animIsPlaying[1] == false)
                {
                    spritePieceList[1].spriteAnim.Play("SecondFrameAnim", 0, 0);

                    animIsPlaying[1] = true;
                }

                Debug.Log("first time : " + spritePieceList[1].spriteAnim.GetCurrentAnimatorStateInfo(0).normalizedTime);

                if (spritePieceList[1].spriteAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.95f)
                {
                    // Set the speed to 0 so it will stop
                    spritePieceList[1].spriteAnim.enabled = false;
                    // Set the state to complete
                    animStates[1] = true;
                }

            }
            else if (animStates[2] == false)
            {
                if (animIsPlaying[2] == false)
                {
                    Debug.Log("second time : " + spritePieceList[2].spriteAnim.GetCurrentAnimatorStateInfo(0).normalizedTime);
                    spritePieceList[2].spriteAnim.Play("ThirdFrameAnim", 0, 0);
                    Debug.Log("second time : " + spritePieceList[2].spriteAnim.GetCurrentAnimatorStateInfo(0).normalizedTime);

                    animIsPlaying[2] = true;
                }

                Debug.Log("second time : " + spritePieceList[2].spriteAnim.GetCurrentAnimatorStateInfo(0).normalizedTime);


                if (spritePieceList[2].spriteAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.95f)
                {
                    // Set the speed to 0 so it will stop
                    spritePieceList[2].spriteAnim.enabled = false;
                    // Set the state to complete
                    animStates[2] = true;
                }

            }
            else if (animStates[3] == false)
            {
                if(animIsPlaying[3] == false)
                {
                    Debug.Log("third time : " + spritePieceList[3].spriteAnim.GetCurrentAnimatorStateInfo(0).normalizedTime);
                    spritePieceList[3].spriteAnim.Play("FourthFrameAnim", 0, 0);
                    Debug.Log("third time : " + spritePieceList[3].spriteAnim.GetCurrentAnimatorStateInfo(0).normalizedTime);

                    animIsPlaying[3] = true;
                }
                Debug.Log("third time : " + spritePieceList[3].spriteAnim.GetCurrentAnimatorStateInfo(0).normalizedTime);

                if (spritePieceList[3].spriteAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.95f)
                {
                    // Set the speed to 0 so it will stop
                    spritePieceList[3].spriteAnim.enabled = false;
                    // Set the state to complete
                    animStates[3] = true;
                }

            }
            else if (animStates[4] == false)
            {
                if (animIsPlaying[4] == false)
                {
                    Debug.Log("fourth time : " + spritePieceList[4].spriteAnim.GetCurrentAnimatorStateInfo(0).normalizedTime);
                    spritePieceList[4].spriteAnim.Play("FifthFrameAnim", 0, 0);
                    Debug.Log("fourth time : " + spritePieceList[4].spriteAnim.GetCurrentAnimatorStateInfo(0).normalizedTime);

                    animIsPlaying[4] = true;
                }
                Debug.Log("fourth time : " + spritePieceList[4].spriteAnim.GetCurrentAnimatorStateInfo(0).normalizedTime);

                if (spritePieceList[4].spriteAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.95f)
                {
                    // Set the speed to 0 so it will stop
                    spritePieceList[4].spriteAnim.enabled = false;
                    // Set the state to complete
                    animStates[4] = true;
                }

            }
            else if (animStates[5] == false)
            {
                if(animIsPlaying[5] == false)
                {
                    Debug.Log("fifth time : " + spritePieceList[5].spriteAnim.GetCurrentAnimatorStateInfo(0).normalizedTime);
                    spritePieceList[5].spriteAnim.Play("SixthFrameAnim", 0, 0);
                    Debug.Log("fifth time : " + spritePieceList[5].spriteAnim.GetCurrentAnimatorStateInfo(0).normalizedTime);

                    animIsPlaying[5] = true;
                }
                Debug.Log("fifth time : " + spritePieceList[5].spriteAnim.GetCurrentAnimatorStateInfo(0).normalizedTime);

                if (spritePieceList[5].spriteAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.95f)
                {
                    // Set the speed to 0 so it will stop
                    spritePieceList[5].spriteAnim.enabled = false;
                    // Set the state to complete
                    animStates[5] = true;
                }
            }
            else
            {
                // Reached the end
                for(int i = 0; i < spritePieceList.Count; ++i)
                {
                    animStates[i] = false;
                    animIsPlaying[i] = false;
                    spritePieceList[i].spriteAnim.enabled = true;
                    spritePieceList[i].spriteAnim.Play("InitState", 0, 0);
                    
                }
            }
        }
    }

    public void ToggleAnimationSequence(int id)
    {
        //// Iterate through all the animations
        //for (int i = 0; i < spritePieceList.Count; ++i)
        //{
        //    //
        //    if (i == id)
        //        spritePieceList[id].SetFrameState(id);
            
        //}
        spritePieceList[id - 1].SetFrameState(id);
    }

    public void ResetAnimations()
    {
            // Iterate through all the animations
            for (int i = 0; i < spritePieceList.Count; ++i)
            {
                spritePieceList[i].ResumeAnimation();
                spritePieceList[i].SetFrameState(0);
            }
        }

        public void PauseAnimation(int id)
    {
        spritePieceList[id - 1].PauseAnimation();
    }

    public void ResumeAnimation(int id)
    {
        spritePieceList[id - 1].ResumeAnimation();
    }
    
    void CheckPuzzlePiece()
    {
        for (int i = 0; i < spritePieceList.Count; ++i)
        {
            // If the ID's match the correct final arrangement
            if (spritePieceList[i].SpriteID == FinalArrangement[i])
            {
                // Pass in final arrangement value cause it the animation id is the same as the final arrangement ids
                spritePieceList[i].SetFrameState(FinalArrangement[i]);
            }
            else
            {
                // Else set it to 0 because thats none
                spritePieceList[i].SetFrameState(0);
            }
        }

    }

    /// <summary>
    /// Checks the status of the puzzle after every placement
    /// If it is completed, it'll play all the animations in sequence 
    /// If it isn't, it'll only play the correct animations
    /// </summary>
    /// <returns></returns>
    bool CheckPuzzleStatus()
    {
        if (CheckIfFull() == false)
        {
            Completed = false;
            return false;
        }

        for (int i = 0; i < spritePieceList.Count; ++i)
        {
            // If the ID's match the correct final arrangement
            if (spritePieceList[i].SpriteID != FinalArrangement[i])
            {
                // Pass in final arrangement value cause it the animation id is the same as the final arrangement ids
                Completed = false;
                return false;
            }
        }

        Completed = true;
        return true;
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

    void CompletedPuzzle()
    {
        // Disable the movable script because it cant be grabbed anymore after completion
        for(int i = 0; i < spritePieceList.Count; ++i)
        {
            // Set all the states back to idle
            spritePieceList[i].SetFrameState(0);
            spritePieceList[i].spriteAnim.Play("InitState", 0, 0);
            // Disable the component so it cant be grabbed
            spritePieceList[i].gameObject.GetComponent<Controllable_Movables>().enabled = false;
        }
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

            // If the puzzle isn't completed yet
            // Check each piece
            if (CheckPuzzleStatus() == false)
                CheckPuzzlePiece();
            else
                CompletedPuzzle();

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
