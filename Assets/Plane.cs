using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    public bool IsColorChange = false;
    public Color defColor;
    public bool IsLocated = false;
    public bool NowLocate = false;
    public float Delay = 2.0f;

    public GameObject LocateObject;

    // Start is called before the first frame update
    void Start()
    {
        defColor = GetComponent<MeshRenderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<MeshRenderer>().material.color = defColor;
        if (IsColorChange)
        {
            IsColorChange = false;
            GetComponent<MeshRenderer>().material.color = Color.red; 
        }

        if (NowLocate)
        {
            Delay -= Time.deltaTime;
            GetComponent<MeshRenderer>().material.color = Color.green;
            if (Delay <= 0)
            {
                Instantiate(LocateObject, transform.position, Quaternion.identity);
                GetComponent<MeshRenderer>().material.color = defColor;
                NowLocate = false;
            }
        }
    }

    public bool LocateCannon()
    {
        if (IsLocated || NowLocate)
        {
            return false;
        }
        IsLocated = true;
        NowLocate = true;
        return true;
    }

}
