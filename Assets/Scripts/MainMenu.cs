using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public static bool gameIsPause = false;

    public GameObject pausePanelShadow;
    public GameObject pauseButton;
    public GameObject instruction;
    //public GameObject player;

    public GameObject menuPanel;
    public GameObject settingPanel;
    
    [SerializeField] Slider volumeSlider = null;

    void Start()
    {
        instruction.SetActive(true);

        pausePanelShadow.SetActive(false);
        pauseButton.SetActive(true);

        menuPanel.SetActive(true);
        settingPanel.SetActive(false);
    }

    void Update()
    {
        if (instruction.activeInHierarchy == true)
        {
            Time.timeScale = 0f;
        }
        if (instruction.activeInHierarchy == false)
        {
            Time.timeScale = 1f;
        }
    }

    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1f;
    }

    public void QuitApp()
    {
        Application.Quit();
    }

    public void Pause(GameObject pausePanelShadow)
    {
        gameIsPause = true;
        if (gameIsPause == true)
        {
            pausePanelShadow.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void Resume(GameObject pausePanelShadow)
    {
        pausePanelShadow.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Replay()
    {
        Time.timeScale = 1f;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void MenuButton()
    {
        SceneManager.LoadScene("Menu");
    }

    public void HidePanel(GameObject panelHide)
    {
        panelHide.SetActive(false);
    }

    public void ShowPanel(GameObject panelShow)
    {
        panelShow.SetActive(true);
    }

    public void SetVolume(float volume)
    {
        //AudioListener.volume = volume;
        //Debug.Log(volume);
        //PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
    }
}
