using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenRefill : MonoBehaviour
{
    public GameObject gameManagerRef;
    //StockManagement stockInst = StockManagement.stockInstance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer != 10)
        {
            return;
        }
        else
        {
            Destroy(other.gameObject);
            gameManagerRef.GetComponent<StockManagement>().ChickenStock++;
            Debug.Log("Inside Refill :" + StockManagement.stockInstance.ChickenStock);

        }
    }
}
