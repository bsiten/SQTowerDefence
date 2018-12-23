using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    public int PreparePhaseTime = 30;
    public int BattlePhase = 120;
    float startTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PreparePhase()
    {
         if (startTime == 0.0f)
        {
            startTime = PhotonNetwork.time;
        } else
        {
            if (PhotonNetwork.time - startTime == 30000)
            {
                startTime = 0.0f;
            }
        }
    }

}
