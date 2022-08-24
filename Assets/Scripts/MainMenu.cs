using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Runtime.CompilerServices;

public class MainMenu : MonoBehaviour
{   
    public GameObject pausePanelShadow;
    public GameObject pauseButton;
    
    public GameObject instruction;

    public GameObject menuPanel;
    public GameObject settingPanel;
    public GameObject frameRateButton;

    public AudioMixer audioMixer;

    void Start()
    {
        instruction.SetActive(true);

        pausePanelShadow.SetActive(false);
        pauseButton.SetActive(true);

        menuPanel.SetActive(true);
        settingPanel.SetActive(false);
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
        pausePanelShadow.SetActive(true);
        Time.timeScale = 0f;
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
        audioMixer.SetFloat("volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFrameRate(int frameIndex)
    {
        if (frameIndex == 0)
        {
            Application.targetFrameRate = 30;
        } 
        else if (frameIndex == 1)
        {
            Application.targetFrameRate = 60;
        }
        else
        {
            Application.targetFrameRate = 120;
        }

        DontDestroyOnLoad(frameRateButton);
    }
}
