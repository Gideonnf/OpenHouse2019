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
    [Tooltip("ID Array Order. Stores the correct order of sprite IDs")]
    public int[] FinalArrangement = new int[6];
    [Tooltip("Store a reference to the sprite table pieces")]
    public GameObject[] SpriteTablePieces = new GameObject[6];

    [System.NonSerialized]
    public List<SpritePiece> spritePieceList = new List<SpritePiece>();
    List<AnimatorStateInfo> spriteAnimStateInfo = new List<AnimatorStateInfo>();
    bool Completed = false;

    bool nextLight = false;

    bool[] slotArray = new bool[6];
    bool[] animStates = new bool[6];
    bool[] animIsPlaying = new bool[6];
    int[] animHashID = new int[6];

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

        animHashID[0] = Animator.StringToHash("Base Layer.FirstFrameAnim");
        animHashID[1] = Animator.StringToHash("Base Layer.SecondFrameAnim");
        animHashID[2] = Animator.StringToHash("Base Layer.ThirdFrameAnim");
        animHashID[3] = Animator.StringToHash("Base Layer.FourthFrameAnim");
        animHashID[4] = Animator.StringToHash("Base Layer.FifthFrameAnim");
        animHashID[5] = Animator.StringToHash("Base Layer.SixthFrameAnim");


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
                    spritePieceList[0].SetFrameState(1);
                    spritePieceList[0].spriteAnim.speed = 1;
                    animIsPlaying[0] = true;
                }
                Debug.Log("0th time : " + spritePieceList[0].spriteAnim.GetCurrentAnimatorStateInfo(0).normalizedTime);

                // if it reaches near the end
                if ((spritePieceList[0].spriteAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f) &&
                    (spritePieceList[0].spriteAnim.GetCurrentAnimatorStateInfo(0).fullPathHash == animHashID[0]))
                {
                    spritePieceList[0].spriteAnim.SetBool("Completed", true);
                    //spritePieceList[0].SetFrameState(0);
                    // spritePieceList[0].spriteAnim.speed = 0;
                    // Set the state to complete
                    animStates[0] = true;
                }
            }
            else if (animStates[1] == false)
            {
                if (animIsPlaying[1] == false)
                {
                    spritePieceList[1].SetFrameState(2);
                    spritePieceList[1].spriteAnim.speed = 1;
                    animIsPlaying[1] = true;
                }

                Debug.Log("first time : " + spritePieceList[1].spriteAnim.GetCurrentAnimatorStateInfo(0).normalizedTime);

                if ((spritePieceList[1].spriteAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f) &&
                    (spritePieceList[1].spriteAnim.GetCurrentAnimatorStateInfo(0).fullPathHash == animHashID[1]))
                {
                    spritePieceList[1].spriteAnim.SetBool("Completed", true);
                    //spritePieceList[1].SetFrameState(0);
                    // spritePieceList[1].spriteAnim.speed = 0;
                    // Set the state to complete
                    animStates[1] = true;
                }

            }
            else if (animStates[2] == false)
            {
                if (animIsPlaying[2] == false)
                {
                    
                    spritePieceList[2].SetFrameState(3);
                    spritePieceList[2].spriteAnim.speed = 1;
                    animIsPlaying[2] = true;
                }

                Debug.Log("second time : " + spritePieceList[2].spriteAnim.GetCurrentAnimatorStateInfo(0).normalizedTime);


                if ((spritePieceList[2].spriteAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >=0.98f) &&
                    (spritePieceList[2].spriteAnim.GetCurrentAnimatorStateInfo(0).fullPathHash == animHashID[2]))
                {
                    spritePieceList[2].spriteAnim.SetBool("Completed", true);
                    //spritePieceList[2].SetFrameState(0);
                    //  spritePieceList[2].spriteAnim.speed = 0;
                    // Set the state to complete
                    animStates[2] = true;
                }

            }
            else if (animStates[3] == false)
            {
                if(animIsPlaying[3] == false)
                {
                    spritePieceList[3].SetFrameState(4);
                    spritePieceList[3].spriteAnim.speed = 1;
                    animIsPlaying[3] = true;

                }
                Debug.Log("third time : " + spritePieceList[3].spriteAnim.GetCurrentAnimatorStateInfo(0).normalizedTime);

                if ((spritePieceList[3].spriteAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.98f) &&
                    (spritePieceList[3].spriteAnim.GetCurrentAnimatorStateInfo(0).fullPathHash == animHashID[3]))
                {
                    spritePieceList[3].spriteAnim.SetBool("Completed", true);
                    //spritePieceList[3].SetFrameState(0);
                    //spritePieceList[3].spriteAnim.speed = 0;
                    // Set the state to complete
                    animStates[3] = true;
                }

            }
            else if (animStates[4] == false)
            {
                if (animIsPlaying[4] == false)
                {
                    spritePieceList[4].SetFrameState(5);
                    spritePieceList[4].spriteAnim.speed = 1;
                    animIsPlaying[4] = true;
                }
                Debug.Log("fourth time : " + spritePieceList[4].spriteAnim.GetCurrentAnimatorStateInfo(0).normalizedTime);

                if ((spritePieceList[4].spriteAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.98f) &&
                    (spritePieceList[4].spriteAnim.GetCurrentAnimatorStateInfo(0).fullPathHash == animHashID[4]))
                {
                    spritePieceList[4].spriteAnim.SetBool("Completed", true);
                    //spritePieceList[4].SetFrameState(0);
                    //spritePieceList[4].spriteAnim.speed = 0;
                    // Set the state to complete
                    animStates[4] = true;
                }

            }
            else if (animStates[5] == false)
            {
                if(animIsPlaying[5] == false)
                {
                    spritePieceList[5].SetFrameState(6);
                    spritePieceList[5].spriteAnim.speed = 1;
                    animIsPlaying[5] = true;
                }
                Debug.Log("fifth time : " + spritePieceList[5].spriteAnim.GetCurrentAnimatorStateInfo(0).normalizedTime);

                if ((spritePieceList[5].spriteAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f) &&
                    (spritePieceList[5].spriteAnim.GetCurrentAnimatorStateInfo(0).fullPathHash == animHashID[5]))
                {
                    spritePieceList[5].spriteAnim.SetBool("Completed", true);
                    // spritePieceList[5].spriteAnim.speed = 0;
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
                    spritePieceList[i].spriteAnim.SetBool("Completed", false);
                    spritePieceList[i].SetFrameState(0);
                }
                if (nextLight == false)
                {
                    PuzzleLightManager.Instance.nextLight();
                    nextLight = true;
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
    
    public void CheckPuzzlePiece()
    {
        for (int i = 0; i < spritePieceList.Count; ++i)
        {
            for(int x = 0; x < FinalArrangement.Length; x++)
            {
                if (spritePieceList[i].SpriteID == FinalArrangement[x])
                {
                    // Pass in final arrangement value cause it the animation id is the same as the final arrangement ids
                    spritePieceList[i].SetFrameState(FinalArrangement[x]);
                    break;
                }
                else
                {
                    // Else set it to 0 because thats none
                    spritePieceList[i].SetFrameState(0);
                }
            }
            if (i > FinalArrangement.Length)
                continue;
            // If the ID's match the correct final arrangement
           
        }
    }

    bool CheckNewPiece(int id)
    {
        if (spritePieceList[id].SpriteID == FinalArrangement[id])
            return true;
        return false;
    }

    /// <summary>
    /// Checks the status of the puzzle after every placement
    /// If it is completed, it'll play all the animations in sequence 
    /// If it isn't, it'll only play the correct animations
    /// </summary>
    /// <returns></returns>
    public bool CheckPuzzleStatus()
    {
        if (spritePieceList.Count < 6)
        {
            Completed = false;
            return false;
        }

        for(int i = 0; i < SpriteTablePieces.Length; ++i)
        {
            SpriteTablePiece spriteTableRef = SpriteTablePieces[i].GetComponent<SpriteTablePiece>();
            if (spriteTableRef != null)
            {
                if (spriteTableRef.correctPos != true)
                {
                    Completed = false;
                    return false;
                }
            }
        }

        //for (int i = 0; i < spritePieceList.Count; ++i)
        //{
        //    // If the ID's match the correct final arrangement
        //    if (spritePieceList[i].SpriteID != FinalArrangement[i])
        //    {
        //        // Pass in final arrangement value cause it the animation id is the same as the final arrangement ids
        //        Completed = false;
        //        return false;
        //    }
        //}

        SwapList();
        Completed = true;
        return true;
    }

    /// <summary>
    /// its just cause the old list is broken
    /// i relaly dont want to redo
    /// so im using this function to fix it 
    /// </summary>
    void SwapList()
    {
        spritePieceList.Clear();
        for(int i = 0; i < SpriteTablePieces.Length; ++i)
        {
            spritePieceList.Add(SpriteTablePieces[i].GetComponent<SpritePiece>());
        }
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

    public void CompletedPuzzle()
    {
        // Disable the movable script because it cant be grabbed anymore after completion
        for(int i = 0; i < spritePieceList.Count; ++i)
        {
            // Set all the states back to idle
            spritePieceList[i].SetFrameState(0);
            spritePieceList[i].spriteAnim.Play("InitState", 0, 0);
            // Disable the component so it cant be grabbed
            //spritePieceList[i].gameObject.GetComponent<Controllable_Movables>().enabled = false;
        }
    }

//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.gameObject.GetComponent<SpritePiece>() == null)
//            return;

//        Controllable_Movables spriteControl = other.gameObject.GetComponent<Controllable_Movables>();
//        SpritePiece spritePiece = other.gameObject.GetComponent<SpritePiece>();
//        // Loop through the bool array
//        // Check for empty slots
//        for(int i = 0; i < slotArray.Length; ++i)
//        {
//            if (slotArray[i] == true)
//                continue;
//            //End the grab forcefully
//            if(spriteControl.grabbedBy)
//              spriteControl.grabbedBy.GrabEnd();

//            //Set the new position and rotation
//            other.gameObject.transform.position = SpritePositions[i].transform.position;
//           // other.transform.parent = this.gameObject.transform;
//            other.gameObject.transform.rotation = SpritePositions[i].transform.rotation;

//            // Turn the object off
//            SpritePositions[i].SetActive(false);

//            spritePieceList.Add(spritePiece);

//            //Set up the sprite
//            spritePiece.PlacedInSlot(i);

//            slotArray[i] = true;

//            // If the piece is correct on placement
//            if (CheckNewPiece(i))
//            {
//                SoundManager.instance.playAudio("Correct");
//            }

//            // If the puzzle isn't completed yet
//            // Check each piece
//            if (CheckPuzzleStatus() == false)
//                CheckPuzzlePiece();
//            else
//                CompletedPuzzle();

//            break;
//        }
//    }

//    private void OnTriggerExit(Collider other)
//    {
//        SpritePiece spritePiece = other.GetComponent<SpritePiece>();
//        if (spritePiece == null)
//            return;

//        // Turn the object off
//        SpritePositions[spritePiece.GetSlodID()].SetActive(true);

//        // Set the slot to empty
//        slotArray[spritePiece.GetSlodID()] = false;
//        spritePieceList.Remove(spritePiece);
//    }
}
