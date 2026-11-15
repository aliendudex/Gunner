using UnityEngine;
using UnityEngine.SceneManagement;

public class Actioner : MonoBehaviour
{
    public void Level() 
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Bye_Bye()
    {
        Application.Quit();
    }
}
