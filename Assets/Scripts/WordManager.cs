using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordManager : MonoBehaviour
{
    public static WordManager Instance;
    public List<LetterSlot> letterSlots = new List<LetterSlot>();
    public string targetWord = "Kafee";

    public List<string> words;

    public GameObject lamp;
    
    void Start()
    {
        Instance = this;
    }

    public void CheckWord()
    {
        
    }
    

}
