using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PronounHandler;
/// <summary>
/// This script is to show an example of how the pronoun tool can work
/// </summary>
public class DialogueHandler : MonoBehaviour
{
    [TextArea(0,2)]
    [SerializeField] string[] dialogue;

    [SerializeField] int currentLine; // current line we're on in the dialogue array

    [SerializeField] Character myCharacter;

    // Start is called before the first frame update
    void Start()
    {
        ShowNextLine();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ShowNextLine();
        }
    }

    public void ShowNextLine() // shows the next line of dialogue
    {
        currentLine++;

        if(currentLine < dialogue.Length)
        {
            Debug.Log(myCharacter.DecipherLine(dialogue[currentLine]));

        }
        else
        {
            Debug.Log("No more dialogue to show!");
        }
    }
}
