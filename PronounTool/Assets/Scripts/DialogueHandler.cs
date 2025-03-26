using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PronounHandler;
using TMPro;
/// <summary>
/// This script is to show an example of how the pronoun tool can work
/// </summary>
public class DialogueHandler : MonoBehaviour
{
    [TextArea(2,5)]
    [SerializeField] string[] dialogue;

    [SerializeField] int currentLine; // current line we're on in the dialogue array

    public Character currentCharacter;

    [SerializeField] TextMeshProUGUI dialogueText, nameText;
    [SerializeField] GameObject textBox;

    // Start is called before the first frame update
    void Start()
    {
        EnableTextBox();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ShowNextLine();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void ShowNextLine() // shows the next line of dialogue
    {
        if(currentLine < dialogue.Length)
        {
            dialogueText.text = LineDecipherer.Instance.DecipherLine(dialogue[currentLine],currentCharacter);
            nameText.text = currentCharacter.GetRandomName();

        }
        else
        {
            DisableTextBox();
            Debug.Log("No more dialogue to show!");
        }
        currentLine++;
    }

    /// <summary>
    /// 
    /// </summary>
    public void EnableTextBox()
    {
        textBox.SetActive(true);
        ShowNextLine();
    }

    /// <summary>
    /// 
    /// </summary>
    public void DisableTextBox()
    {
        textBox.SetActive(false);
    }
}
