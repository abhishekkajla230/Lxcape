using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterManager : MonoBehaviour
{
    public enum GameStates : int
    {
        Idle = 0,
        PlayNarration = 1,
        ArrangingLetters = 2,
        Finished = 3,
    }

    public GameStates currentState = GameStates.Idle;

    public static LetterManager Instance;

    public List<Material> colors;
    public List<Transform> spawnPoints;
    public GameObject letterPrefab;

    public List<LetterSlot> letterSlots;

    public List<string> sentenceList;
    int currentSentence;

    public List<string> sequenceList;

    public string targetSentence;

    public GameScreen gameScreen;

    private List<GameObject> spwanedItems;

    public List<AudioClip> sentenceAudio;

    public AudioClip audioClipNarration;

    private int sequenceIndex;

    bool narrationPlayed = false;

    void Start()
    {
        Instance = this;
        narrationPlayed = false;
        currentState = GameStates.Idle;
        sequenceIndex = -1;
        
        spwanedItems = new List<GameObject>();


        currentSentence = -1;
        CreateSequence();
    }

    void CreateSequence()
    {
        sequenceList.Clear();

        currentSentence = currentSentence + 1;
        if (currentSentence >= sentenceList.Count - 1)
        {
            currentSentence = 0;
        }

        string sentence = sentenceList[currentSentence];
        string [] words = sentence.Split(' ');
        foreach (string word in words)
        {
            sequenceList.Add(word); 
        }
    }

    void SpawnLetters()
    {
        
        sequenceIndex = sequenceIndex + 1;
        if (sequenceIndex >= sequenceList.Count)
        {
            return;
        }
        
        for (int i = 0; i < letterSlots.Count; i++)
        {
            letterSlots[i].gameObject.SetActive(false);
            letterSlots[i].ClearSlot();
        }

        targetSentence = sequenceList[sequenceIndex];

        for (int i = 0; i < spwanedItems.Count; i++)
        {
            Destroy(spwanedItems[i]);
        }
        spwanedItems.Clear();

        List<Transform> spawns = new List<Transform>(spawnPoints);
        for (int i = 0; i < targetSentence.Length; i++)
        {
            int idx = Random.Range(0, spawns.Count);
            Vector3 randomPosition = spawns[idx].position;
            spawns.RemoveAt(idx);

            int idx_c = Random.Range(0, colors.Count);

            GameObject obj = Instantiate(letterPrefab, randomPosition, Quaternion.identity);
            obj.GetComponent<LetterItem>().UpdateData(targetSentence[i].ToString(), colors[idx_c], 22.0f);
            spwanedItems.Add(obj);

            letterSlots[i].gameObject.SetActive(true);
            letterSlots[i].SetCurrentColor(colors[idx_c]);
        }

        UpdateScreenText();
    }

    public void CheckSequence()
    {
        bool correct = true;

        for (int i = 0; i < targetSentence.Length; i++) {
            string targetLetter = targetSentence[i].ToString();
            if (targetLetter != letterSlots[i].GetCurrentLetter())
            {
                correct = false;
                break;
            }
        }

        if (correct)
        {
            if (sequenceIndex >= sequenceList.Count - 1)
            {
                ExecuteFinishState();
            }
            else
            {
                StartCoroutine(StartNewSequence());
            }
        }
        
    }

    void UpdateScreenText()
    {
        string text = "";
        int i = 0;
        for (i = 0; i < sequenceIndex && i < sequenceList.Count; i++)
        {
            text += " " + sequenceList[i].ToString();
        }

        if (currentState == GameStates.Finished)
        {
            text += " " + sequenceList[sequenceList.Count - 1];
        }
        else
        {
            text += "  ";
            for (int k = 0; k < sequenceList[i].Length; k ++)
            {
                text += "_ ";
            }
            
        }

        gameScreen.UpdateText(text);
    }

    IEnumerator StartNewSequence()
    {
        yield return new WaitForSeconds(1.0f);
        SpawnLetters();
    }
    IEnumerator PlayNarration()
    {
        if (narrationPlayed == false)
        {
            narrationPlayed = true;
            GetComponent<AudioSource>().clip = audioClipNarration;
            GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(22f);
            Debug.Log("PlayNarration");
        }
        
        NextWord();
    }

    private void NextWord()
    {
        Debug.Log("NextWord");

        currentState = GameStates.ArrangingLetters;
        sequenceIndex = -1;
        SpawnLetters();
    }

    void ExecuteFinishState()
    {
        Debug.Log("ExecuteFinishState");
        currentState = GameStates.Finished;
        UpdateScreenText();

        GetComponent<AudioSource>().clip = sentenceAudio[currentSentence];
        GetComponent<AudioSource>().Play();
        
        CreateSequence();
        currentState = GameStates.Idle;
    }

    public void OnButtonPressed()
    {
        Debug.Log("OnButtonPressed");
        if (currentState == GameStates.Idle)
        {
            currentState = GameStates.PlayNarration;
            
            StartCoroutine(PlayNarration());
        }
    }

}
