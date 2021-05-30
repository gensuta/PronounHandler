using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This script replaces the tags in PronounHandler so that the proper pronouns
/// are displayed for any characters currently speaking
/// </summary>

namespace PronounHandler
{    
    public class LineDecipherer : MonoBehaviour
    {

        public static LineDecipherer Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"> current line we're deciphering </param>
        /// <param name="currentCharacter"> current character that's speaking</param>
        /// <returns> newly parsed line with appropiate pronouns and grammar! </returns>
        public string DecipherLine(string line, Character currentCharacter)
        {
            // if the tag is capitalized like [Object] we want to use He, She or They, instead of he, shey, or they
            // if the tag is entirely in uppercase we ant to capitalize the entire pronoun for example [OBJECT] would make this appear - where is HE going



            // he's she's singular adding 's
            // they're plural adding 're 
            // ['s] - apostrophe tag

            string s = line;
            string lowerString = s.ToLower();

            if (s.Contains("[name]")) s = s.Replace("[name]", currentCharacter.GetRandomName());
            if (s.Contains("[NAME]")) s = s.Replace("[NAME]", currentCharacter.GetRandomName().ToUpper());


            if (lowerString.Contains("[object]")) s = ReplacePronoun("[object]", s, currentCharacter);

            if (lowerString.Contains("[subject]")) s = ReplacePronoun("[subject]", s, currentCharacter);

            if (lowerString.Contains("[possesive]")) s = ReplacePronoun("[possesive]", s, currentCharacter);

            if (lowerString.Contains("[possessivePronoun]")) s = ReplacePronoun("[possessivePronoun]", s, currentCharacter);

            if (lowerString.Contains("[reflexive]")) s = ReplacePronoun("[reflexive]", s, currentCharacter);

            if (lowerString.Contains("[is]")) 
            {
                string isString = currentCharacter.LastPronounUsed.usesIs || currentCharacter.hasNoPronouns ? "is" : "are";

                s = s.Replace("[is]", isString);
                s = s.Replace("[IS]", isString.ToUpper());
                s = s.Replace("[Is]", CapitalizeFirstLetter(isString, true));

            }

            if (lowerString.Contains("[was]"))
            {
                string wasString = currentCharacter.LastPronounUsed.usesIs || currentCharacter.hasNoPronouns ? "was" : "were"; // this is called ternary!! You can use one clause using &&

                s = s.Replace("[was]", wasString);
                s = s.Replace("[WAS]", wasString.ToUpper());
                s = s.Replace("[Was]", CapitalizeFirstLetter(wasString, true));
            }

            if (lowerString.Contains("['s]"))
            {
                string apostropheString = currentCharacter.LastPronounUsed.usesIs || currentCharacter.hasNoPronouns ? "'s" : "'re"; // this is called ternary!! You can use one clause using &&

                s = s.Replace("['s]", apostropheString);
                s = s.Replace("['S]", apostropheString.ToUpper());
            }




            // if we detect [[ singular | plural]]  [[ likes | like ]] we need to replace it with the proper verb
            // Please find a better alternative  i hate this lol
            // We need to make sure that multiple verbs can exist within the same sentence
            // maybe have an array for each of these square brackets and |


            // we're getting all the possible verb tags!
            // we're starting at the first tag we see
            // and saying that as long as index is greater than 0
            //keep deciphering stuff!
            // index will equal 0 when [[ can't be found

            for (int startTag = s.IndexOf("[["); startTag >= 0; startTag = s.IndexOf("[[", startTag + 1))
            {
                int middleTag = s.IndexOf('|',startTag);
                int endTag = s.IndexOf("]]",startTag);

                if (startTag != -1 && middleTag != -1 && endTag != -1)
                {
                    string singularWord = s.Substring(startTag + 2, middleTag - startTag - 2);
                    string pluralWord = s.Substring(middleTag + 1, endTag - middleTag - 1);

                    // please rename this variable
                    string originalString = "[[" + singularWord + "|" + pluralWord + "]]";
                    string replacedWord = currentCharacter.LastPronounUsed.usesIs || currentCharacter.hasNoPronouns ? singularWord : pluralWord;
                    replacedWord = replacedWord.Trim();

                    s = s.Replace(originalString, replacedWord);
                }
                else
                {
                    Debug.LogWarning("It seems like this verb tag is missing something! Please check the current line\n" + line);
                }
            }

           
          

            //maybe add a lil error in the future so that it's easy to trace back to if someone forgot to add | or ]]



            return s;
        }

        string ReplacePronoun(string tag, string currentLine, Character currentCharacter)
        {

            string newLine = currentLine;

            int startTag = tag.IndexOf('[') + 1;
            int endTag = tag.IndexOf(']');

            string actualTag = tag.Substring(startTag, endTag - startTag).ToLower();

            if (currentLine.Contains(actualTag[0].ToString().ToUpper())) // if the first character of the tag is capitalized in the currentline EX: [Object]
            {
                string amendedTag = "[" + CapitalizeFirstLetter(actualTag, true) + "]";
                newLine = newLine.Replace(amendedTag, currentCharacter.GetRandomPronoun(actualTag, true)); // replacing first letter upper case tag
            }

            if (currentLine.Contains(actualTag.ToUpper())) // if the entire tag is capitalized in the currentline EX: [OBJECT]
            {
                newLine = newLine.Replace(tag.ToUpper(), currentCharacter.GetRandomPronoun(actualTag).ToUpper()); // replacing fully upper case tag
            }

            newLine = newLine.Replace(tag, currentCharacter.GetRandomPronoun(actualTag).ToLower()); // replacing normal lowercase tag if it's there

            return newLine;
        }

        public static string CapitalizeFirstLetter(string word, bool capitalized = false) // kinda goofy but if you want the letter capitalized we return 
        {
            if (capitalized)
            {
                string firstLetter = word[0].ToString().ToUpper(); // getting the first letter and capitalizing
                word = firstLetter + word.Substring(1, word.Length - 1);
            }
            return word;
        }
    }
}