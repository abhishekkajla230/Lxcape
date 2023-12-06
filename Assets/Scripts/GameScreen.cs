using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameScreen : MonoBehaviour
{
    public TextMeshProUGUI screenText;
    void Start()
    {
        screenText.text = "Test abhishek";
    }

    public void UpdateText(string wordtext)
    {
        screenText.text = wordtext;

    }

}
