using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyUnitBehavior : MonoBehaviour {

    public GameObject Solider;
    public GameObject Tank;
    public GameObject Plane;
    public Transform SpawnLocation;

    public Text CashText;
    public Button BuySolider;
    public Button BuyTank;
    public Button BuyPlane;

    private float CashAmount;
    private float timer;
    private float BuyTimer;
    public bool CanBuy;
    public bool BuyNow;

    void Start ()
    {
        BuySolider.onClick.AddListener(SpawnSolider);
        BuyTank.onClick.AddListener(SpawnTank);
        BuyPlane.onClick.AddListener(SpawnPlane);

        CanBuy = true;
        BuyNow = true;
        CashAmount = 0;
        CashText.text = "Cash: " + CashAmount;
        timer = 3f;
	}
	

	void Update ()
    {
        if (timer <= 0)
        {
            CashAmount += 50;
            CashText.text = "Cash: " + CashAmount;
            timer = 3f;
        }
        else
        {
            timer -= Time.deltaTime;
        }

        if(BuyTimer <= 0 && BuyNow == false)
        {
            BuyNow = true;
        }
        else
        {
            BuyTimer -= Time.deltaTime;
        }
	}

    void SpawnSolider()
    {
        if(CashAmount >= 100 && CanBuy == true && BuyNow == true)
        {
            Instantiate(Solider, SpawnLocation.position, SpawnLocation.rotation);
            CashAmount -= 100;
            CashText.text = "Cash: " + CashAmount;
            CanBuy = false;
            BuyNow = false;
            BuyTimer = 10;
        }
    }

    void SpawnTank()
    {
        if (CashAmount >= 300 && CanBuy == true && BuyNow == true)
        {
            Instantiate(Tank, SpawnLocation.position, SpawnLocation.rotation);
            CashAmount -= 300;
            CashText.text = "Cash: " + CashAmount;
            CanBuy = false;
            BuyNow = false;
            BuyTimer = 10;
        }
    }

    void SpawnPlane()
    {
        if (CashAmount >= 500 && CanBuy == true && BuyNow == true)
        {
            Instantiate(Plane, SpawnLocation.position, SpawnLocation.rotation);
            CashAmount -= 500;
            CashText.text = "Cash: " + CashAmount;
            CanBuy = false;
            BuyNow = false;
            BuyTimer = 10;
        }
    }
}
