using UnityEngine;
using UnityEngine.UI;

namespace TowerWar
{


    public class SettingsScript : MonoBehaviour
    {
        public MenuManager menuManager;
        public Slider soundSlider;
        public Slider musicSlider;
     public GameSettings gameSettings;

        public void OnClickBack() => this.menuManager.BackButton(this.gameObject);

        private void OnEnable()
        {
            this.soundSlider.value = PlayerPrefs.GetFloat("SoundVolume");
            this.musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        }

        public void OnClick()
        {
        }

        public void OnSoundSliderMoved() => this.gameSettings.AudioVolumeSettings(this.soundSlider.value);

          public void OnMusicSliderMoved() => this.gameSettings.MusicVolumeSettings(this.musicSlider.value);
    }

}