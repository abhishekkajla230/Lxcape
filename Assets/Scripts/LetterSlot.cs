using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LetterSlot : MonoBehaviour
{
    public int slotIndex;
    public List<LetterItem> letters;

    public Renderer tableCube;

    void Start()
    {
        letters = new List<LetterItem>();
    }

    public void AttachLetter(LetterItem letter)
    {
        letters.Add(letter);
    }

    public void RemoveLetter(LetterItem letter)
    {
        letters.Remove(letter);
    }

    public string GetCurrentLetter()
    {
        if (letters.Count == 1)
        {
            return letters[0].GetLetterText();
        }

        return "#";
    }

    public void SetCurrentColor(Material color) {
        tableCube.material = color;
    }

    public void ClearSlot() { letters.Clear(); }

}
