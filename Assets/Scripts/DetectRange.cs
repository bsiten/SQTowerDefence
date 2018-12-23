using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectRange : MonoBehaviour
{
    public HashSet<GameObject> detectedObjectList = new HashSet<GameObject>();


    void Update()
    {
        foreach (var detectedObject in detectedObjectList)
        {
            if (detectedObject == null)
            {
                detectedObjectList.Remove(detectedObject);
            }
        }
    }


    void OnTriggerEnter(Collider other)
    {
        var layerName = LayerMask.LayerToName(other.gameObject.layer);
        if (layerName == "Entity")
        {
            detectedObjectList.Add(other.gameObject);
        }
    }
    void OnTriggerExit(Collider other)
    {
        detectedObjectList.Remove(other.gameObject);

    }
}