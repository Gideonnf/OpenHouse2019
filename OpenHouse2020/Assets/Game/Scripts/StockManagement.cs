using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StockManagement : MonoBehaviour
{
    public static StockManagement stockInstance;
    // Keeps trackof the number of food stock
    // Ideally, will be seperated into different types
    public float ChickenStock = 5;
    public float VegetableStock = 5;
    public GameObject ChickenCanvas;
    public GameObject VegetableCanvas;

    private void Awake()
    {
        //if (stockInstance != null && stockInstance != this)
        //    Destroy(stockInstance);
        //else
        //    stockInstance = this;
       
    }

    // Start is called before the first frame update
    void Start()
    {
        ChickenCanvas.GetComponentInChildren<Text>().text = ChickenStock.ToString();
        VegetableCanvas.GetComponentInChildren<Text>().text = VegetableStock.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(ChickenStock);
        ChickenCanvas.GetComponentInChildren<Text>().text = ChickenStock.ToString();
        VegetableCanvas.GetComponentInChildren<Text>().text = VegetableStock.ToString();
    }

    public void incrementChickenStock()
    {
        ChickenStock++;
    }

    public void incrementVegStock()
    {
        VegetableStock++;
    }
}
