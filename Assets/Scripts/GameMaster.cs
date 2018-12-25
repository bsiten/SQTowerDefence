using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    public bool PrepareFlag = false;
    public bool BattleFlag = false;
    public bool RoundChangeInterval = false;
   
    public float RoundChangeIntervalTime = 10.0f;

    public Text Player1Text;
    public Text Player2Text;

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
        AttackPlayer.GetComponent<Entity>().Stop();

        Round = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!CheckGameOver())
        {
            MainGameLoop();
        } else
        {
            SceneManager.LoadScene("Result");
        }
    }

    void MainGameLoop()
    {
        
        if (AttackPlayer.GetComponent<AttackPlayer>().PlayerID == ID_PLAYER1)
        {
            Player1Text.GetComponent<RectTransform>().anchoredPosition = new Vector2(880, 290);
        } else
        {
            Player2Text.GetComponent<RectTransform>().anchoredPosition = new Vector2(880, 290);
        }

        if (DefencePlayer.GetComponent<DeffencePlayer>().PlayerID == ID_PLAYER1)
        {
            Player1Text.GetComponent<RectTransform>().anchoredPosition = new Vector2(-650, 290);
        }
        else
        {
            Player2Text.GetComponent<RectTransform>().anchoredPosition = new Vector2(-650, 290);
        }

        if (!PrepareFlag && !BattleFlag && !RoundChangeInterval)
        {
            PreparePhase.GetComponent<PreparePhase>().StartPreparePhase();
            PrepareFlag = true;
        }

        if (PrepareFlag && PreparePhase.GetComponent<PreparePhase>().EndOfPhase)
        {
            PrepareFlag = false;
            
            for (int i = 0; i < PreparePhase.GetComponent<PreparePhase>().Minions.Count; ++i)
            {
                Instantiate(PreparePhase.GetComponent<PreparePhase>().Minions[i], AttackStartPosition, Quaternion.identity);
            }
            AttackPlayer.GetComponent<Entity>().UnStop();
            AttackPlayer.GetComponent<AttackPlayer>().initMinionList();
            Timer.SetTimer(BattleTime);
            BattleFlag = true;
        }

        if (BattleFlag && CheckVictory())
        {
            GameObject[] minions = GameObject.FindGameObjectsWithTag("Minion");

            for (int i = 0; i < minions.Length; ++i)
            {
                Destroy(minions[i].GetComponent<Entity>().transform.gameObject);
            }

            GameObject[] cannons = GameObject.FindGameObjectsWithTag("Cannon");
            for (int i = 0; i < cannons.Length; ++i)
            {
                Destroy(cannons[i].GetComponent<Entity>().transform.gameObject);
            }

            Round++;
            AttackPlayer.transform.position = AttackStartPosition;
            DefencePlayer.transform.position = DeffenceStartPosition;
            DefencePlayer.GetComponent<DeffencePlayer>().NowCannonNum = 0;
            AttackPlayer.GetComponent<AttackPlayer>().PlayerID = 1 - AttackPlayer.GetComponent<AttackPlayer>().PlayerID;
            DefencePlayer.GetComponent<DeffencePlayer>().PlayerID = 1 - DefencePlayer.GetComponent<DeffencePlayer>().PlayerID;
            PreparePhase.GetComponent<PreparePhase>().ResetStatus();
            RoundChangeInterval = true;
            BattleFlag = false;
            Timer.SetTimer(RoundChangeIntervalTime);
            AttackPlayer.GetComponent<Entity>().Stop();
            DefencePlayer.GetComponent<Entity>().Stop();
        }

        if (RoundChangeInterval && Timer.TimerEnd()) 
        {
            GameObject[] minions = GameObject.FindGameObjectsWithTag("Minion");
            for (int i = 0; i < minions.Length; ++i)
            {
                Destroy(minions[i].GetComponent<Entity>().transform.gameObject);
            }

            GameObject[] cannons = GameObject.FindGameObjectsWithTag("Cannon");
            for (int i = 0; i < cannons.Length; ++i)
            {
                Destroy(cannons[i].GetComponent<Entity>().transform.gameObject);
            }
            DefencePlayer.GetComponent<Entity>().UnStop();
            RoundChangeInterval = false;
        }

    }

    public bool CheckVictory()
    {
        if (BattleFlag && Timer.TimerEnd())
        {
            NumPlayerWin[DefencePlayer.GetComponent<DeffencePlayer>().PlayerID]++;
            return true;
        }

        if (DefencePlayer.GetComponent<Entity>().status.health <= 0)
        {
            NumPlayerWin[AttackPlayer.GetComponent<AttackPlayer>().PlayerID]++;
            DefencePlayer.GetComponent<Entity>().status.Reset();
            return true;
        }

        if (AttackPlayer.GetComponent<Entity>().status.health <= 0)
        {
            NumPlayerWin[DefencePlayer.GetComponent<DeffencePlayer>().PlayerID]++;
            AttackPlayer.GetComponent<Entity>().status.Reset();
            return true;
        }

        if (Tower.GetComponent<Entity>().status.health <= 0)
        {
            NumPlayerWin[AttackPlayer.GetComponent<AttackPlayer>().PlayerID]++;
            Tower.GetComponent<Entity>().status.Reset();
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
