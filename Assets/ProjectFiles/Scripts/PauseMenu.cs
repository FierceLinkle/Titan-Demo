using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class PauseMenu : MonoBehaviour
{
    public static bool IsPaused = false;

    public GameObject PauseMenuUI;

    public FirstPersonController fps;

    // Start is called before the first frame update
    void Start()
    {
        fps.m_MouseLook.SetCursorLock(true);
        fps.enabled = true;
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!IsPaused)
            {
                Pause();
            }   
        }
    }

    void Pause()
    {
        fps.m_MouseLook.SetCursorLock(false);
        fps.enabled = false;
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void ResumeGame()
    {
        fps.m_MouseLook.SetCursorLock(true);
        fps.enabled = true;
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }

    public void ReturnToMainMenu()
    {   
        PauseMenuUI.SetActive(false);
        IsPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
