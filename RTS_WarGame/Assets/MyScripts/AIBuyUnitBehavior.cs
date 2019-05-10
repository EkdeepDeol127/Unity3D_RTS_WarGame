using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBuyUnitBehavior : MonoBehaviour {

    public GameObject Solider;
    public GameObject Tank;
    public GameObject Plane;
    public Transform SpawnLocation;

    private float CashAmount;
    private float timer;
    private float Buytimer;
    private int SoliderAmount;
    private int TankAmount;
    private int PlaneAmount;

    void Start ()
    {
        SoliderAmount = 0;
        TankAmount = 0;
        PlaneAmount = 0;
        CashAmount = 0;
        timer = 5f;
        Buytimer = 5f;
	}
	

	void Update ()
    {
        if (timer <= 0)
        {
            CashAmount += 50;
            timer = 5f;
        }
        else
        {
            timer -= Time.deltaTime;
        }
        if(Buytimer <= 0)
        {
            CheckBuy();
        }
        else
        {
            Buytimer -= Time.deltaTime;
        }
    }

    void CheckBuy()
    {
        if(SoliderAmount < 3 && CashAmount >= 100)
        {
            Instantiate(Solider, SpawnLocation.position, SpawnLocation.rotation);
            SoliderAmount++;
            CashAmount -= 100;
            Buytimer = 10;
        }
        else if(TankAmount < 2 && CashAmount >= 300)
        {
            Instantiate(Tank, SpawnLocation.position, SpawnLocation.rotation);
            TankAmount++;
            CashAmount -= 300;
            Buytimer = 15;
        }
        else if(PlaneAmount < 1 && CashAmount >= 500)
        {
            Instantiate(Plane, SpawnLocation.position, SpawnLocation.rotation);
            PlaneAmount++;
            CashAmount -= 500;
            Buytimer = 15;
        }
        else if(SoliderAmount == 3 && TankAmount == 2 && PlaneAmount == 1)
        {
            SoliderAmount = 0;
            TankAmount = 0;
            PlaneAmount = 0;
            Buytimer = 5;
        }
        else
        {
            Buytimer = 5;
        }
    }
}
