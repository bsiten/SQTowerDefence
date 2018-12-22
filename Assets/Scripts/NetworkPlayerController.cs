using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkPlayerController : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer == true)
        {
            Move();
        }  
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += transform.forward * 0.2f;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += transform.forward * -0.2f;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += transform.right * -0.2f;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += transform.right * 0.2f;
        }
    }
}
