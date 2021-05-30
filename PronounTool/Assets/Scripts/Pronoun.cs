using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PronounHandler
{
    [CreateAssetMenu(fileName = "Pronoun", menuName = "Pronoun Handler/Pronoun", order = 0)]
    public class Pronoun : ScriptableObject
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
    }
}