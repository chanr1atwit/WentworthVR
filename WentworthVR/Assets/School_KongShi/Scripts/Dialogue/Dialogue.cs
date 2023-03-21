using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string speakerName;
    [TextArea(3, 10)]
    public List<string> sentences;
    [TextArea(3, 10)]
    public string replaySentence;
}
