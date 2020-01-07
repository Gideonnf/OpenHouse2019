using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HousePuzzle : MonoBehaviour
{
    List<GameObject> goArr_HousePieces;

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
        foreach (var go in goArr_HousePieces)
        {
            if (go.transform.localPosition != Vector3.zero)
                break;

            PuzzleLightManager.GetInstance().nextLight();
        }
    }
}
