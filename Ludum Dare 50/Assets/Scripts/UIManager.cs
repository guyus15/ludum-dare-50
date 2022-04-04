using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public int width;
    public int height;
    [SerializeField] private GameObject _optionsMenu;

    private void Start()
    {
        _optionsMenu.SetActive(false);


    }

    public void SetWidth(int newWidth)
    {
        width = newWidth;
    }

    public void SetHeight(int newHeight)
    {
        height = newHeight;
    }

    public void SetRes()
    {
        Screen.SetResolution(width, height, false);
        Debug.Log("Setting Res");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenMenu(GameObject menu)
    {
        menu.SetActive(true);
    }

public void ExitMenu(GameObject menu)
    {
        menu.SetActive(false);
    }
}