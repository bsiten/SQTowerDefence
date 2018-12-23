using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectRange : MonoBehaviour
{
    public HashSet<GameObject> detectedObjectList = new HashSet<GameObject>();
    // public LayerMask layerMask;
    public List<string> detectTags;
    public Color rangeColor;

    void Update()
    {
        var nextDetectOjbectlist = new HashSet<GameObject>(detectedObjectList);
        foreach (var detectedObject in detectedObjectList)
        {
            if (detectedObject == null)
            {
                // detectedObjectList.Remove(detectedObject);
                nextDetectOjbectlist.Remove(detectedObject);
            }
        }
        detectedObjectList = nextDetectOjbectlist;
    }


    void OnTriggerEnter(Collider other)
    {
        // var layerName = LayerMask.LayerToName(other.gameObject.layer);
        // if (layerMask != null && layerMask == other.gameObject.layer)
        if (detectTags.Contains(other.gameObject.tag))
        {
            // Debug.Log(other.gameObject.name + " Enter " + transform.name);
            detectedObjectList.Add(other.gameObject);
        }
    }
    void OnTriggerExit(Collider other)
    {
        // if (layerMask != null && layerMask == other.gameObject.layer)
        if (detectTags.Contains(other.gameObject.tag))
        {
            // Debug.Log(other.gameObject.name + " Exit " + transform.name);
            detectedObjectList.Remove(other.gameObject);
        }

    }
    void OnDrawGizmos()
    {
        Gizmos.color = rangeColor;
        // var collider = transform.GetComponent<CapsuleCollider>();
        Gizmos.DrawSphere(transform.position, 10);
        // Gizmos.DrawSphere(transform.position, collider.radius);
    }
}