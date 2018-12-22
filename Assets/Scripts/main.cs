using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class main : NetworkBehaviour
{

    public GameObject Cube;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % 300 == 0)
        {
            CmdCreateCube();
        }
    }

    void CmdCreateCube()
    {
        GameObject obj = (GameObject)Instantiate(Cube, transform.position, transform.rotation);
        NetworkServer.Spawn(obj);
    }
}
