using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritePiece : MonoBehaviour
{
    [Header("SpritePuzzle Componenets")]
    [Tooltip("Reference to which slot it is in")]
    int SlotID = 0;

    [Header("Sprite Piece Settings")]
    [Tooltip("Sprite ID is a reference to which keyframe this is")]
    public int SpriteID;
    [Tooltip("Animator Refrence")]
    public Animator spriteAnim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Set up and store any variables needed when the sprite is placed in the slot
    /// </summary>
    /// <param name="_slotID"></param>
    public void PlacedInSlot(int _slotID)
    {
        SlotID = _slotID;
    }

    /// <summary>
    /// Get the slote ID
    /// </summary>
    /// <returns></returns>
    public int GetSlodID()
    {
        return SlotID;
    }

    public void SetFrameState(int id)
    {
        spriteAnim.SetInteger("FrameState", id);
        spriteAnim.Play(spriteAnim.GetCurrentAnimatorStateInfo(0).fullPathHash, 0, 0f);
    }

    public void PauseAnimation()
    {
        spriteAnim.enabled = false;
    }

    public void ResumeAnimation()
    {
        spriteAnim.enabled = true;
    }

}
