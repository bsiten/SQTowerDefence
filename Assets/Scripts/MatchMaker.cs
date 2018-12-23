using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class MatchMaker : NetworkBehaviour
{
    // 取得したMatch情報を格納するためのList
    public List<MatchInfoSnapshot> m_Matches = null;

    void Start()
    {
        // MatchMakerを使うための準備
        NetworkManager.singleton.StartMatchMaker();
    }

    void Update()
    {
        // CキーでMatch（ルーム）を新規作成する
        if (Input.GetKeyDown(KeyCode.C))
        {
            NetworkManager.singleton.matchMaker.CreateMatch("Room Name", 8, true, "", "", "", 0, 0, OnCreateMatch);
        }

        // Lキーで存在するMatchの一覧を取得する
        if (Input.GetKeyDown(KeyCode.L))
        {
            NetworkManager.singleton.matchMaker.ListMatches(0, 10, "", true, 0, 0, OnListMatches);
        }

        // Jキーで存在するMatchに接続する
        if (Input.GetKeyDown(KeyCode.J))
        {
            // とりあえず見つかった一番上のMatchに参加する
            NetworkManager.singleton.matchMaker.JoinMatch(m_Matches[0].networkId, "", "", "", 0, 0, OnJoinMatch);
        }
    }

    // CreateMatch()が終わった時に呼ばれる関数
    void OnCreateMatch(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        if (success)
        {
            print("CreateMatch()成功");

            NetworkManager.singleton.StartHost(matchInfo);
        }
        else
        {
            print("CreateMatch()失敗");
        }
    }

    // ListMatches()が終わったときに呼ばれる関数
    void OnListMatches(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
    {
        if (success)
        {
            print("ListMatches()成功。");
            print("見つかったMatchの数は：" + matches.Count);

            m_Matches = matches;

            // とりあえず取得したMatchの名前を列挙する
            foreach (var match in m_Matches)
            {
                print(match.name);
            }
        }
        else
        {
            print("ListMatches()失敗");
        }
    }

    // JoinMatch()が終わった時に呼ばれる関数
    void OnJoinMatch(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        if (success)
        {
            print("JoinMatch()成功。");

            NetworkManager.singleton.StartClient(matchInfo);
        }
        else
        {
            print("JoinMatch()失敗");
        }
    }
}