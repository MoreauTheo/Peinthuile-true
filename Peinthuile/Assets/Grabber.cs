using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Grabber : MonoBehaviour
{
    private GridBoard board;
    private GameObject selectedObject;
    public int pposx;
    public int pposy;
    private string level;
    private TuileCodex codex;
    public string newtag;
    private PiocheScript pioche;
    private bool pose;
    // Start is called before the first frame update
    void Start()
    {
        board = GetComponent<GridBoard>();
        codex = GetComponent<TuileCodex>();
        pioche = GetComponent<PiocheScript>();

        

    }
    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit hit = CastRay();
            if(hit.collider) {
                
                selectedObject = hit.collider.gameObject;
                Debug.Log(selectedObject.tag);
                if (selectedObject.tag =="vide")
                {
                    PoseTuile(pioche.current);
                }
                else if(selectedObject.tag.Substring(0, selectedObject.tag.Length - 1) != pioche.current.Substring(0, pioche.current.Length-1) && selectedObject.tag.Length <4)
                {
                    Debug.Log(selectedObject.tag.Substring(0, selectedObject.tag.Length - 1));
                    Debug.Log(pioche.current.Substring(0,pioche.current.Length - 1));
                    FusionneTuile(pioche.current);
                }
                else
                {
                    Debug.Log("ENFIN!!!");
                }
                if(pose)
                pioche.NextTurn();
            }
        }
    }

    private RaycastHit CastRay()//permet de tirer un rayon la ou le joueur clic peut importe la direction de la camera
    {
        Vector3 screenMousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);

        //transformation des position ecran en position monde
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);

        //tir du raycast
        RaycastHit hit;
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);
        return hit;
    }

    public void PoseTuile(string tuileAPoser)
    {
        TuileConfig cibleScript = new TuileConfig();
        cibleScript = selectedObject.GetComponent<TuileConfig>();
        board.grille[cibleScript.posx,cibleScript.posy] = Instantiate(codex.TouteTuiles[tuileAPoser], selectedObject.transform.position, selectedObject.transform.rotation);
        Destroy(selectedObject);
        pose = true;
    }

    public void FusionneTuile(string tuileAPoser)
    {
        level = selectedObject.tag.Substring(codex.TouteTuiles[tuileAPoser].tag.Length - 1, 1 );
        newtag = selectedObject.tag.Substring(0, selectedObject.tag.Length - 1);
        newtag = newtag + codex.TouteTuiles[tuileAPoser].tag.Substring(0, codex.TouteTuiles[tuileAPoser].tag.Length - 1);
        newtag = Alphabetize(newtag);
        newtag += level;
        PoseTuile(newtag);

    }

    public static string Alphabetize(string s)
    {
        // Convert to char array.
        // Convert to char array.
        char[] a = s.ToCharArray();

        // Sort letters.
        Array.Sort(a);

        // Return modified string.
        return new string(a);
    }

}
