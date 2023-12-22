using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Mathematics;
using System.Threading;
using UnityEditor.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class Grabber : MonoBehaviour
{
    private GameObject PreviewTile;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI piocheText;
    private GridBoard board;
    private GameObject selectedObject;
    public int pposx;
    public int pposy;
    private int level;
    private TuileCodex codex;
    public string newtag;
    private PiocheScript pioche;
    private bool pose;
    public float score;
    public float scorePreview;
    public GameObject PreviewTuilePlateau;
    public GameObject stock;
    

    // Start is called before the first frame update
    void Start()
    {
        PreviewTile = GameObject.Find("PreviewTile");
        score = 0;
        board = GetComponent<GridBoard>();
        codex = GetComponent<TuileCodex>();
        pioche = GetComponent<PiocheScript>();
        checkBoardLvlUp();
        ApplyScoring();
        pioche.Draw();
        pioche.nbPioche++;
        ChangePreview();

        

    }
    // Update is called once per frame
    void Update()
    {
        if (pioche.nbPioche > 0)
        {


            RaycastHit hit = CastRay();
            if (hit.collider)
            {
                if (Input.GetMouseButtonDown(0))
                {
                
                    selectedObject = hit.collider.gameObject;
                    if (selectedObject.tag == "vide")
                    {
                        PoseTuile(pioche.current,selectedObject);
                        score += Scoring(pioche.current, selectedObject.GetComponent<TuileConfig>().posx, selectedObject.GetComponent<TuileConfig>().posy);
                    }
                    else if (selectedObject.tag.Substring(0, selectedObject.tag.Length - 1) != pioche.current.Substring(0, pioche.current.Length - 1) && selectedObject.tag.Length < 3)
                    {
                        PoseTuile(FusionneTuile(pioche.current), selectedObject);
                    }
                    if (pose)
                        NextTurn();
                }
                else
                {
                    selectedObject = hit.collider.gameObject;
                    if(PreviewTuilePlateau == null)
                    {
                        if (selectedObject.tag == "vide")
                        {
                            ChangeRender(false, selectedObject);
                            stock = selectedObject;
                            PreviewTuilePlateau = Instantiate(codex.TouteTuiles[pioche.current], selectedObject.transform.position, selectedObject.transform.rotation);
                            PreviewTuilePlateau.GetComponent<Collider>().enabled = false;
                        }
                        else if (selectedObject.tag != pioche.current &&  selectedObject.tag.Length != 3)
                        {
                            ChangeRender(false, selectedObject);
                            stock = selectedObject;
                            PreviewTuilePlateau = Instantiate(codex.TouteTuiles[FusionneTuile(pioche.current)], selectedObject.transform.position, selectedObject.transform.rotation);
                            PreviewTuilePlateau.GetComponent<Collider>().enabled = false;
                        }
                    }
                    else  
                    {
                        if (selectedObject.tag == "vide")
                        {
                            ChangeRender(true, stock);
                            ChangeRender(false, selectedObject);
                            if (PreviewTuilePlateau.tag.Length > 2)
                            {
                                Destroy(PreviewTuilePlateau);
                                PreviewTuilePlateau = Instantiate(codex.TouteTuiles[pioche.current], selectedObject.transform.position, selectedObject.transform.rotation);
                                PreviewTuilePlateau.GetComponent<Collider>().enabled = false;

                            }
                            else
                            {
                                PreviewTuilePlateau.transform.position = selectedObject.transform.position;
                                stock = selectedObject;
                            }
                        }
                        else if (selectedObject.tag.Length == 3)
                        {
                            if (stock)
                                ChangeRender(true, stock);
                            stock = null;
                            Destroy(PreviewTuilePlateau);
                            PreviewTuilePlateau = null;
                            scorePreview = 0;
                        }
                        else if (selectedObject.tag.Length == 2 && selectedObject.tag != pioche.current)
                        {
                            if (PreviewTuilePlateau.tag == FusionneTuile(pioche.current))
                            {
                                PreviewTuilePlateau.transform.position = selectedObject.transform.position;
                                stock = selectedObject;
                            }
                            else
                            {
                                ChangeRender(true, stock);
                                Destroy(PreviewTuilePlateau);
                                ChangeRender(false, selectedObject);
                                stock = selectedObject;
                                PreviewTuilePlateau = Instantiate(codex.TouteTuiles[FusionneTuile(pioche.current)], selectedObject.transform.position, selectedObject.transform.rotation);
                                PreviewTuilePlateau.GetComponent<Collider>().enabled = false;
                            }
                        }
                        else 
                        {
                            if (stock)
                                ChangeRender(true, stock);
                            stock = null;
                            Destroy(PreviewTuilePlateau);
                            PreviewTuilePlateau = null;
                            scorePreview = 0;

                        }
                    }
                    
                }
                scoreText.text = score.ToString() + " + " + scorePreview;
            }
            else
            {
                if(stock)
                    ChangeRender(true, stock);
                stock = null;
                Destroy(PreviewTuilePlateau);
                PreviewTuilePlateau = null;
                scorePreview = 0;
            }
        }
        else
        {
            Debug.Log("pêrdu");
        }
    }

    public void ChangeRender(bool targetState,GameObject Target)
    {
        if (Target)
        {
            if (Target.GetComponent<MeshRenderer>())
                Target.GetComponent<MeshRenderer>().enabled = targetState;
            foreach (Transform child in Target.transform)
            {
                if (child.gameObject.GetComponent<MeshRenderer>())
                    child.gameObject.GetComponent<MeshRenderer>().enabled = targetState;
            }
        }
    }


    public void ApplyScoring()
    {
        
        scoreText.text = score.ToString();
        piocheText.text = pioche.nbPioche.ToString();
    }

    public void NextTurn()
    {
        checkBoardLvlUp();
        ApplyScoring();
        pioche.Draw();
        ChangePreview();
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

    public void PoseTuile(string tuileAPoser,GameObject aRemplacer)
    {
        TuileConfig cibleScript = aRemplacer.GetComponent<TuileConfig>();
        board.grille[cibleScript.posx,cibleScript.posy] = Instantiate(codex.TouteTuiles[tuileAPoser], aRemplacer.transform.position, aRemplacer.transform.rotation);
        board.grille[cibleScript.posx, cibleScript.posy].GetComponent<TuileConfig>().posx = cibleScript.posx;
        board.grille[cibleScript.posx, cibleScript.posy].GetComponent<TuileConfig>().posy = cibleScript.posy;
        Destroy(aRemplacer);
        pose = true;
    }

    public string FusionneTuile(string tuileAPoser)
    {
        level =  GetLevelTile(selectedObject.tag);
        newtag = GetTag(selectedObject.tag);
        newtag = newtag + codex.TouteTuiles[tuileAPoser].tag.Substring(0, codex.TouteTuiles[tuileAPoser].tag.Length - 1);
        newtag = Alphabetize(newtag);
        newtag += level;
        return newtag ;

    }

    public float ScoringPreview(string tuilePose, int x, int y)
    {
        float scoreSup = 0;
        foreach (GameObject check in checkAround(x, y))
        {
            if (check.tag != "vide")
            {
                Debug.Log(check);
                scoreSup += GetLevelTile(check.tag);

            }
            if (GetTag(tuilePose) == "F" && GetTag(check.tag) == "V")
            {
                scoreSup += GetLevelTile(check.tag);
            }
            if (GetTag(tuilePose) == "V" && GetTag(check.tag) == "E")
            {
                scoreSup += GetLevelTile(check.tag);
            }
            if (GetTag(tuilePose) == "C" && GetTag(check.tag) == "F")
            {
                scoreSup += GetLevelTile(check.tag);
            }
            if (GetTag(tuilePose) == "E" && GetTag(check.tag) == "C")
            {
                scoreSup += GetLevelTile(check.tag);
            }

        }
        return scoreSup;
    }

    public float Scoring(string tuilePose,int x,int y)
    {
        float scoreSup = 0;
        foreach(GameObject check in checkAround(x,y))
        {
            if(check.tag != "vide")
            {
                Debug.Log(check);
                scoreSup += GetLevelTile(check.tag);
                
            }
            if(GetTag(tuilePose) == "F" && GetTag(check.tag) == "V")
            {
                scoreSup += GetLevelTile(check.tag);
            }
            if (GetTag(tuilePose) == "V" && GetTag(check.tag) == "E")
            {
                scoreSup += GetLevelTile(check.tag);
            }
            if (GetTag(tuilePose) == "C" && GetTag(check.tag) == "F")
            {
                scoreSup += GetLevelTile(check.tag);
            }
            if (GetTag(tuilePose) == "E" && GetTag(check.tag) == "C")
            {
                scoreSup += GetLevelTile(check.tag);
            }

        }
        return scoreSup;
    }
    

    public string Alphabetize(string s)
    {
        // Convert to char array.
        // Convert to char array.
        char[] a = s.ToCharArray();

        // Sort letters.
        Array.Sort(a);

        // Return modified string.
        return new string(a);
    }

    public void checkBoardLvlUp()
    {
        
        foreach(GameObject check in board.grille)
        {
            if(check.tag != "vide")
            {
                if (GetLevelTile(check.tag) < 3)
                {
                    int posix = check.GetComponent<TuileConfig>().posx;
                    int posiy = check.GetComponent<TuileConfig>().posy;
                    int newLevel = GetLevelTile(check.tag) + 1;
                    bool canLevelUp = true;
                    foreach (char biome in GetTag(check.tag))
                    {
                        
                        if (biome.ToString() == "F")
                        {
                            if (!CheckTileIndividualy(posix, posiy, "C", "E", newLevel))
                            {

                                canLevelUp = false;
                            }
                        }
                        if (biome.ToString() == "C")
                        {
                            if (!CheckTileIndividualy(posix, posiy, "E", "V", newLevel))
                            {

                                canLevelUp = false;
                            }
                        }
                        if (biome.ToString() == "E")
                        {
                            if (!CheckTileIndividualy(posix, posiy, "V", "F", newLevel))
                            {

                                canLevelUp = false;
                            }
                        }
                        if (biome.ToString() == "V")
                        {
                            if (!CheckTileIndividualy(posix, posiy, "F", "C", newLevel))
                            {

                                canLevelUp = false;
                            }
                        }

                    }
                    if(canLevelUp)
                    {
                        PoseTuile(GetTag(check.tag) + newLevel, check);
                    }
                }
            }
        }
    }
    public void ChangePreview()
    {
        GameObject asupr = PreviewTile;
        PreviewTile = Instantiate(codex.TouteTuiles[pioche.current], PreviewTile.transform.position, PreviewTile.transform.rotation);
        PreviewTile.layer = LayerMask.NameToLayer("Preview");
        foreach(Transform child in PreviewTile.transform)
        {
            child.gameObject.layer = LayerMask.NameToLayer("Preview");
        }
        Destroy(asupr );
    }
    public bool CheckTileIndividualy(int x,int y,string tagCheck, string tagCount,int levelUp)
    {
        bool checkUpgrade = false;
        int levelMini = 0;
        foreach (GameObject check in checkAround(x,y))
        {
            if(GetTag(check.tag) == tagCheck)
            {
                checkUpgrade = true;
            }
            if(GetTag(check.tag) == tagCount)
            {
                levelMini += GetLevelTile(check.tag);
            }
        }
        if(checkUpgrade && levelMini >= levelUp)
            return true;
        else
            return false;
        
    }


    public List<GameObject> checkAround(int x, int y)
    {
            List<GameObject> tuilesAutour = new List<GameObject>();
            if (y != 11)
            {
                tuilesAutour.Add(board.grille[x, y + 1]);//tuile du dessus
                if (x != 11)
                {
                    tuilesAutour.Add(board.grille[x + 1, y + (x % 2) ]);//coter droit haut
                }                    
                if (x != 0)          
                {                    
                    tuilesAutour.Add(board.grille[x - 1, y + (x % 2)]);//coter gauche haut
                }

            }
            if (y != 0)
            {
                tuilesAutour.Add(board.grille[x, y - 1]);//tuile du dessous
                if (x != 11)
                {
                    tuilesAutour.Add(board.grille[x + 1, y -1 + (x % 2)]);//coter droit bas
                }
                if (x != 0)
                {
                    tuilesAutour.Add(board.grille[x - 1, y -1+ (x % 2)]);//coter gauche bas
                }

            }
            
            return tuilesAutour;

    }

  
    public string GetTag(string tagg)
    {
        return tagg.Substring(0, tagg.Length-1 );
    }
    public int GetLevelTile(string tagg)
    { 
        return int.Parse(tagg.Substring(tagg.Length - 1, 1));
    }
}
