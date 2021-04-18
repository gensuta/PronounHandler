using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace PronounHandler
{
    // Makes filling pronoun details easier! With autofill! C:
   /* 
    [CustomEditor(typeof(Pronoun))]
    public class PronounEditor : Editor
    {
        int chosenOption = 0;

        public override void OnInspectorGUI()
        {
            Pronoun myPronoun = (Pronoun)target;

            GUIStyle boldStyle = new GUIStyle();
            boldStyle.fontStyle = FontStyle.Bold;


            EditorGUI.BeginChangeCheck();


            // chosen option autofills the current pronoun
            chosenOption = EditorGUILayout.Popup("Autofill Option", chosenOption, PronounHolder.Instance._visibleOptions);

            if (EditorGUI.EndChangeCheck())
            {
                if (chosenOption != PronounHolder.Instance._visibleOptions.Length - 1)
                {
                    myPronoun = PronounHolder.Instance.AutoFillPronoun(chosenOption, myPronoun);
                    Debug.Log("Autofilled characters pronoun to " + myPronoun._subject);
                }
            }

            EditorGUI.indentLevel++;

            EditorGUILayout.LabelField("Subject");
            myPronoun._subject = EditorGUILayout.TextField(myPronoun._subject);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Object");
            myPronoun._object = EditorGUILayout.TextField(myPronoun._object);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Possesive");
            myPronoun._possesive = EditorGUILayout.TextField(myPronoun._possesive);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Possesive Pronoun");
            myPronoun._possessivePronoun = EditorGUILayout.TextField(myPronoun._possessivePronoun);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Reflexive");
            myPronoun._reflexive = EditorGUILayout.TextField(myPronoun._reflexive);
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(3);

            myPronoun.isSingular = GUILayout.Toggle(myPronoun.isSingular, "Is the pronoun singular?");


            EditorGUI.indentLevel--;

        }
    }
    */
}
