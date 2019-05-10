using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanBuyTrigger : MonoBehaviour {

    public BuyUnitBehavior CanBuyVariable;

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Friendly"))
        {
            CanBuyVariable.CanBuy = true;
            Debug.Log("CanBuy: " + CanBuyVariable.CanBuy);
        }
    }

}
