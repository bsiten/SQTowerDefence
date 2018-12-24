using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public int[] NumPlayerWin = new int[2];
    const int ID_PLAYER1 = 0;
    const int ID_PLAYER2 = 1;

    public TimerManager Timer;

    public float BattleTime;

    public PreparePhase PreparePhase;

    private bool EndOfPreparePhase = false;

    public AttackPlayer AttackPlayer;
    public DeffencePlayer DefencePlayer;

    public GameObject Tower;

    public List<int> PlayerOrder = new List<int>{ 1, 0, 0, 1, 1, 0, 0, 1, 1, 0};

    public int Round = 0;

    public Vector3 DeffenceStartPosition;
    public Vector3 AttackStartPosition;
    public Vector3 CrystalPosition;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!CheckGameOver())
        {
            if (DefencePlayer == null)
            {
                Instantiate(Tower, CrystalPosition, Quaternion.identity);
                Instantiate(DefencePlayer, DeffenceStartPosition, Quaternion.identity);
                DefencePlayer.GetComponent<DeffencePlayer>().PlayerID = PlayerOrder[Round * 2];
            }
            PreparePhase.StartPreparePhase();
            if (PreparePhase.EndOfPhase)
            {
                //AttackPlayer 作成
                    if (AttackPlayer == null)
                {
                    Instantiate(AttackPlayer, AttackStartPosition, Quaternion.identity);
                    AttackPlayer.GetComponent<AttackPlayer>().PlayerID = PlayerOrder[Round * 2 + 1];
                }
                //
                EndOfPreparePhase = true;
                PreparePhase.EndOfPhase = false;
            }

            if (EndOfPreparePhase)
            {
                Timer.SetTimer(BattleTime);
                EndOfPreparePhase = false;
            }

            if (CheckVictory())
            {
                AttackPlayer = null;
                DefencePlayer = null;
                Tower = null;
                Timer.TimerEnd();
                ++Round;
            }
        }
        else
        {
            SceneManager.LoadScene("Result");
        }
    }

    public bool CheckVictory()
    {
        if (EndOfPreparePhase && Timer.TimerEnd())
        {
            NumPlayerWin[DefencePlayer.GetComponent<DeffencePlayer>().PlayerID]++;
            return true;
        }

        if (AttackPlayer.GetComponent<Entity>().status.health <= 0)
        {
            NumPlayerWin[AttackPlayer.GetComponent<AttackPlayer>().PlayerID]++;
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
        if (NumPlayerWin[ID_PLAYER1] >= 3)
        {
            Debug.Log("Player1 won!!!!!");
            return true;
        } else if (NumPlayerWin[ID_PLAYER2] >= 3)
        {
            Debug.Log("Player2 won!!!!!");
            return true;
        }
        return false;
    }

}
