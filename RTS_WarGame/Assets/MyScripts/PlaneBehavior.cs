using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaneBehavior : MonoBehaviour
{
    public float Health;
    public float Armor;
    public float Range;
    public float Speed;
    public float Accuracy;
    public float Damage;
    public Transform ArmorBar;
    public Transform HealthBar;

    void Start()//Add stats for units and anything that BOTH Player and Enemy Planes need.
    {
        this.gameObject.name = this.gameObject.GetInstanceID().ToString();
        Health = 500;
        Armor = 500;
        Range = 20;//not being used
        Speed = 15;
        Accuracy = 0.0f;
        Damage = 20;
    }

    public void TakeDamage(float damage, GameObject enemy)
    {
        if(Armor <= 0)
        {
            if(Armor < 0)//just if armor is negative, then apply to health
            {
                Health += Armor;
                Armor = 0;
            }
            if(Health > 0)
            {
                Health -= damage;
                HealthBar.localScale = new Vector2((Health / 100), 1);
                if (Health <= 0)
                {
                    Health = 0;
                    this.transform.position = new Vector3(0, -500, 0);
                    this.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            Armor -= damage;
            ArmorBar.localScale = new Vector2((Armor / 500), 1);
        }
    }

    public void AddHealth(float health)
    {
        Health += health;
        if (Health > 500)
        {
            Health = 500;
        }
    }

    public void AddArmor(float armor)
    {
        Armor += armor;
        if (Armor > 500)
        {
            Armor = 500;
        }
    }

    public void AccuracyCheck(GameObject enemy)
    {
        float tempAim = Random.Range(0.5f, 1f);
        float tempHlth = (Health / 500);
        Accuracy = tempHlth;
        if (Accuracy < 0.5f)//base accuracy setter
        {
            Accuracy = 0.5f;
        }
        if (tempAim <= Accuracy)
        {
            switch (enemy.layer)
            {
                case 10:
                    enemy.GetComponent<SoliderBehavior>().TakeDamage(Damage, enemy);
                    break;
                case 11:
                    enemy.GetComponent<TankBehavior>().TakeDamage(Damage, enemy);
                    break;
                case 12:
                    enemy.GetComponent<PlaneBehavior>().TakeDamage(Damage, enemy);
                    break;
                case 13:
                    enemy.GetComponent<BaseBehavior>().TakeDamage(Damage, enemy);
                    break;
                default:
                    break;
            }
        }
    }

    public float SpeedCheck()
    {
        float tempHlth = (Health / 20);
        Speed = tempHlth;
        if (Speed < 10f)//base Speed setter
        {
            Speed = 10f;
        }
        return Speed;
    }
}
