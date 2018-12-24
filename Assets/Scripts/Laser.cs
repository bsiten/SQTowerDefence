using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : BulletBase
{
    [SerializeField] List<string> targetTags;
    void OnTriggerEnter(Collider other)
    {
        foreach (var tag in targetTags)
        {
            if (tag == other.gameObject.tag)
            {
                Explode();
            }
        }
    }
}