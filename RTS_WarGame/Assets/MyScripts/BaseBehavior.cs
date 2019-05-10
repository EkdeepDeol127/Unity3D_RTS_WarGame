using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseBehavior : MonoBehaviour
{
    public float Health;
    public float Armor;
    public Transform ArmorBar;
    public Transform HealthBar;

    void Start()//Add stats for units and anything that BOTH Player and Enemy soliders need.
    {
        this.gameObject.name = this.gameObject.GetInstanceID().ToString();
        Health = 100;
        Armor = 150;
    }

    public void TakeDamage(float damage, GameObject enemy)
    {
        if (Armor <= 0)
        {
            if (Armor < 0)//just if armor is negative, then apply to health
            {
                Health += Armor;
                Armor = 0;
            }
            if (Health > 0)
            {
                Health -= damage;
                HealthBar.localScale = new Vector2((Health / 1000), 1);
                if (Health <= 0)
                {
                    Health = 0;
                    if(gameObject.tag == "Friendly")
                    {
                        SceneManager.LoadScene("LoseScreen");
                    }
                    else if(gameObject.tag == "Enemy")
                    {
                        SceneManager.LoadScene("WinScreen");
                    }
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
        if (Health > 1000)
        {
            Health = 1000;
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
}