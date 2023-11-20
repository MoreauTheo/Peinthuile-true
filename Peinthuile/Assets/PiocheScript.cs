using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiocheScript : MonoBehaviour
{
    public List<string> poolPiochable;
    public string current;
    public int nbPioche;
    public int score;
    void Start()
    {
        NextTurn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NextTurn()
    {
        current = poolPiochable[Random.Range(0,poolPiochable.Count)];
        nbPioche--;
    }

}
