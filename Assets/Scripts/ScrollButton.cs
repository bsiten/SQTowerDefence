using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class ScrollButton : MonoBehaviour
{

    List<MatchInfoSnapshot> m_Matches = null;

    [SerializeField]
    private GameObject btnPref;  //ボタンプレハブ

    void Start() { 

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            NetworkManager.singleton.matchMaker.ListMatches(0, 10, "", true, 0, 0, OnListMatches);
        }
    }

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

            //Content取得(ボタンを並べる場所)
            RectTransform content = GameObject.Find("Canvas/Scroll View/Viewport/Content").GetComponent<RectTransform>();

            //Contentの高さ決定
            //(ボタンの高さ+ボタン同士の間隔)*ボタン数
            float btnSpace = content.GetComponent<VerticalLayoutGroup>().spacing;
            float btnHeight = btnPref.GetComponent<LayoutElement>().preferredHeight;
            content.sizeDelta = new Vector2(0, (btnHeight + btnSpace) * m_Matches.Count);
            for (int i = 0; i < m_Matches.Count; i++)
            {
                int no = i;
                //ボタン生成
                GameObject btn = (GameObject)Instantiate(btnPref);

                //ボタンをContentの子に設定
                btn.transform.SetParent(content, false);

                //ボタンのテキスト変更
                btn.transform.GetComponentInChildren<Text>().text = "room" + no.ToString();

                //ボタンのクリックイベント登録
                btn.transform.GetComponent<Button>().onClick.AddListener(() => OnClick(no));
            }

        }
        else
        {
            print("ListMatches()失敗");
        }
    }

    public void OnClick(int no)
    {
        Debug.Log(no);
        NetworkManager.singleton.matchMaker.JoinMatch(m_Matches[no].networkId, "", "", "", 0, 0, OnJoinMatch);
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