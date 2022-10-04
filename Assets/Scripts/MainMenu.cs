 using UnityEngine;
 using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField]
    GameObject _controlScreen;

    [SerializeField] private GameObject _firstButton, _controlsFirstbutton, _returnFirstButton;
    

    private void Start()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
        _controlScreen.SetActive(false);
    }

    public void StartGame() {
        Cursor.visible = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Exit() {
        Application.Quit();
    }

    public void ShowControls() {
        EventSystem.current.SetSelectedGameObject(_controlsFirstbutton);
        _controlScreen.SetActive(true);
    }

    public void BackToMenu() {
        EventSystem.current.SetSelectedGameObject(_returnFirstButton);
        _controlScreen.SetActive(false);
    }
}
