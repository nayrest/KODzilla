using UnityEngine;

[CreateAssetMenu(fileName = "AdData", menuName = "Game/Ad Data")]
public class AdData : ScriptableObject
{
    [Header("Рекламные данные")]
    public Sprite adImage;
    public string title;
    public string description;
    public string buttonText = "Узнать больше";
    public string url;

    [Header("Настройки показа")]
    public float weight = 1f;

    [Header("Настройки шрифтов")]
    public Font titleFont;
    public Font descriptionFont;
    public Font buttonFont;

    [Header("Настройки цвета текста")]
    public Color titleColor = Color.black;
    public Color descriptionColor = Color.gray;
    public Color buttonTextColor = Color.white;

    [Header("Настройки размера текста")]
    public int titleFontSize = 32;
    public int descriptionFontSize = 18;
    public int buttonFontSize = 20;

    [Header("Стиль текста")]
    public FontStyle titleStyle = FontStyle.Bold;
    public FontStyle descriptionStyle = FontStyle.Normal;
    public FontStyle buttonStyle = FontStyle.Normal;

    [Header("Выравнивание текста")]
    public TextAnchor titleAlignment = TextAnchor.UpperCenter;
    public TextAnchor descriptionAlignment = TextAnchor.UpperCenter;
    public TextAnchor buttonAlignment = TextAnchor.MiddleCenter;
}