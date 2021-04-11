using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace PronounHandler
{
    /// <summary>
    /// Character Editor allows for Character names and pronouns to be easily edited in the inspector
    /// </summary>
    [CustomEditor(typeof(Character))]
    public class CharacterEditor : Editor
    {
        int chosenOption = 0;
        public override void OnInspectorGUI()
        {
            Character myCharacter = (Character)target;

            GUIStyle boldStyle = new GUIStyle();
            boldStyle.fontStyle = FontStyle.Bold;



            // start editing the names and adding/removing names for this character

            GUILayout.Label("Name(s)", boldStyle);


            int tracker = 0;

            foreach (string s in myCharacter.names.ToArray())
            {

                EditorGUILayout.BeginHorizontal();
                myCharacter.names[tracker] = EditorGUILayout.TextField(myCharacter.names[tracker]);

                if (GUILayout.Button("Remove Name"))
                {
                    myCharacter.names.Remove(s);
                }

                EditorGUILayout.EndHorizontal();

                tracker++;
            }


            if (GUILayout.Button("Add Name"))
            {
                string newName = "new name";

                myCharacter.AddName(newName);
            }


            // start editing the pronouns and adding/removing pronouns for this character

            GUILayout.Space(15);

            GUILayout.Label("Pronouns", boldStyle);


            GUILayout.Space(5);

            myCharacter.hasAnyPronouns = GUILayout.Toggle(myCharacter.hasAnyPronouns, "Has any pronouns?");

            myCharacter.hasNoPronouns = GUILayout.Toggle(myCharacter.hasNoPronouns, "Has no pronouns?");

            // only show pronouns if the character actually uses them!
            if (!myCharacter.hasNoPronouns)
            {

                if (!myCharacter.hasAnyPronouns)
                {
                    for (tracker = 0; tracker < myCharacter.pronouns.Count; tracker++)
                    {
                        Pronoun p = myCharacter.pronouns[tracker];

                        EditorGUILayout.BeginVertical();

                        EditorGUI.BeginChangeCheck();


                        // chosen option autofills the current pronoun
                        chosenOption = EditorGUILayout.Popup("Autofill Option", chosenOption, myCharacter._visibleOptions);

                        if (EditorGUI.EndChangeCheck())
                        {
                            if (chosenOption != myCharacter._visibleOptions.Length - 1)
                            {
                                p = myCharacter.AutoFillPronoun(chosenOption, p);
                                Debug.Log("Autofilled characters pronoun to " + p._subject);
                            }
                        }

                        EditorGUI.indentLevel++;
                        EditorGUILayout.BeginHorizontal();

                        EditorGUILayout.LabelField("Subject");
                        p._subject = EditorGUILayout.TextField(p._subject);
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Object");
                        p._object = EditorGUILayout.TextField(p._object);
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Possesive");
                        p._possesive = EditorGUILayout.TextField(p._possesive);
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Possesive Pronoun");
                        p._possessivePronoun = EditorGUILayout.TextField(p._possessivePronoun);
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Reflexive");
                        p._reflexive = EditorGUILayout.TextField(p._reflexive);
                        EditorGUILayout.EndHorizontal();

                        GUILayout.Space(3);

                        p.isSingular = GUILayout.Toggle(p.isSingular, "Is the pronoun singular?");


                        EditorGUI.indentLevel--;

                        if (GUILayout.Button("Remove Pronoun"))
                        {
                            myCharacter.pronouns.Remove(p);
                        }
                        EditorGUILayout.EndVertical();

                        GUILayout.Space(10);
                    }

                    GUILayout.Space(15);

                    if (GUILayout.Button("Add Pronoun"))
                    {
                        myCharacter.pronouns.Add(new Pronoun());
                    }
                }
                else
                {
                    for (tracker = 0; tracker < myCharacter.unfavoredPronouns.Count; tracker++)
                    {
                        Pronoun p = myCharacter.unfavoredPronouns[tracker];

                        EditorGUILayout.BeginVertical();

                        EditorGUI.BeginChangeCheck();


                        // chosen option autofills the current pronoun
                        chosenOption = EditorGUILayout.Popup("Pronoun Option", chosenOption, myCharacter._visibleOptions);

                        if (EditorGUI.EndChangeCheck())
                        {
                            if (chosenOption != myCharacter._visibleOptions.Length - 1)
                            {
                                p = myCharacter.AutoFillPronoun(chosenOption, p);
                                Debug.Log("Autofilled characters pronoun to " + p._subject);
                            }
                        }

                        EditorGUI.indentLevel++;
                        EditorGUILayout.BeginHorizontal();

                        EditorGUILayout.LabelField("Subject");
                        p._subject = EditorGUILayout.TextField(p._subject);
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Object");
                        p._object = EditorGUILayout.TextField(p._object);
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Possesive");
                        p._possesive = EditorGUILayout.TextField(p._possesive);
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Possesive Pronoun");
                        p._possessivePronoun = EditorGUILayout.TextField(p._possessivePronoun);
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Reflexive");
                        p._reflexive = EditorGUILayout.TextField(p._reflexive);
                        EditorGUILayout.EndHorizontal();


                        EditorGUI.indentLevel--;

                        if (GUILayout.Button("Remove Unfavored Pronoun"))
                        {
                            myCharacter.unfavoredPronouns.Remove(p);
                        }
                        EditorGUILayout.EndVertical();

                        GUILayout.Space(10);
                    }

                    GUILayout.Space(15);

                    if (GUILayout.Button("Add Unfavored Pronoun"))
                    {
                        myCharacter.unfavoredPronouns.Add(new Pronoun());
                    }
                }
            }

            GUILayout.Space(5);



            if (GUILayout.Button("Refresh Pronoun Options"))
            {
                myCharacter.RefreshPronounOptions();
            }


        }
    }
}