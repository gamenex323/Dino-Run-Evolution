using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
// Sajid Saingal using static MaxSdkCallbacks;

public class GameExitHandler : MonoBehaviour
{
    [SerializeField] GameObject exitPanel;
    [SerializeField] GameObject mainCanvas;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (exitPanel)
            {
                exitPanel.SetActive(true);
                mainCanvas.SetActive(false);
                Time.timeScale = 0;
            }
        }
    }

    public void onUserClickYesNo(int choice)
    {
        if (choice == 1)
        {
            Application.Quit();
        }
        else
        {
            Time.timeScale = 1;
            exitPanel.SetActive(false);
            mainCanvas.SetActive(true);
        }
    }
}


