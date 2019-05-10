using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {//Add AI Behavior

    private NavMeshAgent agent;
    public GameObject[] player;
    public GameObject[] targetHold;
    public GameObject[] Squad;
    public Transform target;
    private EnemyShoot UnitShootScript;
    private SoliderBehavior SB;
    private TankBehavior TB;
    private PlaneBehavior PB;
    private int checks;
    private bool once;
    private AudioSource _AS;
    public AudioClip _AC;
    private XmlManagerScript Data;
    private float SFXvolume;

    private void Start()
    {
        Data = GetComponent<XmlManagerScript>();
        switch (gameObject.layer)
        {
            case 10:
                SB = GetComponent<SoliderBehavior>();
                agent = GetComponent<NavMeshAgent>();
                agent.SetDestination(GetComponent<Transform>().position);
                agent.speed = SB.SpeedCheck();
                break;
            case 11:
                TB = GetComponent<TankBehavior>();
                agent = GetComponent<NavMeshAgent>();
                agent.SetDestination(GetComponent<Transform>().position);
                agent.speed = TB.SpeedCheck();
                break;
            case 12:
                PB = GetComponent<PlaneBehavior>();
                agent = gameObject.GetComponentInParent<NavMeshAgent>();
                agent.SetDestination(GetComponent<Transform>().position);
                agent.speed = PB.SpeedCheck();
                break;
            default:
                break;
        }
        UnitShootScript = this.GetComponent<EnemyShoot>();

        if (Data.LoadData())
        {
            SFXvolume = Data.tempHold.SoundEffectsVolume;
        }
        else
        {
            SFXvolume = 0.5f;
        }
        _AS = GetComponent<AudioSource>();

        targetHold = new GameObject[20];
        checks = 0;
        once = false;
        FindTarget();
    }

    private void Update()
    {
        if(once == false)
        {
            once = true;
            switch (gameObject.layer)
            {
                case 10:
                    agent.speed = SB.SpeedCheck();
                    break;
                case 11:
                    agent.speed = TB.SpeedCheck();
                    break;
                case 12:
                    agent.speed = PB.SpeedCheck();
                    break;
                default:
                    break;
            }
        }

        if(Vector3.Distance(agent.destination, this.transform.position) < 10)
        {
            for (int i = 0; i < Squad.Length; i++)
            {
                if (Squad[i] != null && Squad[i].activeSelf == true)
                {
                    Squad[i].GetComponent<EnemyMovement>().FindTarget();
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)//tells squad that we have found a target
    {
        if (other.tag == "Friendly")
        {
            for (int i = 0; i < Squad.Length; i++)
            {
                if (Squad[i] != null && Squad[i].activeSelf == true)
                {
                    switch (other.gameObject.layer)
                    {
                        case 10://if enemy is solider
                            switch (Squad[i].gameObject.layer)//then everyone can attack it
                            {
                                case 10:
                                    Squad[i].GetComponent<EnemyMovement>().addUnitID(other.GetComponent<UnitMovement>().Squad);
                                    Squad[i].GetComponent<EnemyShoot>().setInRange(true);
                                    Squad[i].GetComponent<EnemyShoot>().setEnemyAim(targetHold);
                                    Squad[i].GetComponent<EnemyMovement>().agent.updateRotation = false;
                                    Squad[i].GetComponent<EnemyMovement>().agent.isStopped = true;
                                    if (this.agent.updateRotation == true)
                                    {
                                        Squad[i].transform.LookAt(other.gameObject.transform);
                                    }
                                    break;
                                case 11:
                                    Squad[i].GetComponent<EnemyMovement>().addUnitID(other.GetComponent<UnitMovement>().Squad);
                                    Squad[i].GetComponent<EnemyShoot>().setInRange(true);
                                    Squad[i].GetComponent<EnemyShoot>().setEnemyAim(targetHold);
                                    Squad[i].GetComponent<EnemyMovement>().agent.updateRotation = false;
                                    Squad[i].GetComponent<EnemyMovement>().agent.isStopped = true;
                                    if (this.agent.updateRotation == true)
                                    {
                                        Squad[i].transform.LookAt(other.gameObject.transform);
                                    }
                                    break;
                                case 12:
                                    Squad[i].GetComponent<EnemyMovement>().addUnitID(other.GetComponent<UnitMovement>().Squad);
                                    Squad[i].GetComponent<EnemyShoot>().setInRange(true);
                                    Squad[i].GetComponent<EnemyShoot>().setEnemyAim(targetHold);
                                    Squad[i].GetComponent<EnemyMovement>().agent.updateRotation = false;
                                    Squad[i].GetComponent<EnemyMovement>().agent.isStopped = true;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case 11://if enemy is Tank
                            switch (Squad[i].gameObject.layer)//then everyone can attack it
                            {
                                case 10:
                                    Squad[i].GetComponent<EnemyMovement>().addUnitID(other.GetComponent<UnitMovement>().Squad);
                                    Squad[i].GetComponent<EnemyShoot>().setInRange(true);
                                    Squad[i].GetComponent<EnemyShoot>().setEnemyAim(targetHold);
                                    Squad[i].GetComponent<EnemyMovement>().agent.updateRotation = false;
                                    Squad[i].GetComponent<EnemyMovement>().agent.isStopped = true;
                                    if (this.agent.updateRotation == true)
                                    {
                                        Squad[i].transform.LookAt(other.gameObject.transform);
                                    }
                                    break;
                                case 11:
                                    Squad[i].GetComponent<EnemyMovement>().addUnitID(other.GetComponent<UnitMovement>().Squad);
                                    Squad[i].GetComponent<EnemyShoot>().setInRange(true);
                                    Squad[i].GetComponent<EnemyShoot>().setEnemyAim(targetHold);
                                    Squad[i].GetComponent<EnemyMovement>().agent.updateRotation = false;
                                    Squad[i].GetComponent<EnemyMovement>().agent.isStopped = true;
                                    if (this.agent.updateRotation == true)
                                    {
                                        Squad[i].transform.LookAt(other.gameObject.transform);
                                    }
                                    break;
                                case 12:
                                    Squad[i].GetComponent<EnemyMovement>().addUnitID(other.GetComponent<UnitMovement>().Squad);
                                    Squad[i].GetComponent<EnemyShoot>().setInRange(true);
                                    Squad[i].GetComponent<EnemyShoot>().setEnemyAim(targetHold);
                                    Squad[i].GetComponent<EnemyMovement>().agent.updateRotation = false;
                                    Squad[i].GetComponent<EnemyMovement>().agent.isStopped = true;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case 12://if enemy is Plane
                            if(Squad[i].gameObject.layer == 12)//then only plane can attack it
                            {
                                Squad[i].GetComponent<EnemyMovement>().addUnitID(other.GetComponent<UnitMovement>().Squad);
                                Squad[i].GetComponent<EnemyShoot>().setInRange(true);
                                Squad[i].GetComponent<EnemyShoot>().setEnemyAim(targetHold);
                                Squad[i].GetComponent<EnemyMovement>().agent.updateRotation = false;
                                Squad[i].GetComponent<EnemyMovement>().agent.isStopped = true;
                            }
                            break;
                        case 13://if enemy is Base
                            Squad[i].GetComponent<EnemyMovement>().addUnitID(other.gameObject);
                            Squad[i].GetComponent<EnemyShoot>().setInRange(true);
                            Squad[i].GetComponent<EnemyShoot>().setEnemyAim(targetHold);
                            Squad[i].GetComponent<EnemyMovement>().agent.updateRotation = false;
                            Squad[i].GetComponent<EnemyMovement>().agent.isStopped = true;
                            if (this.agent.updateRotation == true)
                            {
                                Squad[i].transform.LookAt(other.gameObject.transform);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        removeUnitID(other.gameObject);
        reCheckArray();
    }

    public void addUnitID(GameObject[] other)
    {
        bool exists = false;
        for (int j = 0; j < other.Length; j++)
        {
            for (int i = 0; i < targetHold.Length; i++)
            {
                if (targetHold[i] != null && targetHold[i].gameObject.GetInstanceID() == other[j].GetInstanceID())//checks if already in array
                {
                    exists = true;
                }
            }
        }
        if (exists == false)
        {
            for (int j = 0; j < other.Length; j++)
            {
                for (int i = 0; i < targetHold.Length; i++)
                {
                    if (targetHold[i] == null)
                    {
                        targetHold[i] = other[j];
                        break;
                    }
                }
            }
        }
    }

    public void addUnitID(GameObject other)
    {
        bool exists = false;
        for (int i = 0; i < targetHold.Length; i++)
        {
            if (targetHold[i] != null && targetHold[i].gameObject.GetInstanceID() == other.GetInstanceID())//checks if already in array
            {
                exists = true;
            }
        }
        if (exists == false)
        {
            for (int i = 0; i < targetHold.Length; i++)
            {
                if (targetHold[i] == null)
                {
                    targetHold[i] = other;
                    break;
                }
            }
        }
    }

    public void removeUnitID(GameObject other)
    {
        for (int i = 0; i < targetHold.Length; i++)//clears object from array
        {
            if (targetHold[i] != null)
            {
                if (targetHold[i].gameObject.GetInstanceID() == other.GetInstanceID())
                {
                    targetHold[i] = null;
                    break;
                }
            }
        }
    }

    public void reCheckArray()//clear the array of all unactive objects
    {
        for (int i = 0; i < targetHold.Length; i++)
        {
            if (targetHold[i] != null && targetHold[i].activeSelf == false)
            {
                targetHold[i] = null;
            }
        }
        for (int j = 0; j < targetHold.Length; j++)//look for any targets are still left
        {
            if (targetHold[j] == null)//checks if its all empty
            {
                checks++;
            }
            if (targetHold[j] != null && targetHold[j].activeSelf == true)
            {
                UnitShootScript.setEnemyAim(targetHold);
                break;
            }
            else if (checks == targetHold.Length)//means if array empty
            {
                for (int i = 0; i < Squad.Length; i++)
                {
                    if (Squad[i] != null && Squad[i].activeSelf == true)
                    {
                        switch (Squad[i].gameObject.layer)
                        {
                            case 10:
                                Squad[i].GetComponent<EnemyMovement>().agent.speed = SB.SpeedCheck();
                                break;
                            case 11:
                                Squad[i].GetComponent<EnemyMovement>().agent.speed = TB.SpeedCheck();
                                break;
                            case 12:
                                Squad[i].GetComponent<EnemyMovement>().agent.speed = PB.SpeedCheck();
                                break;
                            default:
                                break;
                        }
                        Squad[i].GetComponent<EnemyShoot>().setInRange(false);
                        Squad[i].GetComponent<EnemyShoot>().KilledEnemy();
                        Squad[i].GetComponent<EnemyMovement>().agent.updateRotation = true;
                        Squad[i].GetComponent<EnemyMovement>().agent.isStopped = false;
                    }
                }
                checks = 0;
                break;
            }
        }
    }

    public void SquadCheck()//clears squad of dead friends
    {
        for (int i = 0; i < Squad.Length; i++)
        {
            if (Squad[i] != null && Squad[i].activeSelf == false)
            {
                Squad[i] = null;
            }
        }
    }

    public void FindTarget()
    {
        player = GameObject.FindGameObjectsWithTag("Friendly");
        for (int i = 0; i < player.Length; i++)
        {
            if(player[i].activeSelf != false)
            {
                switch (player[i].layer)//picks which enemy it should engage
                {
                    case 10://solider
                        if (this.gameObject.layer == 10 || this.gameObject.layer == 11)//checks if itself is a solider/tank
                        {
                            target = player[i].transform;
                            agent.SetDestination(target.position);
                            Debug.Log("Targeting Solider");
                        }
                        break;
                    case 11://tank
                        if (this.gameObject.layer == 11 || this.gameObject.layer == 10)//checks if itself is a tank/plane
                        {
                            target = player[i].transform;
                            agent.SetDestination(target.position);
                            Debug.Log("Targeting Tank");
                        }
                        break;
                    case 12://plane
                        if (this.gameObject.layer == 12)//checks if itself is a plane
                        {
                            target = player[i].transform;
                            agent.SetDestination(target.position);
                            Debug.Log("Targeting Plane");
                        }
                        break;
                    case 13://Base
                            target = player[i].transform;
                            agent.SetDestination(target.position);
                            Debug.Log("Targeting Base");
                        break;
                    default:
                        Debug.Log("No Enemy");
                        break;
                }
                if (target != null)
                {
                    break;
                }
            }
        }
        _AS.PlayOneShot(_AC, SFXvolume);
    }
}
