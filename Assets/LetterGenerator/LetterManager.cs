using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterManager : MonoBehaviour
{
    public static LetterManager Instance;

    public List<Material> colors;
    public List<Transform> spawnPoints;
    public GameObject letterPrefab;

    public List<LetterSlot> letterSlots;

    public List<string> sequenceList;

    public string targetSentence;

    public GameObject successObject;

    private List<GameObject> spwanedItems;

    private int sequenceIndex;

    void Start()
    {
        Instance = this;
        sequenceIndex = -1;
        successObject.SetActive(false);
        spwanedItems = new List<GameObject>();
        SpawnLetters();
    }

    void SpawnLetters()
    {
        for (int i = 0; i < letterSlots.Count; i++)
        {
            letterSlots[i].gameObject.SetActive(false);
            letterSlots[i].ClearSlot();
        }
        successObject.SetActive(false);


        sequenceIndex = sequenceIndex + 1;
        if (sequenceIndex >= sequenceList.Count)
        {
            sequenceIndex = 0;
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
    }

    
    void Update()
    {
        
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

        successObject.SetActive(correct);

        if (correct)
        {
            StartCoroutine(StartNewSequence());
        }
        
    }


    IEnumerator StartNewSequence()
    {
        yield return new WaitForSeconds(5.0f);
        SpawnLetters();
    }
    

}
