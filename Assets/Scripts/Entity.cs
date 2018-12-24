using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    [SerializeField] protected float speed;
    [SerializeField] protected float maxMovementSpeed;
    [SerializeField] protected Status status;

    [SerializeField] protected List<GameObject> destroyObjectList;
    [SerializeField] public bool nonRegenelation = false;
    protected Vector3 m_velocity;
    protected float speedCoefficion = 1;

    public void Start()
    {
        status.Reset();
        // foreach (var destroyObject in destroyObjectList)
        // {
        //     Instantiate(destroyObject, transform.position + Vector3.up, transform.rotation);
        // }
    }

    public void Update()
    {
        BuffProcess();
        StatusCheck();
        Move();
    }

    //概要：
    // Entityのステータス管理．
    [System.Serializable]
    public struct Status
    {
        public float health;
        public float maxHealth;
        public HashSet<Buff> buffList;
        public Dictionary<Buff, float> buff_duration_list;

        public void Reset()
        {
            health = maxHealth;
            buffList = new HashSet<Buff>();
            buff_duration_list = new Dictionary<Buff, float>();
        }

        public void ClearBuff()
        {
            buffList.Clear();
            buff_duration_list.Clear();
        }
        public void AddBuff(Buff buff, float duration)
        {
            buffList.Add(buff);
            if (!buff_duration_list.ContainsKey(buff))
            {
                buff_duration_list.Add(buff, duration);
            }
        }

    }

    //概要:
    // Entityに与える効果の汎用クラス
    // buffの種類ごとの処理はEntity側で実装する．
    [System.Serializable]
    public struct Buff
    {
        public string name;    //id
        public float intensity;     //intensity
        // public float duration; //duration time
        // public void ReduceDuration(float time)
        // {
        //     duration -= time;
        // }
    }

    //////////////////////////////////////////////////////////////////////////////
    //public method
    public void AddBuff(Buff buff, float duration)
    {
        status.AddBuff(buff, duration);
    }

    //////////////////////////////////////////////////////////////////////////////
    //protected virtual member function

    //概要:
    // Entityをvelocity方向に移動．maxMovementSpeedで制限する．
    protected void Move()
    {
        if (m_velocity.magnitude > maxMovementSpeed)
        {
            m_velocity = m_velocity.normalized * maxMovementSpeed;
        }
        // transform.position += m_velocity * Time.deltaTime;
        transform.position += m_velocity * speedCoefficion * Time.deltaTime;
        transform.LookAt(transform.position + m_velocity * Time.deltaTime);
    }
    //概要
    // 各buffごとの処理
    protected void BuffProcess()
    {
        //init buff properties
        speedCoefficion = 1;
        //copy list
        var nextBuffList = new HashSet<Buff>(status.buffList);
        var nextBuffDuarationList = new Dictionary<Buff, float>(status.buff_duration_list);
        //process buffs
        foreach (var buff in status.buffList)
        {
            if (buff.name == "Damage")//instant damage
            {
                // Debug.Log(transform.gameObject.name + " Damage loaded");
                status.health -= buff.intensity;
            }
            if (buff.name == "Injured")//split damage
            {
                // Debug.Log("Injured buff loaded");
                status.health -= buff.intensity * Time.deltaTime;
            }
            if (buff.name == "Heal")//instant heal
            {
                // Debug.Log("Heal loaded");
                status.health += buff.intensity;
            }
            if (buff.name == "Regeneration" && !nonRegenelation)//split heal
            {
                // Debug.Log("Regeneration buff loaded");
                status.health += buff.intensity * Time.deltaTime;
            }
            if (buff.name == "Slow")//speed down
            {
                if (buff.intensity > 0)
                {
                    // Debug.Log("Slow buff loaded");
                    speedCoefficion /= buff.intensity;
                }
            }
            if (buff.name == "Fast")//speed up
            {
                if (buff.intensity > 0)
                {
                    // Debug.Log("Fast buff loaded");
                    speedCoefficion *= buff.intensity;
                }
            }
            //reduce duaration
            // status.buff_duration_list[buff] -= Time.deltaTime;
            nextBuffDuarationList[buff] -= Time.deltaTime;
            if (nextBuffDuarationList[buff] < 0)
            {
                nextBuffList.Remove(buff);
                nextBuffDuarationList.Remove(buff);
            }
        }
        status.buffList = nextBuffList;
        status.buff_duration_list = nextBuffDuarationList;
    }

    //概要
    // Status値ごとの処理
    //死亡などを実行する．
    protected void StatusCheck()
    {
        if (status.health > status.maxHealth)
        {
            status.health = status.maxHealth;
        }
        //死亡処理
        if (status.health <= 0)
        {
            Dead();
        }
    }
    //概要
    //  所望の軌道になるように速度を計算
    protected void CalculateVelocity() { m_velocity = Vector3.zero; }
    //死亡処理
    protected void Dead()
    {
        if (destroyObjectList.Count != 0)
        {
            foreach (var destroyObject in destroyObjectList)
            {
                Instantiate(destroyObject, transform.position, transform.rotation);
            }
        }
        // Destroy(transform.gameObject);
        transform.gameObject.SetActive(false);
    }

    //DetectRange内で最も近いオブジェクトを取得
    protected GameObject NearestObject(DetectRange range)
    {
        float min_distance = 0;
        GameObject nearestObject = null;
        if (range.detectedObjectList.Count != 0)
        {
            foreach (var detectedObject in range.detectedObjectList)
            {
                var distance = (transform.position - detectedObject.transform.position).magnitude;
                if (min_distance == 0 || min_distance > distance)
                {
                    min_distance = distance;
                    nearestObject = detectedObject;
                }
            }
        }
        return nearestObject;
    }
    //////////////////////////////////////////////////////////////////////////////
    //private member function

    //debug用
    // private void CalcVelocity()
    // {
    //     m_velocity = movementSpeed * Vector3.right;
    // }

}
