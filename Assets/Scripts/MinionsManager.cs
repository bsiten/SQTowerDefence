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

    public int Margin = 120;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = -1; i < 4; ++i)
        {
            Vector3 pos = PanelTrans.position;
            pos.y = 800;
            pos.x += i * Margin;
            Image Instant = Instantiate(MinionImage, pos, Quaternion.identity);
            Instant.transform.parent = PanelTrans;
            MinionImages.Add(Instant);
        }
        
        for (int i = 0; i < 5; ++i)
        {
            Minions.Add(null);
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


            if (id == 1)
            {
                Minions[i] = PrefabMinion;
            }
            else if (id == 2)
            {
                Minions[i] = PrefabMinion_Longrange;
            }
            else if (id == 3)
            {

            }

        }
    }

    public List<GameObject> GetMinions()
    {
        return Minions;
    }

}
