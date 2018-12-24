using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public int Round;
    public TimerManager Timer;

    public float BattleTime;

    public PreparePhase PreparePhase;

    private bool EndOfPreparePhase = false;

    public AttackPlayer AttackPlayer;
    private AttackPlayer TempAttackPlayer;
    public DeffencePlayer DefencePlayer;
    private DeffencePlayer TempDefencePlayer;

    public GameObject Tower;
    private GameObject TempTower;

    public int[] NumPlayerWin = new int[2];
    const int ID_PLAYER1 = 0;
    const int ID_PLAYER2 = 1;

    public Vector3 DeffenceStartPosition;
    public Vector3 AttackStartPosition;
    public Vector3 CrystalPosition;

    // Start is called before the first frame update
    void Start()
    {
        //AttackPlayer = Instantiate(AttackPlayer, AttackStartPosition, Quaternion.identity);
        //DefencePlayer = Instantiate(DefencePlayer, DeffenceStartPosition, Quaternion.identity);
        AttackPlayer.GetComponent<AttackPlayer>().PlayerID = ID_PLAYER1;
        DefencePlayer.GetComponent<DeffencePlayer>().PlayerID = ID_PLAYER2;

        Tower = Instantiate(Tower, DeffenceStartPosition, Quaternion.identity);
        TempTower = Tower;

        TempAttackPlayer = AttackPlayer;
        TempDefencePlayer = DefencePlayer;

        Round = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!CheckGameOver())
        {
            MainGameLoop();
        }
    }

    void MainGameLoop()
    {
        if (!EndOfPreparePhase)
        {
            PreparePhase.GetComponent<PreparePhase>().StartPreparePhase();
        }

        if (!EndOfPreparePhase && PreparePhase.GetComponent<PreparePhase>().EndOfPhase)
        {
            EndOfPreparePhase = PreparePhase.GetComponent<PreparePhase>().EndOfPhase;
            PreparePhase.GetComponent<PreparePhase>().EndOfPhase = false;
            for (int i = 0; i < PreparePhase.GetComponent<PreparePhase>().Minions.Count; ++i)
            {
                Debug.Log("111");
                Instantiate(PreparePhase.GetComponent<PreparePhase>().Minions[i], AttackStartPosition, Quaternion.identity);
            }
            Timer.SetTimer(BattleTime);
        }

        if (CheckVictory() && EndOfPreparePhase)
        {
            Round++;
            AttackPlayer = TempAttackPlayer;
            AttackPlayer.Start();
            DefencePlayer = TempDefencePlayer;
            DefencePlayer.Start();
            AttackPlayer.GetComponent<AttackPlayer>().PlayerID = 1 - AttackPlayer.GetComponent<AttackPlayer>().PlayerID;
            DefencePlayer.GetComponent<DeffencePlayer>().PlayerID = 1 - DefencePlayer.GetComponent<DeffencePlayer>().PlayerID;
            PreparePhase.GetComponent<PreparePhase>().ResetStatus();
            Tower = TempTower;
        }
    }

    public bool CheckVictory()
    {
        if (Timer.TimerEnd())
        {
            NumPlayerWin[DefencePlayer.GetComponent<DeffencePlayer>().PlayerID]++;
            return true;
        }

        if (DefencePlayer.GetComponent<Entity>().status.health <= 0)
        {
            NumPlayerWin[AttackPlayer.GetComponent<AttackPlayer>().PlayerID]++;
            return true;
        }

        if (AttackPlayer.GetComponent<Entity>().status.health <= 0)
        {
            NumPlayerWin[DefencePlayer.GetComponent<DeffencePlayer>().PlayerID]++;
            return true;
        }

        if (Tower.GetComponent<Entity>().status.health <= 0)
        {
            NumPlayerWin[AttackPlayer.GetComponent<AttackPlayer>().PlayerID]++;
            return true;
        }
        return false;
    }

    public bool CheckGameOver()
    {
        if (NumPlayerWin[0] >= 3)
        {
            Debug.Log("Player1 won!!!!!");
            return true;
        } else if (NumPlayerWin[1] >= 3)
        {
            Debug.Log("Player2 won!!!!!");
            return true;
        }
        return false;
    }

}
