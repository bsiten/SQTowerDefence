using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Networking_02 : MonoBehaviour
{
    PhotonPlayer[] player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player = PhotonNetwork.playerList;
        if (player.Length == 2)
        {
            SceneManager.LoadScene("Battle");
        }
    }
}
