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
    GameObject m_barrel;

    void Start()
    {
        m_barrel = transform.gameObject;
        // m_barrel = transform.Find("Barrel").gameObject;
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
        var aim = new Vector3(target.x, m_barrel.transform.position.y, target.z);
        var angle_dif = Vector3.Angle(m_barrel.transform.forward, aim);
        var angle_move = (angle_dif < maxRotateVelocity * Time.deltaTime) ? angle_dif : maxRotateVelocity * Time.deltaTime;
        var rotate_vector = Vector3.Cross(m_barrel.transform.forward, aim).normalized * angle_move;
        m_barrel.transform.Rotate(rotate_vector);
        // m_barrel.transform.localPosition = Vector3.Dot(rotate_vector, m_barrel.transform.localPosition);
        m_barrel.transform.localPosition = Quaternion.AngleAxis(angle_move, rotate_vector.normalized) * m_barrel.transform.localPosition;

    }

}
