using UnityEngine;

namespace TowerWar
{
    public class GameSettings : MonoBehaviour, IGameSettings
    {
        public SoundManager soundManager;

        public static GameSettings Instance { get; private set; }

        private void Awake()
        {
            if ((Object)GameSettings.Instance == (Object)null)
                GameSettings.Instance = this;
            else
                Object.Destroy((Object)this.gameObject);
        }

        private void Start()
        {
            this.AudioVolumeSettings(PlayerPrefs.GetFloat("SoundVolume"));
            Debug.Log((object)("Audio value recieved " + PlayerPrefs.GetFloat("SoundVolume").ToString()));
            this.MusicVolumeSettings(PlayerPrefs.GetFloat("MusicVolume"));
            Debug.Log((object)("Music value recieved " + PlayerPrefs.GetFloat("MusicVolume").ToString()));
        }

        public void AudioVolumeSettings(float sliderValue)
        {
            PlayerPrefs.SetFloat("SoundVolume", sliderValue);
            Debug.Log((object)("Sound value set to " + sliderValue.ToString()));
            foreach (AudioSource allAudio in this.soundManager.allAudios)
                allAudio.volume = sliderValue;
        }

        public void MusicVolumeSettings(float sliderValue)
        {
            PlayerPrefs.SetFloat("MusicVolume", sliderValue);
            Debug.Log((object)("Music value set to " + sliderValue.ToString()));
            this.soundManager.musicAudio.volume = sliderValue;
        }

        public void VideoSettings()
        {
        }

        public void PauseGame() => Time.timeScale = 0.0f;

        public void ResumeGame() => Time.timeScale = 1f;
    }
}
