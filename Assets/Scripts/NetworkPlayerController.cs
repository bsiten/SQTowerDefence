using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkPlayerController : MonoBehaviour
{
    PhotonView photonView;
    Vector3 speed = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        this.photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.photonView.isMine)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                speed = transform.forward * 0.2f;
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                speed = transform.forward * -0.2f;
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                speed = transform.right * -0.2f;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                speed = transform.right * 0.2f;
            }
        }
    }

    void FixedUpdate()
    {
        transform.position += speed;
        speed = Vector3.zero;
    }
}
