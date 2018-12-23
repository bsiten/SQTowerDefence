using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : Entity
{
    public float speed = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        // m_velocity = (mousePos - screenPos).normalized * speed;
        m_velocity = -(mousePos - screenPos).normalized * speed;
        m_velocity.z = m_velocity.y;
        m_velocity.y = 0.0f;
        base.Update();
    }
}
