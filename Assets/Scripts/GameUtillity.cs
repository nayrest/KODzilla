using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUtility : MonoBehaviour
{
    public GameObject help;

    void Start()
    {
        help.SetActive(false);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ShowHelp()
    {
        help.SetActive(true);
    }

    public void HideHelp()
    {
        help.SetActive(false);
    }
}
