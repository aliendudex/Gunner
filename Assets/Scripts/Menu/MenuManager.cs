using ED262C;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private SimpleArrayStack<MenuType> menuStack;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menuStack = new SimpleArrayStack<MenuType> ();
        menuStack.Push(MenuType.Main);
        Debug.Log("Current menu: " + menuStack.Peek());

        OpenMenu(MenuType.Options);
        OpenMenu(MenuType.Controls);

        GoBack();
        GoBack();
    }

    public void OpenMenu(MenuType menu)
    {
        menuStack.Push (menu);
        Debug.Log("Opened menu: " + menu);
        Debug.Log("Current menu: " + menuStack.Peek());
    }

    public void GoBack()
    {
        if(menuStack.Count > 1)
        {
            menuStack.Pop();

            Debug.Log("Went back to: " + menuStack.Peek());
        }
    }
}
