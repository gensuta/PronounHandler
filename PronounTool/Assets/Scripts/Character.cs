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

        public string GetRandomName() 
        {
            return names[Random.Range(0, names.Count)];
        }

        public string GetRandomPronoun(string type)
        {

            if (!hasNoPronouns && !hasAnyPronouns) // if you have specific pronouns you use
            {
                int randP = Random.Range(0, pronouns.Count);

                lastPronounUsed = pronouns[randP];

                switch (type)
                {
                    case ("subject"):
                        return pronouns[randP]._subject;
                    case ("object"):
                        return pronouns[randP]._object;
                    case ("possesive"):
                        return pronouns[randP]._possesive;
                    case ("possessivePronoun"):
                        return pronouns[randP]._possessivePronoun;
                    case ("reflexive"):
                        return pronouns[randP]._reflexive;
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
                            return randomPronoun._subject;
                        case ("object"):
                            return randomPronoun._object;
                        case ("possesive"):
                            return randomPronoun._possesive;
                        case ("possessivePronoun"):
                            return randomPronoun._possessivePronoun;
                        case ("reflexive"):
                            return randomPronoun._reflexive;
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


        public string DecipherLine(string line)
        {
            // [name], [obj] [sbj] [pos] [posP] [refl], [is] [was] , [likes|like] remember to trim! maybe get rid of is/are
            // give positions for each to possibly get rid of these if statements :0
            // tags!! let's call them tags and search the SO to swap it out

            // research on how can you parse strings and minimizing the amt of garbage that's being generated 
            //load in all strings and parse during loading screen and delete garbage generated before player gets in the game



            string s = line;
           
            if (s.Contains("[name]")) s = s.Replace("[name]", GetRandomName());

            if (s.Contains("[object]")) s = s.Replace("[object]", GetRandomPronoun("object"));

            if (s.Contains("[subject]")) s = s.Replace("[subject]", GetRandomPronoun("subject"));

            if (s.Contains("[possesive]")) s = s.Replace("[possesive]", GetRandomPronoun("possesive"));

            if (s.Contains("[possessivePronoun]")) s = s.Replace("[possessivePronoun]", GetRandomPronoun("possessivePronoun"));

            if (s.Contains("[reflexive]")) s = s.Replace("[reflexive]", GetRandomPronoun("reflexive"));

            if(s.Contains("[is]"))
            {
                string isString = lastPronounUsed.isSingular ? "is" : "are";

                s = s.Replace("[is]", isString);
            }

            if (s.Contains("[was]"))
            {
                string wasString = lastPronounUsed.isSingular ? "was" : "were"; // this is called ternary!! You can use one clause using &&

                s = s.Replace("[was]", wasString);
            }

            // if we detect [[ singular | plural]]  [[ likes | like ]] we need to replace it with the proper verb
            // Please find a better alternative  i hate this lol

            int startTag = s.IndexOf("[[");
            int middleTag = s.IndexOf('|') ;
            int endTag = s.IndexOf("]]");

            if (startTag != -1 && middleTag != -1 && endTag != -1)
            {
                string singularWord = s.Substring(startTag +2, middleTag - startTag -2);
                string pluralWord = s.Substring(middleTag+1, endTag - middleTag -1);

                // please rename this variable
                string originalString = "[[" + singularWord + "|" + pluralWord + "]]";
                string replacedWord = lastPronounUsed.isSingular ? singularWord : pluralWord;
                replacedWord = replacedWord.Trim();

                s = s.Replace(originalString, replacedWord);
            }

            //maybe add a lil error in the future so that it's easy to trace back to if someone forgot to add | or ]]

           

            return s;
        }

        public void AddName(string n)
        {
            names.Add(n);
        }
    }

}

