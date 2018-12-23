using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AreaOfEffect : MonoBehaviour
{
    [SerializeField] Entity.Buff buff;
    [SerializeField] float buffDuration = 0;

    [SerializeField] float duration = 1;

    // [SerializeField] LayerMask layerMask;
    DetectRange effectRange;

    public void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.name == "EffectRange")
            {
                effectRange = child.GetComponent<DetectRange>();
            }
        }
        // Debug.Log(buff.name);
    }

    private void Update()
    {
        foreach (var entityObject in effectRange.detectedObjectList)
        {
            if (entityObject != null)
            {
                entityObject.GetComponent<Entity>().AddBuff(buff, buffDuration);
            }
        }
        if (duration < 0)
        {
            // Destroy(this);
            Destroy(transform.gameObject);
        }
        duration -= Time.deltaTime;
    }

    // private void OnTriggerStay(Collider other)
    // {
    //     var entity = other.gameObject.GetComponent<Entity>();
    //     if (entity != null)
    //     {
    //         entity_list.Add(entity);
    //     }
    // }

}