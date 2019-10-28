using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundCounter : MonoBehaviour
{
    [SerializeField] Text roundText;
    [SerializeField] Text winner;
    [SerializeField] Text redRes;
    [SerializeField] Text blueRes;

    public static int round = 0;
    public static bool running = true;
    public static string winTeam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(running)
        {
            round++;
            roundText.text = "Round: " + round;
            winTeam = "";

            System.Threading.Thread.Sleep(100);
        }

        winner.text = "Victor: " + winTeam;

        redRes.text = "Red $"+ Building.redResources;
        blueRes.text = "Blue $" + Building.blueResources;
    }
}
