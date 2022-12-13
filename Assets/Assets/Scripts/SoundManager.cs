using System.Collections.Generic;
using UnityEngine;


    public class SoundManager : MonoBehaviour
    {
        public AudioClip[] towerUpgradeClips;
        public AudioClip[] meleeCollisionClips;
        public AudioClip[] heavyCollisionClips;
        public AudioClip[] archerHitClips;
        public AudioSource towerUpgradeAudio;
        public AudioSource meleeCollisionAudio;
        public AudioSource heavyCollisionAudio;
        public AudioSource archerHitAudio;
        public List<AudioSource> allAudios;
        public AudioSource musicAudio;

        private void Awake() => this.LoadAudioSources();

        public void LoadAudioSources()
        {
            this.allAudios.Add(this.towerUpgradeAudio);
            this.allAudios.Add(this.meleeCollisionAudio);
            this.allAudios.Add(this.heavyCollisionAudio);
            this.allAudios.Add(this.archerHitAudio);
        }

        public void LoadClips()
        {
        }
    }
