using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinionsManager : MonoBehaviour
{
    public int MinionNum = 5;
    public List<Image> MinionImages;
    public List<GameObject> Minions;

    public Image MinionImage;
    public Transform PanelTrans;
    public GameObject PrefabMinion;
    public GameObject PrefabMinion_Longrange;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = -2; i < 3; ++i)
        {
            Vector3 pos = PanelTrans.position;
            pos.y = 800;
            pos.x += i * 150;
            Image Instant = Instantiate(MinionImage, pos, Quaternion.identity);
            Instant.transform.parent = PanelTrans;
            MinionImages.Add(Instant);
        }
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void UpdateMinions()
    {
        for (int i = 0; i < 5; ++i)
        {
            int id = MinionImages[i].GetComponent<DropObject>().MinionId;

            Debug.Log(id);

            if (id == 1)
            {
                Minions.Add(PrefabMinion);
            }
            else if (id == 2)
            {
                Minions.Add(PrefabMinion_Longrange);
            }

        }
    }

    public List<GameObject> GetMinions()
    {
        return Minions;
    }

}
