using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelCompleteManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] public GameObject levelCompleteMessagePrefab;
    [SerializeField] public GameObject adWindowPrefab;

    [Header("Settings")]
    [SerializeField] private Transform uiParent; // Canvas или другой родитель
    [SerializeField] public GameObject wordsGridObject;
    [SerializeField] public GameObject returnToMain;
    [SerializeField] private string mainMenuSceneName = "MainMenu";
    [SerializeField] private float messageDisplayTime = 2f;
    [SerializeField] private string adLinkURL = "https://www.gazprombank.ru/personal/credit-cards/7950641/";

    private GameObject currentMessage;
    private GameObject currentAdWindow;

    private void OnEnable()
    {
        GameEvents.OnLevelComplete += OnLevelComplete;
    }

    private void OnDisable()
    {
        GameEvents.OnLevelComplete -= OnLevelComplete;
    }

    private void OnLevelComplete()
    {
        StartCoroutine(LevelCompleteSequence());
    }

    private IEnumerator LevelCompleteSequence()
    {
        // Скрываем игровое поле
        if (wordsGridObject != null)
        {
            wordsGridObject.SetActive(false);
            returnToMain.SetActive(false);
        }

        // 1. Создаем сообщение о прохождении уровня
        if (levelCompleteMessagePrefab != null && uiParent != null)
        {
            currentMessage = Instantiate(levelCompleteMessagePrefab, uiParent);

            // Ждем указанное время
            yield return new WaitForSeconds(messageDisplayTime);

            // Уничтожаем сообщение
            Destroy(currentMessage);
        }

        // 2. Создаем рекламное окно
        if (adWindowPrefab != null && uiParent != null)
        {
            currentAdWindow = Instantiate(adWindowPrefab, uiParent);

            // Настраиваем кнопки рекламного окна
            SetupAdWindow(currentAdWindow);
        }
    }

    private void SetupAdWindow(GameObject adWindow)
    {
        // Находим кнопки в префабе
        var closeButton = adWindow.transform.Find("AdClose")?.GetComponent<UnityEngine.UI.Button>();
        var adButton = adWindow.transform.Find("AdButton")?.GetComponent<UnityEngine.UI.Button>();

        // Назначаем обработчики
        if (closeButton != null)
            closeButton.onClick.AddListener(OnCloseButtonClicked);

        if (adButton != null)
            adButton.onClick.AddListener(OnAdButtonClicked);
    }

    public void OnCloseButtonClicked()
    {
        StartCoroutine(LoadMainMenuAsync());
    }

    private IEnumerator LoadMainMenuAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(mainMenuSceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void OnAdButtonClicked()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        Application.ExternalEval($"window.open('{adLinkURL}','_blank')");
#else
        Application.OpenURL(adLinkURL);
#endif
    }
}