using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSizeManager : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;

    public static int mapX = 20;
    public static int mapZ = 20;

    int camZoom = 20;

    public static bool updateMap;

    // Start is called before the first frame update
    void Start()
    {
        updateMap = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(updateMap)
        {
            Debug.Log("Resizing Map");
            transform.localScale = new Vector3(mapX, 2, mapZ);
            transform.position = new Vector3(mapX / 2, -1, mapZ / 2);

            camZoom = mapX > mapZ ? mapX : mapZ;

            mainCamera.transform.position = new Vector3(mapX / 2, camZoom, mapZ / 2);

            updateMap = false;
        }
    }
}
