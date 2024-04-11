using System.Collections.Generic;
using Ludo.ScriptableObjects;
using UnityEngine;

namespace Ludo.Managers
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;
        [SerializeField] private AudioDB audioDB;
        [SerializeField] private List<AudioSource> audioSources;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        public void Play(AudioName audioName)
        {
            var audio = audioDB.GetAudio(audioName);
            var source = GetFreeAudioSource();
            source.clip = audio.Clip;
            source.volume = audio.Volume;
            source.Play();
        }

        private AudioSource GetFreeAudioSource()
        {
            return audioSources.Find(x => !x.isPlaying);
        }
    }
}