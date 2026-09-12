using ED262C;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject[] menus;

    private SimpleArrayStack<MenuType> menuStack;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menuStack = new SimpleArrayStack<MenuType> ();
        menuStack.Push(MenuType.Main);
        Debug.Log("Current menu: " + menuStack.Peek());

        UpdateMenu();
    }

    public void OpenMenu(MenuType menu)
    {
        menuStack.Push(menu);
        UpdateMenu();
        Debug.Log("Opened menu: " + menu);
        Debug.Log("Current menu: " + menuStack.Peek());
    }

    public void GoBack()
    {
        if(menuStack.Count > 1)
        {
            MenuType menuToClose = menuStack.Pop();
            //switch (menuToClose)
            //{
            //    case MenuType.Options:
            //        optionsMenu.SetActive(false);
            //        break;
            //    case MenuType.Controls:
            //        controlsMenu.SetActive(false);
            //        break;
            //}
            UpdateMenu();

            Debug.Log("Went back to: " + menuStack.Peek());
        }
    }

    public void OpenMenuInt(int enumIndex)
    {
        if(enumIndex < 0 || enumIndex > menus.Length)
        {
            Debug.LogWarning("Invalid menu index: " + enumIndex);
        }
        OpenMenu((MenuType)enumIndex);
    }

    public void UpdateMenu()
    {
        // Sacan del stack un MenuType, y lo castean a int
        // Le dan SetActive al indice correspondiente
        // ej: si reciben MenuType.Main, abre menus[i]
        for (int i = 0; i < menus.Length; i++)
        {
            menus[i].SetActive(false);
        }

        MenuType[] activeMenus = menuStack.ToArray();

        for (int i = 0; i < activeMenus.Length; i++)
        {
            menus[(int)activeMenus[i]].SetActive(true);
        }
        //switch (menuStack.Peek())
        //{
        //    case MenuType.Main:
        //        mainMenu.SetActive(true);
        //        break;
        //    case MenuType.Options:
        //        optionsMenu.SetActive(true);
        //        break;
        //    case MenuType.Controls:
        //        controlsMenu.SetActive(true);
        //        break;
        //}
    }
}
