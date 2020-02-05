using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HousePuzzle : MonoBehaviour
{
    public GameObject puzzleLight;

    List<GameObject> goArr_HousePieces;

    bool Completed = false;

    // Start is called before the first frame update
    void Start()
    {
        goArr_HousePieces = new List<GameObject>();

        for (int i = 0; i < this.transform.childCount; ++i)
        {
            if (this.transform.GetChild(i).gameObject.GetComponent<HousePiece>() == null)
                continue;

            goArr_HousePieces.Add(this.transform.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(CheckPuzzle())
        {
            if (Completed == false)
            {
                PuzzleLightManager.Instance.nextLight();
                Debug.Log("Puzzle completed");
                Completed = true;
            }
        }

    }

    bool CheckPuzzle()
    {
        foreach (var go in goArr_HousePieces)
        {
            if (go.GetComponent<HousePiece>().correctObject == false)
                return false;

        }

        return true;
    }
}
