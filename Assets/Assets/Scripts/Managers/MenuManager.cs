using System.Collections.Generic;
using UnityEngine;


    public class MenuManager : MonoBehaviour
    {
        public Stack<GameObject> menuStack = new Stack<GameObject>();
        public GameObject mainMenu;
        public GameObject settingsMenu;
        public GameObject civilizationMenu;

        public static MenuManager Instance { get; private set; }

        private void Awake() => this.menuStack.Push(this.mainMenu);

        public void ChangeMenu(Menus menus, GameObject closingMenu)
        {
            if ((Object)this.menuStack.Peek() != (Object)closingMenu)
            {
                this.menuStack.Push(closingMenu);
                closingMenu.SetActive(false);
            }
            else
                closingMenu.SetActive(false);
            switch (menus)
            {
                case Menus.MainMenu:
                    this.menuStack.Push(this.mainMenu);
                    this.mainMenu.SetActive(true);
                    break;
                case Menus.Settings:
                    this.menuStack.Push(this.settingsMenu);
                    this.settingsMenu.SetActive(true);
                    break;
                case Menus.CivilizationSelect:
                    this.menuStack.Push(this.civilizationMenu);
                    this.civilizationMenu.SetActive(true);
                    break;
            }
            Debug.Log((object)"switch reached");
        }

        public void BackButton(GameObject closingMenu)
        {
            if ((Object)this.menuStack.Peek() == (Object)closingMenu)
            {
                this.menuStack.Pop();
                closingMenu.SetActive(false);
                this.menuStack.Peek().SetActive(true);
            }
            else
                Debug.Log((object)":)");
        }
    }
