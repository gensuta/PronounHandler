using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PronounHandler
{
    /// <summary>
    /// The Character class holds all the pronoun information as well as names for specific characters
    /// </summary>
    [CreateAssetMenu(fileName = "Character", menuName = "Character Asset/Character", order = 0)]
    public class Character : ScriptableObject
    {
        public List<string> names; // name(s) this character uses
        public List<Pronoun> pronouns; // pronouns this character uses
        public List<Pronoun> unfavoredPronouns; // if a character uses any pronouns, this variable is to mark any they don't like
        public bool hasAnyPronouns, hasNoPronouns; // no pronouns means use the character's name! Any pronouns means use any available pronouns for the character


        // the list of pronouns being taken from the text file. This contains info that pronounOptions will use
        string[] pronounOptionsText;

        // generated from the pronounOptions text file. Allows pronouns to be autofilled for characters
        List<Pronoun> pronounOptions;

        // options are what's going to be displayed in the dropdown menu in the inspector
        public string[] _visibleOptions;

        Pronoun lastPronounUsed;

        public string GetRandomName() 
        {
            return names[Random.Range(0, names.Count)];
        }

        public string GetRandomPronoun(string type)
        {
            int randP = Random.Range(0, pronouns.Count);

            lastPronounUsed = pronouns[randP];

            if (!hasNoPronouns && !hasAnyPronouns)
            {
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
            else
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
                    randP = Random.Range(0, pronounOptions.Count);
                    lastPronounUsed = pronounOptions[randP];

                    if (CheckIfUnfavored(pronounOptions[randP]))
                    {
                        // implement a way to grab a pronoun that's not unfavored!
                    }

                    switch (type)
                    {
                        case ("subject"):
                            return pronounOptions[randP]._subject;
                        case ("object"):
                            return pronounOptions[randP]._object;
                        case ("possesive"):
                            return pronounOptions[randP]._possesive;
                        case ("possessivePronoun"):
                            return pronounOptions[randP]._possessivePronoun;
                        case ("reflexive"):
                            return pronounOptions[randP]._reflexive;
                    }
                }
            }
            return "!!ERROR!!";
        }


        bool CheckIfUnfavored(Pronoun pronoun) // checking if this pronoun is unfavored by this character
        {
            if(unfavoredPronouns.Contains(pronoun))
            {
                return true;
            }
            return false;
        }


        // PLEASE MOVE THIS FUNCTION SOMEWHERE ELSE LATER
        // We need to differentiate they're and he/he is etc.
        // We need to figure out capatilization stuff as well!! please!!
        // How do we handle words like "likes" when it comes to pronouns? Ex: he likes vs they like
        public string DecipherLine(string line)
        {
            // [name], [obj] [sbj] [pos] [posP] [refl], [is] [was]



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

            return s;
        }


        public Character()
        {
            names = new List<string>();
            pronouns = new List<Pronoun>();
            unfavoredPronouns = new List<Pronoun>();

            names.Add("Default Name");
            pronouns.Add(new Pronoun());
        }

        public void AddName(string n)
        {
            names.Add(n);
        }

        //Places options into the list
        public void RefreshPronounOptions()
        {

            TextAsset mytxtData = Resources.Load("PronounOptions") as TextAsset;

            pronounOptionsText = mytxtData.text.Split('\n'); // every 5 is a new pronoun
            pronounOptions = new List<Pronoun>();

            for (int i = 0; i < pronounOptionsText.Length - 1; i += 5)
            {
                Pronoun p = new Pronoun();


                p._subject = pronounOptionsText[i];
                p._object = pronounOptionsText[i + 1];
                p._possesive = pronounOptionsText[i + 2];
                p._possessivePronoun = pronounOptionsText[i + 3];
                p._reflexive = pronounOptionsText[i + 4];

                pronounOptions.Add(p);
            }


            _visibleOptions = new string[pronounOptions.Count + 1];

            for (int i = 0; i < (pronounOptions.Count); i++)
            {
                _visibleOptions[i] = pronounOptions[i]._subject;
            }

            _visibleOptions[pronounOptions.Count] = "Other";

            Debug.Log("Successfully updated pronoun options!");
        }

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

    [System.Serializable]
    public class Pronoun
    {
        public string _subject, _object, _possesive, _possessivePronoun, _reflexive;
        public bool isSingular; // does this pronoun use is/was or are/were 
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

