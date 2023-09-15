using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUi : MonoBehaviour
{
    public Image[] lifebars;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HealthRemaining(int healthbars) {
        for (int i = 0; i <= healthbars; i++) {
            if (i == healthbars) {
                lifebars[i].enabled = false;
            }
        }
    }
}
