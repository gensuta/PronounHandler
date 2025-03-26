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
        [SerializeField] List<PronounObject> _pronouns; // pronouns this character uses
        [SerializeField] List<PronounObject> _unfavoredPronouns; // if a character uses any pronouns, this variable is to mark any they don't like
        public bool hasAnyPronouns, hasNoPronouns; // no pronouns means use the character's name! Any pronouns means use any available pronouns for the character

        // TODO: Ask if this was a dumb idea ( the below )
        public List<Pronoun> pronouns;
        public List<Pronoun> unfavoredPronouns;



        Pronoun lastPronounUsed; // keeping the last pronoun used to help with grammar
        public Pronoun LastPronounUsed { get => lastPronounUsed;}


        public void RefreshPronouns()
        {
            foreach(PronounObject p in _pronouns) // adding in all the pronoun objects
            {
                if (!pronouns.Contains(p.pronoun))
                {
                    pronouns.Add(p.pronoun);
                }
            }

            foreach(PronounObject p in _unfavoredPronouns)
            {
                if (!unfavoredPronouns.Contains(p.pronoun))
                {
                    unfavoredPronouns.Add(p.pronoun);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetRandomName() 
        {
            return names[Random.Range(0, names.Count)];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="capitalized"></param>
        /// <returns></returns>
        public string GetRandomPronoun(string type, bool capitalized = false)
        {

            if (!hasNoPronouns && !hasAnyPronouns) // if you have specific pronouns you use
            {
                int randP = Random.Range(0, pronouns.Count);

                lastPronounUsed = pronouns[randP];
                Debug.Log("Setting last pronoun used to: " + pronouns[randP]._object);

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

                    Debug.Log("Setting last pronoun used to: " + randomPronoun._object);

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


  
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Pronoun GetFavoredPronoun() // getting a random favored pronun
        {
            List<Pronoun> favoredPronouns = new List<Pronoun>();

            foreach (Pronoun p in PronounHolder.Instance.pronounOptions)
            {
                if (!isUnfavored(p))
                {
                    favoredPronouns.Add(p);
                }
            }

            return favoredPronouns[Random.Range(0, favoredPronouns.Count)];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pronoun"></param>
        /// <returns></returns>
        bool isUnfavored(Pronoun pronoun) // checking if this pronoun is unfavored by this character
        {
            //TODO: Um? Why is it called unfavored? Maybe we should track the favorites instead of tracking the not favorites?
            if(unfavoredPronouns.Contains(pronoun))
            {
                return true;
            }
            return false;
        }
    }

}

