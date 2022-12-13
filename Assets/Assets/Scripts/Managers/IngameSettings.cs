using UnityEngine;
using UnityEngine.UI;

namespace TowerWar
{
    public class IngameSettings : MonoBehaviour
    {
        public Slider soundSlider;
        public Slider musicSlider;

        private void Awake()
        {
            this.soundSlider.value = PlayerPrefs.GetFloat("SoundVolume");
            this.musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        }

        public void OnSoundSliderMoved() => GameSettings.Instance.AudioVolumeSettings(this.soundSlider.value);

        public void OnMusicSliderMoved() => GameSettings.Instance.MusicVolumeSettings(this.musicSlider.value);

        public void OnClickBack()
        {
            GameSettings.Instance.ResumeGame();
            IngameUiManager.Instance.BackButton(this.gameObject);
        }
    }
}
