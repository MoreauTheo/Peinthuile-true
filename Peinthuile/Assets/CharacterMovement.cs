using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public string level;
    public string[] levelSplitted;
    public GridBoard board;
    public Vector2Int pos;
    public Vector2Int DesiredPosition;
    public GameObject[,] grille2;
    void Start()
    {
        pos = new Vector2Int(4, 4);
        DesiredPosition = pos;
        Applymovement();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            DesiredPosition = new Vector2Int(pos.x, pos.y+1);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            DesiredPosition = new Vector2Int(pos.x, pos.y-1);
        }
        if(Input.GetKeyDown(KeyCode.E)) 
        {
            DesiredPosition = new Vector2Int(pos.x + 1, pos.y + (pos.x % 2));
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DesiredPosition = new Vector2Int(pos.x - 1, pos.y + (pos.x % 2));
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            DesiredPosition = new Vector2Int(pos.x - 1, pos.y - 1 + (pos.x % 2));
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            DesiredPosition = new Vector2Int(pos.x + 1, pos.y -1 + (pos.x % 2));
        }
        CheckMovement();
        Applymovement();
    }
    // Update is called once per frame
    public void Applymovement()
    {
        
        transform.position = board.grille[pos.x, pos.y].transform.position ;
    }
    public void CheckMovement()
    {
        if (board.grille[DesiredPosition.x,DesiredPosition.y].tag != "Mur")
            pos = DesiredPosition;
    }
}
