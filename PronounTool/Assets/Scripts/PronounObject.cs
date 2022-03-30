using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PronounHandler
{
    [CreateAssetMenu(fileName = "Pronoun", menuName = "Pronoun Handler/Pronoun", order = 0)]
    public class PronounObject : ScriptableObject
    {
        public Pronoun pronoun;
    }
}
