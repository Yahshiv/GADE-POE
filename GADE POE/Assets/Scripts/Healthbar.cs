using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    [SerializeField] RectTransform healthBar;
    [SerializeField] Unit me;
    [SerializeField] Building meB;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform);
        if (me != null)
        {
            healthBar.sizeDelta = new Vector2(50f * ((float)me.Health / me.MaxHealth), healthBar.sizeDelta.y);
        }
        else if (meB != null)
        {
            healthBar.sizeDelta = new Vector2(50f * ((float)meB.Health / meB.MaxHealth), healthBar.sizeDelta.y);
        }
        else
        {
            Debug.Log("Health Bar Failed to find Attached Unit or Building");
        }
    }
}
