using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] int health, maxHealth, speed, atk;
    [SerializeField] string team;
    [SerializeField] string type;
    [SerializeField] float range;
    [SerializeField] bool alive = true;

    GameObject thisUnit;

    float distance, tempDistance;
    Unit target, targeted;
    Building targetB, targetedB;

    public int Health
    {
        get => health;
        set => health = value;
    }

    public string Team
    {
        get => team;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Name is "+name);
        switch (name)
        {
            case "MeleeUnitBlue(Clone)":
                SetType("Knight");
                team = "Blue";
                break;
            case "MeleeUnitRed(Clone)":
                SetType("Knight");
                team = "Red";
                break;
            case "RangedUnitBlue(Clone)":
                SetType("Archer");
                team = "Blue";
                break;
            case "RangedUnitRed(Clone)":
                SetType("Archer");
                team = "Red";
                break;
            case "Wizard(Clone)":
                SetType("Wizard");
                team = "Wizard";
                break;
            default:
                Debug.Log("Defaulted Type Switch With Name "+name);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetTarget();
        
        if(targetedB != null)
        {
            tempDistance = (targetedB.transform.position - transform.position).magnitude;
            if (tempDistance <= range)
            {
                Attack(targetedB);
            }
            else
            {
                Move(targetedB);
            }
        }
        else if(targeted != null)
        {
            tempDistance = (targeted.transform.position - transform.position).magnitude;
            if (tempDistance <= range)
            {
                Attack(targeted);
            }
            else
            {
                Move(targeted);
            }
        }
        else
        {
            Debug.Log("No Valid Targets Found");
        }

        WithinBounds();
    }

    void GetTarget()
    {
        GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
        GameObject[] buildings = GameObject.FindGameObjectsWithTag("Building");

        distance = float.MaxValue;
        targeted = null;
        targetedB = null;

        if(buildings!=null)
            foreach (GameObject building in buildings)
            {
                targetB = building.GetComponent<Building>();

                if (targetB == null || targetB.Team == Team)
                {
                    Debug.Log("my team "+team+" target team "+targetB.Team);
                    continue;
                }

                tempDistance = (building.transform.position - transform.position).magnitude;

                if (tempDistance < distance)
                {
                    Debug.Log("Target Building Aquired");
                    targetedB = targetB;
                    distance = tempDistance;
                }
            }

        if(units!=null)
            foreach (GameObject unit in units)
            {
                target = unit.GetComponent<Unit>();

                if (target == null || target.Team == Team)
                {
                    continue;
                }

                tempDistance = (unit.transform.position - transform.position).magnitude;

                if (tempDistance < distance)
                {
                    targeted = target;
                    distance = tempDistance;
                }
            }
    }

    void Move(Unit target)
    {
        int xDist, zDist, moves;

        xDist = (int)Mathf.Abs(target.transform.position.x - transform.position.x);
        zDist = (int)Mathf.Abs(target.transform.position.z - transform.position.z);

        if(Mathf.Abs(xDist) >= Mathf.Abs(zDist))
        {
            moves = xDist > 0 ? 1 : -1;

            //if(xDist > 0)
            //{
            //    moves = 1;
            //}
            //else
            //{
            //    moves = -1;
            //}

            transform.position = new Vector3((transform.position.x + moves), transform.position.y, transform.position.z);
        }
        else
        {
            moves = zDist > 0 ? 1 : -1;
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + moves);
        }
    }

    void Move(Building target)
    {
        int xDist, zDist, moves;

        xDist = (int)(target.transform.position.x - transform.position.x);
        zDist = (int)(target.transform.position.z - transform.position.z);

        if (Mathf.Abs(xDist) >= Mathf.Abs(zDist))
        {
            moves = xDist > 0 ? 1 : -1;
            transform.position = new Vector3(transform.position.x + moves, transform.position.y, transform.position.z);
            Debug.Log("Moving to (" + (transform.position.x + moves) + "," + transform.position.y + "," + transform.position.z + ")");

        }
        else
        {
            moves = zDist > 0 ? 1 : -1;
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + moves);
            Debug.Log("Moving to ("+transform.position.x+","+transform.position.y+","+(transform.position.z+moves)+")");
        }
    }
    void Attack(Unit target)
    {
        target.Health -= atk;

        if(target.Health <= 0)
        {
            target.Die();
        }
    }

    void Attack(Building target)
    {
        target.Health -= atk;

        if (target.Health <= 0)
        {
            target.Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
        alive = false;
    }

    public void SetType(string type)
    {
        if(type=="Knight")
        {
            maxHealth = 20;
            speed = 1;
            atk = 5;
            this.type = type;
            range = 1.9f;
        }
        else if(type=="Archer")
        {
            maxHealth = 15;
            speed = 2;
            atk = 7;
            this.type = type;
            range = 3.5f;
        }
        else
        {
            maxHealth = 25;
            speed = 1;
            atk = 10;
            this.type = type;
            range = 1.9f;
        }
        health = maxHealth;
    }

    void WithinBounds()
    {
        if(transform.position.x < 0)
        {
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
        }

        if (transform.position.z < 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }

        if (transform.position.x > MapSizeManager.mapX - 1)
        {
            transform.position = new Vector3(MapSizeManager.mapX - 1, transform.position.y, transform.position.z);
        }

        if (transform.position.z > MapSizeManager.mapZ - 1)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, MapSizeManager.mapZ - 1);
        }
    }
}
