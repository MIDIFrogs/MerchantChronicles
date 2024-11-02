using System;
using UnityEngine;

namespace MerchantChronicles.DialogSystem
{
    [Serializable]
    public class Replic
    {
        [SerializeField] private ReplicAuthor author;
        [SerializeField][Multiline] private string message;
        [SerializeField] private AudioClip voice;

        public ReplicAuthor Author => author;

        public AudioClip Voice => voice;

        public string Message => message;
    }
}
