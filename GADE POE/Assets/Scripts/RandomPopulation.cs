using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPopulation : MonoBehaviour
{
    [SerializeField] GameObject factoryRed;
    [SerializeField] GameObject factoryBlue;
    [SerializeField] GameObject resourceRed;
    [SerializeField] GameObject resourceBlue;
    [SerializeField] GameObject wizard;
    [SerializeField] GameObject meleeRed;
    [SerializeField] GameObject meleeBlue;
    [SerializeField] GameObject rangedRed;
    [SerializeField] GameObject rangedBlue;

    public static int numUnits = 12;
    public static int numBuildings = 4;
    public static int mapX = 20;
    public static int mapZ = 20;

    GameObject[] units;
    GameObject[] buildings;
    bool[,] occupied;

    int rndX = 0, rndZ = 0, rndType, rndTeam;
    public static bool updatePop;

    // Start is called before the first frame update
    void Start()
    {
        updatePop = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(updatePop)
        {
            if(units!=null || buildings != null)
            {
                Debug.Log("Clearing Board");
                units = GameObject.FindGameObjectsWithTag("Unit");
                buildings = GameObject.FindGameObjectsWithTag("Building");

                foreach(GameObject unit in units)
                {
                    Destroy(unit);
                }

                foreach(GameObject building in buildings)
                {
                    Destroy(building);
                }
            }

            Debug.Log("Randomly Populating");
            units = new GameObject[numUnits];
            buildings = new GameObject[numBuildings];
            occupied = new bool[mapX, mapZ];

            for (int i = 0; i < units.Length; i++)
            {
                do
                {
                    rndX = Random.Range(0, mapX);
                    rndZ = Random.Range(0, mapZ);
                } while (occupied[rndX, rndZ]);

                rndType = Random.Range(0, 3);
                rndTeam = Random.Range(0, 2);

                switch (rndType)
                {
                    case 0:
                        units[i] = rndTeam == 0 ? rangedBlue : rangedRed;
                        break;
                    case 1:
                        units[i] = rndTeam == 0 ? meleeBlue : meleeRed;
                        break;
                    default:
                        units[i] = wizard;
                        break;
                }

                units[i].transform.position = new Vector3(rndX, 0, rndZ);

                Instantiate(units[i]);
                occupied[rndX, rndZ] = true;
            }

            for (int j = 0; j < buildings.Length; j++)
            {
                do
                {
                    rndX = Random.Range(0, mapX);
                    rndZ = Random.Range(0, mapZ);
                } while (occupied[rndX, rndZ]);

                rndType = Random.Range(0, 2);
                rndTeam = Random.Range(0, 2);

                switch (rndType)
                {
                    case 0:
                        buildings[j] = rndTeam == 0 ? factoryBlue : factoryRed;
                        break;
                    default:
                        buildings[j] = rndTeam == 0 ? resourceBlue : resourceRed;
                        break;
                }

                buildings[j].transform.position = new Vector3(rndX, 0, rndZ);

                Instantiate(buildings[j]);
                occupied[rndX, rndZ] = true;
            }

            updatePop = false;
        }
    }
}
