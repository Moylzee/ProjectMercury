using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public float txtSpeed;
    private int index;
    public bool isDialogueActive = false;

    private string[] sentences = new string[] {"Date: 29-05-2024\nDays Since Ballylofa was overrun: 225\nDays Since Last Human Sighting: 178\n\nDear Diary,\nI Don't Know How Much Longer I Can Take This.\nI'm Running Out Of Food And Water.\nThe Hordes Are Endless.\nI Will Pick One Of The Guns On My Wall And Leave.\nTodays Goal:\nSURVIVE\n\n(click to exit)"};
    void Start()
    {
        textDisplay.text = "";
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textDisplay.text == sentences[index])
            {
                NextSentence();
            }else {
            StopAllCoroutines();
            textDisplay.text = sentences[index];
            }
        }
    }
    // Start the Dialogue
    void StartDialogue()
    {
        isDialogueActive = true;
        index = 0;
        StartCoroutine(Type());
    }   
    // End the Dialogue
    void EndDialogue() {
        isDialogueActive = false;
    }
    // Type the Letters One-by-One
    IEnumerator Type()
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(txtSpeed);
        }
    }
    // Check if There Are More Sentences
    // Display Next Sentence if there is one 
    // End Dialogue if There's no Other Sentences 
    void NextSentence()
    {
        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "\n";
            StartCoroutine(Type());
        }else {
            gameObject.SetActive(false);
        }
    }
}