using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PronounHandler
{
    //TODO: Ask if it was smart to separate pronoun and create the "pronounobject" class
    [System.Serializable]
    public class Pronoun
    {
        public string _subject, _object, _possesive, _possessivePronoun, _reflexive;
        public bool usesIs; // does this pronoun use is/was or are/were 
        public Pronoun()
        {
            _subject = "they";
            _object = "them";
            _possesive = "their";
            _possessivePronoun = "theirs";
            _reflexive = "themself";
        }

        public Pronoun(string subject,string objectPronoun, string possesive, string possesivePronoun,string reflexive)
        {
            _subject = subject;
            _object = objectPronoun;
            _possesive = possesive;
            _possessivePronoun = possesivePronoun;
            _reflexive = reflexive;
        }
    }
}