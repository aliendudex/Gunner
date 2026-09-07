using ED262C;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject controlsMenu;

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
            menuStack.Pop();
            UpdateMenu();

            Debug.Log("Went back to: " + menuStack.Peek());
        }
    }

    public void OpenOptions()
    {
        OpenMenu(MenuType.Options);
    }

    public void OpenControls()
    {
        OpenMenu(MenuType.Controls);
    }

    public void UpdateMenu()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        controlsMenu.SetActive(false);

        switch (menuStack.Peek())
        {
            case MenuType.Main:
                mainMenu.SetActive(true);
                break;
            case MenuType.Options:
                optionsMenu.SetActive(true);
                break;
            case MenuType.Controls:
                controlsMenu.SetActive(true);
                break;
        }
    }
}
