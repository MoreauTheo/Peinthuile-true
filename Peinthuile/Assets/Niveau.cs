using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[ExecuteInEditMode]
public class Niveau : ScriptableObject
{
    // Start is called before the first frame update

    [SerializeField][TextArea(12, 12)] private string content;
    public string Content => content;
}
