using UnityEngine;
using System.Collections;

public class LevelCompleteManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject levelCompleteMessagePrefab;
    [SerializeField] private GameObject wordsGridObject;
    [SerializeField] private GameObject closeGame;
    [SerializeField] private Transform uiParent; // Добавьте это!

    [Header("Ad System")]
    [SerializeField] private AdManager adManager;
    [SerializeField] private float messageDisplayTime = 2f;

    private GameObject currentMessageInstance;

    private void OnEnable()
    {
        GameEvents.OnLevelComplete += OnLevelComplete;
        Debug.Log("LevelCompleteManager: Подписка на события");
    }

    private void OnDisable()
    {
        GameEvents.OnLevelComplete -= OnLevelComplete;
    }

    private void OnLevelComplete()
    {
        Debug.Log("LevelCompleteManager: Уровень завершен!");
        StartCoroutine(LevelCompleteSequence());
    }

    private IEnumerator LevelCompleteSequence()
    {
        Debug.Log("LevelCompleteManager: Начало последовательности");

        // Скрываем игровое поле
        if (wordsGridObject != null)
        {
            wordsGridObject.SetActive(false);
            closeGame.SetActive(false);
            Debug.Log("LevelCompleteManager: Игровое поле скрыто");
        }
        else
        {
            Debug.LogError("LevelCompleteManager: Words Grid Object не назначен!");
        }

        // 1. Показываем сообщение о прохождении уровня
        if (levelCompleteMessagePrefab != null && uiParent != null)
        {
            Debug.Log("LevelCompleteManager: Создаем сообщение");

            // Создаем экземпляр префаба
            currentMessageInstance = Instantiate(levelCompleteMessagePrefab, uiParent);
            currentMessageInstance.SetActive(true);

            Debug.Log($"LevelCompleteManager: Сообщение создано, активен: {currentMessageInstance.activeInHierarchy}");

            // Ждем указанное время
            yield return new WaitForSeconds(messageDisplayTime);

            // Уничтожаем сообщение
            if (currentMessageInstance != null)
            {
                Destroy(currentMessageInstance);
                Debug.Log("LevelCompleteManager: Сообщение уничтожено");
            }
        }
        else
        {
            Debug.LogError("LevelCompleteManager: Префаб сообщения или UI Parent не назначены!");
            if (levelCompleteMessagePrefab == null)
                Debug.LogError("LevelCompleteMessagePrefab is NULL");
            if (uiParent == null)
                Debug.LogError("UIParent is NULL");
        }

        // 2. Показываем случайную рекламу
        if (adManager != null)
        {
            Debug.Log("LevelCompleteManager: Показываем рекламу");
            adManager.ShowRandomAd();
        }
        else
        {
            Debug.LogError("LevelCompleteManager: AdManager не назначен!");
        }

        Debug.Log("LevelCompleteManager: Последовательность завершена");
    }
}