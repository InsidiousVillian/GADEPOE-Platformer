using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenuManager : MonoBehaviour
{
    [Header("Scene Configuration")]
    [Tooltip("The exact string name of your gameplay scene as spelled in your project assets folder")]
    [SerializeField] private string Scene1 = "Level1";
    [SerializeField] private string Scene2 = "Level2";
    [SerializeField] private string Scene3 = "Level3";


    
    public void PlayLevel1()
    {
        Debug.Log("MainMenu: Loading gameplay scene...");
        SceneManager.LoadScene(Scene1);
    }

    public void PlayLevel2()
    {
        Debug.Log("MainMenu: Loading gameplay scene...");
        SceneManager.LoadScene(Scene2);
    }

    public void PlayLevel3()
    {
        Debug.Log("MainMenu: Loading gameplay scene...");
        SceneManager.LoadScene(Scene3);
    }

    // 2. Trigger this method via the Quit Button
    public void QuitGame()
    {
        Debug.Log("MainMenu: Exiting application...");
        
        
        Application.Quit();

        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}