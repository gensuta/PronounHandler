using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PronounHandler;
using UnityEngine.UI;
/// <summary>
/// This script displays all the pronouns the player can choose and 
/// stores it into a Character ( PLEASE FIGURE THIS OUT!!!)
/// </summary>
public class PronounChooser : MonoBehaviour
{
    [SerializeField] PronounHolder pronounHolder;
    [SerializeField] GameObject pronounButton, buttonHolder;
    List<Button> pronounButtons;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayPronouns()
    {
        foreach(Pronoun p in pronounHolder.pronounOptions)
        {
            GameObject g = Instantiate(pronounButton, buttonHolder.transform);

            pronounButtons.Add(g.GetComponent<Button>()); // may not need this list. rethink abt this next week

            // script for pronounButtons specifcally so it holds a pronoun and when pressed it is added to a character and when
            // pressed again it's removed from the characters pronouns
            // maybe this script handles the colors as well!

            // Pronoun button on = Yellow
            // Pronoun button off = Grey
        }
    }

    // display if you want to have any pronouns or no pronouns ( three buttons. any prns. no prns. continue)
    // IF ANY
        // display all available pronouns with the question "which prns do you NOT want tho?"
        // player can select all prns they don't want and hit continue
        // make sure the character has anyPrns to TRUE and store any selecred prns into unfavorable
        //continue to the rest of the game

    // If NO
        // continue to the rest of the game lol

    //IF CONTINUE
        // display all available pronouns + OTHER
        
        // player chooses all the pronouns they want

        // IF OTHER is chosen and they hit continue
            // display all grammatical forms of a pronoun ( object, subject, possesive, pronounPossesive, reflexive )
            // allow player to type in what they want
            // When they hit done store new pronoun!! - ASK SUPERVISOR ABT THIS!!!!!!!
            // They can choose to add another prn afterwards OR they can hit continue

        // store pronouns into a character 



    // NOT IN THIS SCRIPT BUT
    // for choosing names you insert one and then are asked if you'd like to submit another one for yourself
}
