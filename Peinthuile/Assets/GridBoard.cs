using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBoard : MonoBehaviour
{
    // Start is called before the first frame update
    private float posx;
    private float posy;
    public GameObject tuile;
    public GameObject[,] grille;
    void Start()
    {
        grille = new GameObject[12, 12];

        for(int x= 0; x < 12;x++)
        {
            for(int y = 0; y < 12;y++)
            {
                posx = x;
                posy = y;
                if(x%2 == 1)
                {
                    posy += 0.45f;
                   
                }
                grille[x,y] = Instantiate(tuile,new Vector3(posx,0,posy + 0.05f),Quaternion.identity);
                grille[x, y].transform.Rotate(new Vector3(90, 0, 30));
                grille[x, y].GetComponent<TuileConfig>().posx = x;
                grille[x, y].GetComponent<TuileConfig>().posy = y;
            }
        }
    }
   

}
