using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    protected int health, maxHealth, speed;
    protected string team;
    protected string type;
    protected bool alive = true;
    public int Health
    {
        get => health;
        set => health = value;
    }

    public int MaxHealth
    {
        get => maxHealth;
    }

    public string Team
    {
        get => team;
    }
    // Start is called before the first frame update
    void Start()
    {
        switch (name)
        {
            case "ResourceBuildingBlue(Clone)":
                SetType("Resource");
                team = "Blue";
                break;
            case "ResourceBuildingRed(Clone)":
                SetType("Resource");
                team = "Red";
                break;
            case "FactoryBuildingBlue(Clone)":
                SetType("Factory");
                team = "Blue";
                break;
            case "FactoryBuildingRed(Clone)":
                SetType("Factory");
                team = "Red";
                break;
            default:
                Debug.Log("Defaulted Type Switch With Name " + name);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {
        Destroy(gameObject);
        alive = false;
    }

    public void SetType(string type)
    {
        if (type == "Resource")
        {
            maxHealth = 50;
            speed = 1;
            this.type = type;
        }
        else
        {
            maxHealth = 35;
            speed = 5;
            this.type = type;
        }
        health = maxHealth;
    }
}
