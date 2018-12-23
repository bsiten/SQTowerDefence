using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBase : Entity
{

    [SerializeField] float maxRotateVelocity = 0;
    [SerializeField] float fireRate = 1;
    [SerializeField] float fireQuantity = 1;


    // List<Collider> m_collider_list;
    // HashSet<GameObject> m_in_range_object_list = new HashSet<GameObject>();
    // HashSet<GameObject> m_in_range_object_list;
    DetectRange targetRange;
    [SerializeField] GameObject m_barrel;
    [SerializeField] GameObject bullet;

    float m_fireInterval = 1;

    public new void Start()
    {
        base.Start();
        // targetRange = transform.gameObject.GetComponentInChildren<DetectRange>();
        foreach (Transform child in transform)
        {
            if (child.name == "TargetRange")
            {
                targetRange = child.GetComponent<DetectRange>();
            }
        }
        // m_barrel = transform.gameObject;
        // m_barrel = transform.Find("Barrel").gameObject;
    }


    public new void Update()
    {
        base.Update();
        Aim();
    }

    // void OnTriggerEnter(Collider other)
    // {
    //     var layerName = LayerMask.LayerToName(other.gameObject.layer);
    //     if (layerName == "Entity")
    //     {
    //         m_in_range_object_list.Add(other.gameObject);
    //     }
    // }
    // void OnTriggerExit(Collider other)
    // {
    //     m_in_range_object_list.Remove(other.gameObject);

    // }

    // 概要:
    //     select target object in list & aim to it
    void Aim()
    {
        // var m_in_range_object_list = targetRange.detectedObjectList;
        // if (m_in_range_object_list.Count != 0)
        // {
        //     float min_distance = 0;
        //     GameObject nearest_object = null;
        //     foreach (var in_range_object in m_in_range_object_list)
        //     {
        //         if (in_range_object == null)
        //         {
        //             m_in_range_object_list.Remove(in_range_object);
        //             continue;
        //         }
        //         var distance = (transform.position - in_range_object.transform.position).magnitude;
        //         if (min_distance == 0 || min_distance > distance)
        //         {
        //             min_distance = distance;
        //             nearest_object = in_range_object;
        //         }
        //     }
        //     AimTo(nearest_object.transform.position);
        // }
        var targetObject = NearestObject(targetRange);
        if (targetObject != null)
        {
            AimTo(targetObject.transform.position);
        }
        m_fireInterval -= Time.deltaTime;
    }


    // 概要:
    //     aim to target point
    //
    // パラメーター:
    //   target:
    //      target position in world system
    //
    // 戻り値:
    //      void
    void AimTo(Vector3 target)
    {
        var pos = transform.position;
        var aim = new Vector3(target.x - pos.x, 0, target.z - pos.z);
        var angle_dif = Vector3.Angle(m_barrel.transform.forward, aim);
        var angle_move = (angle_dif < maxRotateVelocity * Time.deltaTime) ? angle_dif : maxRotateVelocity * Time.deltaTime;
        var rotate_vector = Vector3.Cross(m_barrel.transform.forward, aim).normalized * angle_move;
        m_barrel.transform.Rotate(rotate_vector);
        if (m_fireInterval < 0)
        {
            Fire(target);
            m_fireInterval = fireRate;
        }
    }

    //概要
    void Fire(Vector3 target)
    {
        var fire_bullet = GameObject.Instantiate(bullet, m_barrel.transform.position, m_barrel.transform.rotation);
        var comp_bullet = fire_bullet.GetComponent<BulletBase>();
        comp_bullet.SetInpactPoint(target);
    }

}
