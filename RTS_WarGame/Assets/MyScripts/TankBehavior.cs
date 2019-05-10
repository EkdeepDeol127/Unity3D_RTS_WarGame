using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankBehavior : MonoBehaviour
{
    public float Health;
    public float Armor;
    public float Range;
    public float Speed;
    public float Accuracy;
    public float Damage;
    public Transform ArmorBar;
    public Transform HealthBar;

    void Start()//Add stats for units and anything that BOTH Player and Enemy Tanks need.
    {
        this.gameObject.name = this.gameObject.GetInstanceID().ToString();
        Health = 1500;
        Armor = 1500;
        Range = 30;//not being used
        Speed = 10;
        Accuracy = 0.0f;
        Damage = 150;
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
                HealthBar.localScale = new Vector2((Health / 1500), 1);
                if(Health <= 0)
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
            ArmorBar.localScale = new Vector2((Armor / 1500), 1);
        }
    }

    public void AddHealth(float health)
    {
        Health += health;
        if (Health > 1500)
        {
            Health = 1500;
        }
    }

    public void AddArmor(float armor)
    {
        Armor += armor;
        if (Armor > 1500)
        {
            Armor = 1500;
        }
    }

    public void AccuracyCheck(GameObject enemy)
    {
        float tempAim = Random.Range(0.4f, 1f);
        float tempHlth = (Health / 1500);
        Accuracy = tempHlth;
        if (Accuracy < 0.4f)//base accuracy setter
        {
            Accuracy = 0.4f;
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
        float tempHlth = (Health / 150);
        Speed = tempHlth;
        if (Speed < 3f)//base Speed setter
        {
            Speed = 3f;
        }
        return Speed;
    }
}
