using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
public class AreaOfEffect : MonoBehaviour
{
    [SerializeField] Entity.Buff buff;
    [SerializeField] float buffDuration = 0;

    HashSet<Entity> entity_list = new HashSet<Entity>();
    [SerializeField] float duration = 1;

    public void Start()
    {
        // Debug.Log(buff.name);
    }

    private void Update()
    {
        foreach (var entity in entity_list)
        {
            if (entity == null)
            {
                entity_list.Remove(entity);
            }
            else
            {
                entity.AddBuff(buff, buffDuration);
            }
        }
        if (duration < 0)
        {
            // Destroy(this);
            Destroy(transform.gameObject);
        }
        duration -= Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("AoE onCollisionDebug");
        var entity = other.gameObject.GetComponent<Entity>();
        if (entity != null)
        {
            entity_list.Add(entity);
        }
    }

}