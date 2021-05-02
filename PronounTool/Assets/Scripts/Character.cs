using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PronounHandler
{
    /// <summary>
    /// The Character class holds all the pronoun information as well as names for specific characters
    /// </summary>
    [CreateAssetMenu(fileName = "Character", menuName = "Pronoun Handler/Character", order = 1)]
    public class Character : ScriptableObject
    {
        public List<string> names; // name(s) this character uses
        public List<Pronoun> pronouns; // pronouns this character uses
        public List<Pronoun> unfavoredPronouns; // if a character uses any pronouns, this variable is to mark any they don't like
        public bool hasAnyPronouns, hasNoPronouns; // no pronouns means use the character's name! Any pronouns means use any available pronouns for the character
        
        Pronoun lastPronounUsed; // keeping the last pronoun used to help with grammar
        public Pronoun LastPronounUsed { get => lastPronounUsed;}

        public string GetRandomName() 
        {
            return names[Random.Range(0, names.Count)];
        }

        public string GetRandomPronoun(string type, bool capitalized = false)
        {

            if (!hasNoPronouns && !hasAnyPronouns) // if you have specific pronouns you use
            {
                int randP = Random.Range(0, pronouns.Count);

                lastPronounUsed = pronouns[randP];

                switch (type)
                {
                    case ("subject"):
                        return LineDecipherer.CapitalizeFirstLetter(pronouns[randP]._subject, capitalized);
                    case ("object"):
                        return LineDecipherer.CapitalizeFirstLetter(pronouns[randP]._object, capitalized);
                    case ("possesive"):
                        return LineDecipherer.CapitalizeFirstLetter(pronouns[randP]._possesive, capitalized);
                    case ("possessivePronoun"):
                        return LineDecipherer.CapitalizeFirstLetter(pronouns[randP]._possessivePronoun, capitalized);
                    case ("reflexive"):
                        return LineDecipherer.CapitalizeFirstLetter(pronouns[randP]._reflexive, capitalized);
                }
            }
            else // if you don't use pronouns
            {
                if (hasNoPronouns)
                {
                    switch (type)
                    {
                        case ("subject"):
                            return GetRandomName();
                        case ("object"):
                            return GetRandomName();
                        case ("possesive"):
                            return GetRandomName() + "'s";
                        case ("possessivePronoun"):
                            return GetRandomName() + "'s";
                        case ("reflexive"):
                            return GetRandomName() + "'s self";
                    }
                }

                if (hasAnyPronouns)
                {
                    Pronoun randomPronoun = GetFavoredPronoun();
                    lastPronounUsed = randomPronoun;

                    switch (type)
                    {
                        case ("subject"):
                            return LineDecipherer.CapitalizeFirstLetter(randomPronoun._subject, capitalized);
                        case ("object"):
                            return LineDecipherer.CapitalizeFirstLetter(randomPronoun._object, capitalized);
                        case ("possesive"):
                            return LineDecipherer.CapitalizeFirstLetter(randomPronoun._possesive, capitalized);
                        case ("possessivePronoun"):
                            return LineDecipherer.CapitalizeFirstLetter(randomPronoun._possessivePronoun, capitalized);
                        case ("reflexive"):
                            return LineDecipherer.CapitalizeFirstLetter(randomPronoun._reflexive, capitalized);
                    }
                }
            }
            return "!!ERROR!!";
        }


  

        Pronoun GetFavoredPronoun() // getting a random favored pronun
        {
            List<Pronoun> favoredPronouns = new List<Pronoun>();

            foreach (Pronoun p in PronounHolder.Instance.pronounOptions)
            {
                if (!CheckIfUnfavored(p))
                {
                    favoredPronouns.Add(p);
                }
            }

            return favoredPronouns[Random.Range(0, favoredPronouns.Count)];
        }

        bool CheckIfUnfavored(Pronoun pronoun) // checking if this pronoun is unfavored by this character
        {
            if(unfavoredPronouns.Contains(pronoun))
            {
                return true;
            }
            return false;
        }
    }

}

