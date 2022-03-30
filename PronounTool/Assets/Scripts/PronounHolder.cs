using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PronounHandler
{
    /// <summary>
    /// Holds all the possible pronouns a character could have
    /// </summary>
    public class PronounHolder : MonoBehaviour
    {



        public List<Pronoun> pronounOptions; // developers can type in pronounOptions or have it transfer from a textfile

       // options are what's going to be displayed in the dropdown menu in the inspector
       public string[] _visibleOptions;

        public static PronounHolder Instance;

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }

            if (this != Instance)
            {
                Destroy(gameObject);
            }

            RefreshPronounOptions(); // TODO: turn into editor button plzzz
        
        }

        public void RefreshPronounOptions()
         {

             if(pronounOptions == null) pronounOptions = new List<Pronoun>();

            //loading in any pronouns from the PronounOptions.txt file!

            string pronounOptionsResource = Resources.Load<TextAsset>("PronounOptions").text;
            string[] pronounList = pronounOptionsResource.Split('\n');
            for(int i = 0; i < pronounList.Length; i+=5)
            {
                string _subject = pronounList[i];
                if (i + 1 >= pronounList.Length)
                {
                    //TODO: Make a nicer comment plz <3
                    Debug.Log("Error! Stopping at " + _subject + " because we don't have enough info.\nWe were about to go out of bounds of the array");
                    break;
                }
                else
                {
                    string _object = pronounList[i + 1];
                    string _possesive = pronounList[i + 2];
                    string _possessivePronoun = pronounList[i + 3];
                    string _reflexive = pronounList[i + 4];

                    Pronoun newPronoun = new Pronoun(_subject, _object, _possesive, _possessivePronoun, _reflexive);

                    //TODO: We need to make sure we're not adding the same exact pronoun twice
                    pronounOptions.Add(newPronoun);

                    Debug.Log("Successfully added " + newPronoun._subject);
                }
            }

            // adding in any pronounObjects we haven't added into the list

            PronounObject[] pronounObjects = Resources.LoadAll<PronounObject>("ScriptableObject/Pronouns/");

            foreach(PronounObject pronounObject in pronounObjects)
            {
                if (!pronounOptions.Contains(pronounObject.pronoun))
                {
                    //TODO: We need to make sure we're not adding the same exact pronoun twice
                    pronounOptions.Add(pronounObject.pronoun);

                    Debug.Log("Successfully added " + pronounObject.pronoun._subject);
                }
            }


            _visibleOptions = new string[pronounOptions.Count + 1];

             for (int i = 0; i < (pronounOptions.Count); i++)
             {
                 _visibleOptions[i] = pronounOptions[i]._subject;
             }

             // When other is chosen a pop up showing all the UI for a custom pronoun should come up
             // and the new pronoun should be easily added to visibleOptions/pronounOptions
             _visibleOptions[pronounOptions.Count] = "Other";
            

             Debug.Log("Successfully updated pronoun options!");
         }

    }
}