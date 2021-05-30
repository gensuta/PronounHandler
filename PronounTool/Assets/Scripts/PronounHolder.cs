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
        // Can be refreshed to add pronoun sOs from a folder. Allows pronouns to be autofilled for characters
        public List<Pronoun> pronounOptions;

        // options are what's going to be displayed in the dropdown menu in the inspector
       // public string[] _visibleOptions;

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
        }

     /*   public void RefreshPronounOptions()
         {

             pronounOptions = new List<Pronoun>();

             // this should search and add in pronouns from a folder

             _visibleOptions = new string[pronounOptions.Count + 1];

             for (int i = 0; i < (pronounOptions.Count); i++)
             {
                 _visibleOptions[i] = pronounOptions[i]._subject;
             }

             // When other is chosen the player can create a new pronoun that should be save to the list/directory somehow
             // shouldn't pop up for autofill in inspector probably
             _visibleOptions[pronounOptions.Count] = "Other";
            

             Debug.Log("Successfully updated pronoun options!");
         }*/

         public Pronoun AutoFillPronoun(int opt, Pronoun p = null)
         {
             if (p != null)
             {
                 p._subject = pronounOptions[opt]._subject;
                 p._object = pronounOptions[opt]._object;
                 p._possesive = pronounOptions[opt]._possesive;
                 p._possessivePronoun = pronounOptions[opt]._possessivePronoun;
                 p._reflexive = pronounOptions[opt]._reflexive;


             }
             return pronounOptions[opt];
         }
    }
}