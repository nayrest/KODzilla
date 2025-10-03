using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AdWindowController : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image adImage;
    [SerializeField] private Text titleText;
    [SerializeField] private Text descriptionText;
    [SerializeField] private Button actionButton;
    [SerializeField] private Text buttonText;
    [SerializeField] private Button closeButton;

    [Header("Settings")]
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    private string adUrl;

    void Start()
    {
        // Назначаем обработчики
        closeButton.onClick.AddListener(OnCloseButtonClicked);
        actionButton.onClick.AddListener(OnAdButtonClicked);
    }

    public void SetupAd(AdData adData)
    {
        // Заполняем UI данными из AdData
        if (adImage != null && adData.adImage != null)
            adImage.sprite = adData.adImage;

        if (titleText != null)
            titleText.text = adData.title;

        if (descriptionText != null)
            descriptionText.text = adData.description;

        if (buttonText != null)
            buttonText.text = adData.buttonText;

        adUrl = adData.url;

        // Если нет изображения, скрываем Image компонент
        if (adData.adImage == null && adImage != null)
            adImage.gameObject.SetActive(false);
    }

    public void OnCloseButtonClicked()
    {
        // Загрузка главного меню
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void OnAdButtonClicked()
    {
        // Открытие ссылки
        if (!string.IsNullOrEmpty(adUrl))
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            Application.ExternalEval($"window.open('{adUrl}','_blank')");
#else
            Application.OpenURL(adUrl);
#endif
        }
    }
}