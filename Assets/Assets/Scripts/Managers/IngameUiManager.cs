using System.Collections.Generic;
using UnityEngine;

namespace TowerWar
{
    public class IngameUiManager : MonoBehaviour
    {
        public Stack<GameObject> menuStack = new Stack<GameObject>();
        public GameObject GameplayUi;
        public GameObject IngameSettings;

        public static IngameUiManager Instance { get; private set; }

        private void Awake()
        {
            this.menuStack.Push(this.GameplayUi);
            if ((Object)IngameUiManager.Instance == (Object)null)
                IngameUiManager.Instance = this;
            else
                Object.Destroy((Object)this.gameObject);
        }

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
                case Menus.GameplayUi:
                    this.menuStack.Push(this.GameplayUi);
                    this.GameplayUi.SetActive(true);
                    break;
                case Menus.IngameSettings:
                    this.menuStack.Push(this.IngameSettings);
                    this.IngameSettings.SetActive(true);
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
}
