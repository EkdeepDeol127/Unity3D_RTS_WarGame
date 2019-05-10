using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{

    private float DelayFire;
    private int GunClip;
    private GameObject tempEnemy;
    private SoliderBehavior SB;
    private TankBehavior TB;
    private PlaneBehavior PB;
    private EnemyMovement UM;
    private bool _InR;
    private bool _HT;
    private AudioSource _AS;
    public AudioClip _AC;
    public ParticleSystem muzzleFlash;
    private XmlManagerScript Data;
    private float SFXvolume;

    void Start()
    {
        Data = GetComponent<XmlManagerScript>();
        switch (gameObject.layer)
        {
            case 10:
                SB = GetComponent<SoliderBehavior>();
                DelayFire = 0.1f;
                GunClip = 31;
                break;
            case 11:
                TB = GetComponent<TankBehavior>();
                DelayFire = 0.5f;
                GunClip = 4;
                break;
            case 12:
                PB = GetComponent<PlaneBehavior>();
                DelayFire = 0.1f;
                GunClip = 100;
                break;
            default:
                break;
        }
        UM = GetComponent<EnemyMovement>();

        if (Data.LoadData())
        {
            SFXvolume = Data.tempHold.SoundEffectsVolume;
        }
        else
        {
            SFXvolume = 0.5f;
        }
        _AS = GetComponent<AudioSource>();

        _InR = false;
    }

    void Update()
    {
        if (DelayFire <= 0 && _InR == true && _HT == true)
        {
            VirtualBulletFire(tempEnemy);
            switch (gameObject.layer)
            {
                case 10:
                    DelayFire = 1f;
                    break;
                case 11:
                    DelayFire = 3f;
                    break;
                case 12:
                    DelayFire = 2f;
                    break;
                default:
                    break;
            }
        }
        else if (DelayFire > 0)
        {
            DelayFire -= Time.deltaTime;
        }
    }

    public void setEnemyAim(GameObject[] enemy)
    {
        for (int i = 0; i < enemy.Length; i++)
        {
            if (enemy[i] != null)
            {
                tempEnemy = enemy[i];
                _HT = true;
                break;
            }
        }
    }
    void VirtualBulletFire(GameObject Enemy)
    {
        if (Enemy.activeSelf != false)//mean if enemy is not dead
        {
            if (GunClip > 0)
            {
                GunClip--;
                muzzleFlash.Play();
                _AS.PlayOneShot(_AC, SFXvolume);
                switch (gameObject.layer)
                {
                    case 10:
                        SB.AccuracyCheck(Enemy);
                        break;
                    case 11:
                        TB.AccuracyCheck(Enemy);
                        break;
                    case 12:
                        PB.AccuracyCheck(Enemy);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (gameObject.layer)
                {
                    case 10:
                        DelayFire = 1.5f;
                        GunClip = 31;
                        break;
                    case 11:
                        DelayFire = 3.5f;
                        GunClip = 4;
                        break;
                    case 12:
                        DelayFire = 2.5f;
                        GunClip = 100;
                        break;
                    default:
                        break;
                }
                muzzleFlash.Stop();
            }
        }
        else
        {
            muzzleFlash.Stop();
            UM.removeUnitID(Enemy);
            UM.reCheckArray();
        }
    }

    public void setInRange(bool InRange)
    {
        _InR = InRange;
    }

    public void KilledEnemy()
    {
        _HT = false;
    }
}
