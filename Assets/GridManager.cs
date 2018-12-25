using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    public GameObject Tile;
    public GameObject Stage;
    public GameObject Grids;
    public List<GameObject> Tiles;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; ++i)
        {
            /*int margin = i + 1;
            if (margin == 4 || margin = 3)
            {
                margin--; 
            }*/
            int margin = i;
            
            for (int j = 0 + margin; j < 10 - margin; ++j)
            {
                Vector3 tilePos = new Vector3(-(Tile.transform.localScale.x * 5 + Tile.transform.localScale.x * 10 * i), 1.26f, Tile.transform.localScale.z * 10 * j - 11f);

                GameObject instantObject = (GameObject)GameObject.Instantiate(Tile, tilePos, Quaternion.identity);

                Tiles.Add(instantObject);

                instantObject.transform.parent = Grids.transform;

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
