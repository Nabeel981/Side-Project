using UnityEngine;
using UnityEngine.SceneManagement;


    public class MainMenuScript : MonoBehaviour
    {
        public MenuManager menuManager;

        public void OnClickSettings() => this.menuManager.ChangeMenu(Menus.Settings, this.gameObject);

        public void OnClickPlay() => this.menuManager.ChangeMenu(Menus.CivilizationSelect, this.gameObject);

        public void OnClickOnline() => SceneManager.LoadScene(1);
    }

