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
    bool wizWin;

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
        if(RoundCounter.round%speed==0 && alive)
        {
            GetTarget();

            if (targetedB != null && team != "Wizard")
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
            else if (targeted != null)
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
            else if (team != "Wizard")
            {
                RoundCounter.running = false;
                RoundCounter.winTeam = team;
            }
            else if(wizWin)
            {
                RoundCounter.running = false;
                RoundCounter.winTeam = "Wizard";
            }

            WithinBounds();
        }
    }

    void GetTarget()
    {
        GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
        GameObject[] buildings = GameObject.FindGameObjectsWithTag("Building");

        distance = float.MaxValue;
        targeted = null;
        targetedB = null;
        wizWin = true;

        if(buildings!=null)
            foreach (GameObject building in buildings)
            {
                targetB = building.GetComponent<Building>();
                Debug.Log("my team " + team + " target team " + targetB.Team);

                if (targetB == null || targetB.Team == Team)
                {
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

                if(target.Team != "Wizard")
                {
                    wizWin = false;
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

        xDist = (int)(target.transform.position.x - transform.position.x);
        zDist = (int)(target.transform.position.z - transform.position.z);

        if(Mathf.Abs(xDist) >= Mathf.Abs(zDist))
        {
            moves = xDist > 0 ? 1 : -1;

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
            //Debug.Log("Moving to (" + (transform.position.x + moves) + "," + transform.position.y + "," + transform.position.z + ")");

        }
        else
        {
            moves = zDist > 0 ? 1 : -1;
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + moves);
            //Debug.Log("Moving to ("+transform.position.x+","+transform.position.y+","+(transform.position.z+moves)+")");
        }
    }
    void Attack(Unit target)
    {
        if(team!="Wizard")
        {
            target.Health -= atk;
        }
        else
        {
            GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");

            foreach (GameObject unit in units)
            {
                target = unit.GetComponent<Unit>();

                if (target == null || target.Team == Team)
                {
                    continue;
                }

                tempDistance = (unit.transform.position - transform.position).magnitude;

                if (tempDistance <= range)
                {
                    targeted = target;
                    targeted.Health -= atk;

                    if (targeted.Health <= 0)
                    {
                        targeted.Die();
                    }
                }
            }
        }

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
            maxHealth = 10;
            speed = 3;
            atk = 15;
            this.type = type;
            range = 1.5f;
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
