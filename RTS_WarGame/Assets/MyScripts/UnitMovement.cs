using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Camera cam;
    public GameObject[] targetHold;
    public GameObject[] Squad;
    private UnitShoot UnitShootScript;
    private SoliderBehavior SB;
    private TankBehavior TB;
    private PlaneBehavior PB;
    private int checks;
    public Vector2 ScreenPos;
    public bool OnScreen;
    public bool Selected;
    public UnitSelect _unitSelect;
    private AudioSource _AS;
    public AudioClip _AC;
    private XmlManagerScript Data;
    private float SFXvolume;

    private void Start()
    {
        Data = GetComponent<XmlManagerScript>();
        cam = Camera.main;
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
        UnitShootScript = this.GetComponent<UnitShoot>();
        _unitSelect = FindObjectOfType<UnitSelect>();

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
        Selected = false;
    }

    private void Update()
    {
        if(!Selected)
        {
            ScreenPos = cam.WorldToScreenPoint(this.transform.position);
            if(_unitSelect.InScreenSpace(ScreenPos))
            {
                if(!OnScreen)
                {
                    _unitSelect.UnitsOnScreen.Add(this.gameObject);
                    OnScreen = true;
                }
            }
            else
            {
                if(OnScreen)
                {
                    _unitSelect.removeUnitFromArray(this.gameObject);
                }
            }
        }

        //if(agent.isStopped == true && gameObject.layer == 12)//if i want to animate the plane around the target if attacking
        //{
        //    transform.RotateAround()?
        //}
    }

    void OnTriggerEnter(Collider other)//tells squad that we have found a target
    {
        if (other.tag == "Enemy")
        {
            for (int i = 0; i < Squad.Length; i++)
            {
                if(Squad[i] != null && Squad[i].activeSelf == true)
                {
                    switch (other.gameObject.layer)
                    {
                        case 10://if enemy is solider
                            switch (Squad[i].gameObject.layer)//then everyone can attack it
                            {
                                case 10:
                                    Squad[i].GetComponent<UnitMovement>().addUnitID(other.GetComponent<EnemyMovement>().Squad);
                                    Squad[i].GetComponent<UnitShoot>().setInRange(true);
                                    Squad[i].GetComponent<UnitShoot>().setEnemyAim(targetHold);
                                    Squad[i].GetComponent<UnitMovement>().agent.updateRotation = false;
                                    Squad[i].GetComponent<UnitMovement>().agent.isStopped = true;
                                    if (this.agent.updateRotation == true)
                                    {
                                        Squad[i].transform.LookAt(other.gameObject.transform);
                                    }
                                    break;
                                case 11:
                                    Squad[i].GetComponent<UnitMovement>().addUnitID(other.GetComponent<EnemyMovement>().Squad);
                                    Squad[i].GetComponent<UnitShoot>().setInRange(true);
                                    Squad[i].GetComponent<UnitShoot>().setEnemyAim(targetHold);
                                    Squad[i].GetComponent<UnitMovement>().agent.updateRotation = false;
                                    Squad[i].GetComponent<UnitMovement>().agent.isStopped = true;
                                    if (this.agent.updateRotation == true)
                                    {
                                        Squad[i].transform.LookAt(other.gameObject.transform);
                                    }
                                    break;
                                case 12:
                                    Squad[i].GetComponent<UnitMovement>().addUnitID(other.GetComponent<EnemyMovement>().Squad);
                                    Squad[i].GetComponent<UnitShoot>().setInRange(true);
                                    Squad[i].GetComponent<UnitShoot>().setEnemyAim(targetHold);
                                    Squad[i].GetComponent<UnitMovement>().agent.updateRotation = false;
                                    Squad[i].GetComponent<UnitMovement>().agent.isStopped = true;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case 11://if enemy is Tank
                            switch (Squad[i].gameObject.layer)//then everyone can attack it
                            {
                                case 10:
                                    Squad[i].GetComponent<UnitMovement>().addUnitID(other.GetComponent<EnemyMovement>().Squad);
                                    Squad[i].GetComponent<UnitShoot>().setInRange(true);
                                    Squad[i].GetComponent<UnitShoot>().setEnemyAim(targetHold);
                                    Squad[i].GetComponent<UnitMovement>().agent.updateRotation = false;
                                    Squad[i].GetComponent<UnitMovement>().agent.isStopped = true;
                                    if (this.agent.updateRotation == true)
                                    {
                                        Squad[i].transform.LookAt(other.gameObject.transform);
                                    }
                                    break;
                                case 11:
                                    Squad[i].GetComponent<UnitMovement>().addUnitID(other.GetComponent<EnemyMovement>().Squad);
                                    Squad[i].GetComponent<UnitShoot>().setInRange(true);
                                    Squad[i].GetComponent<UnitShoot>().setEnemyAim(targetHold);
                                    Squad[i].GetComponent<UnitMovement>().agent.updateRotation = false;
                                    Squad[i].GetComponent<UnitMovement>().agent.isStopped = true;
                                    if (this.agent.updateRotation == true)
                                    {
                                        Squad[i].transform.LookAt(other.gameObject.transform);
                                    }
                                    break;
                                case 12:
                                    Squad[i].GetComponent<UnitMovement>().addUnitID(other.GetComponent<EnemyMovement>().Squad);
                                    Squad[i].GetComponent<UnitShoot>().setInRange(true);
                                    Squad[i].GetComponent<UnitShoot>().setEnemyAim(targetHold);
                                    Squad[i].GetComponent<UnitMovement>().agent.updateRotation = false;
                                    Squad[i].GetComponent<UnitMovement>().agent.isStopped = true;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case 12://if enemy is Plane
                            if (Squad[i].gameObject.layer == 12)//then only plane can attack it
                            {
                                Squad[i].GetComponent<UnitMovement>().addUnitID(other.GetComponent<EnemyMovement>().Squad);
                                Squad[i].GetComponent<UnitShoot>().setInRange(true);
                                Squad[i].GetComponent<UnitShoot>().setEnemyAim(targetHold);
                                Squad[i].GetComponent<UnitMovement>().agent.updateRotation = false;
                                Squad[i].GetComponent<UnitMovement>().agent.isStopped = true;
                            }
                            break;
                        case 13://if enemy is Base
                            Squad[i].GetComponent<UnitMovement>().addUnitID(other.gameObject);
                            Squad[i].GetComponent<UnitShoot>().setInRange(true);
                            Squad[i].GetComponent<UnitShoot>().setEnemyAim(targetHold);
                            Squad[i].GetComponent<UnitMovement>().agent.updateRotation = false;
                            Squad[i].GetComponent<UnitMovement>().agent.isStopped = true;
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

    public void SetunitDestination(RaycastHit hit)
    {
        for (int i = 0; i < Squad.Length; i++)
        {
            if (Squad[i] != null && Squad[i].activeSelf == true)
            {
                switch (Squad[i].gameObject.layer)
                {
                    case 10:
                        Squad[i].GetComponent<UnitMovement>().agent.speed = SB.SpeedCheck();
                        break;
                    case 11:
                        Squad[i].GetComponent<UnitMovement>().agent.speed = TB.SpeedCheck();
                        break;
                    case 12:
                        Squad[i].GetComponent<UnitMovement>().agent.speed = PB.SpeedCheck();
                        break;
                    default:
                        break;
                }
                Squad[i].GetComponent<UnitMovement>().agent.updateRotation = true;
                Squad[i].GetComponent<UnitMovement>().agent.isStopped = false;
                Squad[i].GetComponent<UnitShoot>().setInRange(false);
                Squad[i].GetComponent<UnitMovement>().agent.SetDestination(hit.point);
                _AS.PlayOneShot(_AC, SFXvolume);
            }
        }
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
        if(exists == false)
        {
            for(int j = 0; j < other.Length; j++)
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
        if(exists == false)
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
            else if (checks == targetHold.Length)
            {
                for (int i = 0; i < Squad.Length; i++)
                {
                    if (Squad[i] != null && Squad[i].activeSelf == true)
                    {
                        switch (Squad[i].gameObject.layer)
                        {
                            case 10:
                                Squad[i].GetComponent<UnitMovement>().agent.speed = SB.SpeedCheck();
                                break;
                            case 11:
                                Squad[i].GetComponent<UnitMovement>().agent.speed = TB.SpeedCheck();
                                break;
                            case 12:
                                Squad[i].GetComponent<UnitMovement>().agent.speed = PB.SpeedCheck();
                                break;
                            default:
                                break;
                        }
                        Squad[i].GetComponent<UnitShoot>().setInRange(false);
                        Squad[i].GetComponent<UnitShoot>().KilledEnemy();
                        Squad[i].GetComponent<UnitMovement>().agent.updateRotation = true;
                        Squad[i].GetComponent<UnitMovement>().agent.isStopped = false;
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
}
