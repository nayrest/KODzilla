using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GrideSquare : MonoBehaviour
{
    public int SquareIndex { get; set; }

    private AlphabetData.LetterData normalLetterData;
    private AlphabetData.LetterData selectedLetterData;
    private AlphabetData.LetterData correctLetterData;

    private SpriteRenderer displayImage;

    void Start()
    {
        displayImage = GetComponent<SpriteRenderer>();
    }

    public void SetSprite(AlphabetData.LetterData normalLetterData, AlphabetData.LetterData selectedLetterData, AlphabetData.LetterData correctLetterData)
    {
        normalLetterData = normalLetterData;
        selectedLetterData = selectedLetterData;
        correctLetterData = correctLetterData;

        GetComponent<SpriteRenderer>().sprite = normalLetterData.image;
    }
}
