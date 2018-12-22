using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBase : MonoBehaviour
{

    [SerializeField] float maxRotateVelocity = 0;
    [SerializeField] float fireRate = 1;
    [SerializeField] float fireQuantity = 1;


    // List<Collider> m_collider_list;
    HashSet<GameObject> m_in_range_object_list = new HashSet<GameObject>();

    void Start()
    {
    }


    void Update()
    {
        Aim();

    }

    void OnTriggerEnter(Collider other)
    {
        m_in_range_object_list.Add(other.gameObject);
    }
    void OnTriggerExit(Collider other)
    {
        m_in_range_object_list.Remove(other.gameObject);

    }

    // 概要:
    //     select target object in list & aim to it
    void Aim()
    {
        if (m_in_range_object_list.Count != 0)
        {
            float min_distance = 0;
            GameObject nearest_object = null;
            foreach (var in_range_object in m_in_range_object_list)
            {
                var distance = (transform.position - in_range_object.transform.position).magnitude;
                if (min_distance == 0 || min_distance > distance)
                {
                    min_distance = distance;
                    nearest_object = in_range_object;
                }
            }
            AimTo(nearest_object.transform.position);
        }
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
        var aim = new Vector3(target.x, transform.position.y, target.z);
        var angle_dif = Vector3.Angle(transform.forward, aim);
        var angle_move = (angle_dif < maxRotateVelocity) ? angle_dif : maxRotateVelocity;
        var rotate_dir = Vector3.Cross(transform.forward, aim).normalized * angle_move;
        transform.Rotate(rotate_dir);

    }

}
