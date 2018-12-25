using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreparePhase : MonoBehaviour
{
    public float LimitTime = 20;
    public bool PrepareNow = false;

    public TimerManager Timer;

    public GameObject panel;
    public MinionsManager MiniMana;
    private MinionsManager TempMinimana;

    public bool EndOfPhase = false;

    public List<GameObject> Minions;

    // Start is called before the first frame update
    void Start()
    {
        TempMinimana = MiniMana;
    }

    public void StartPreparePhase()
    {
        if (PrepareNow)
        {
            return;
        }
        Minions = new List<GameObject>(5);
        MiniMana = TempMinimana;
        PrepareNow = true;
        panel.SetActive(true);
        Timer.SetTimer(LimitTime);
        //Instantiate("DeffencePlayer");
    }

    // Update is called once per frame
    void Update()
    {
        if (Timer.TimerEnd())
        {
            EndPhase();
        }
    }

    public void EndPhase()
    {
        MiniMana.UpdateMinions();
        Minions = MiniMana.GetMinions();

        panel.SetActive(false);

        EndOfPhase = true;
        
    }

    public void ResetStatus()
    {
        MiniMana = null;
        Minions = null;
        PrepareNow = false;
        EndOfPhase = false;
    }

}
