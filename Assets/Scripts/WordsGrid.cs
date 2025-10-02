using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class NewEmptyCSharpScript : MonoBehaviour
{
    public GameData currentGameData;
    public GameObject grideSquarePrefad;
    public AlphabetData alphabetData;

    public float squareOffset = 0.0f;
    public float topPosition;

    private List<GameObject> squareList = new List<GameObject>();


    void Start()
    {
        SpawnGridSquares();
        SetSquaresPosition();
    }

    private void SetSquaresPosition()
    {
        var squareRect = squareList[0].GetComponent<SpriteRenderer>().sprite.rect;
        var squareTransform = squareList[0].GetComponent<Transform>();

        var offset = new Vector2
        {
            x = (squareRect.width * squareTransform.localScale.x + squareOffset) * 0.01f,
            y = (squareRect.height * squareTransform.localScale.y + squareOffset) * 0.01f
        };

        var startPosition = GetFirstSquarePosition();
        int columnNumber = 0;
        int rowNumber = 0;

        foreach (var square in squareList)
        {
            if (rowNumber + 1 > currentGameData.selectedBoardData.Rows)
            {
                columnNumber++;
                rowNumber = 0;
            }

            var positionX = startPosition.x + offset.x * columnNumber;
            var positionY = startPosition.y - offset.y * rowNumber;

            square.GetComponent<Transform>().position = new Vector2(positionX, positionY);
            rowNumber++;
        }
    }

    private Vector2 GetFirstSquarePosition()
    {
        var startPosition = new Vector2(0f, transform.position.y);
        var squareRect = squareList[0].GetComponent<SpriteRenderer>().sprite.rect;
        var squareTransform = squareList[0].GetComponent<Transform>();
        var squareSize = new Vector2(0f, 0f);

        squareSize.x = squareRect.width * squareTransform.localScale.x;
        squareSize.y = squareRect.height * squareTransform.localScale.y;

        var midWidthPosition = (((currentGameData.selectedBoardData.Columns - 1) * squareSize.x) / 2) * 0.01f;
        var midWidthHeight = (((currentGameData.selectedBoardData.Rows - 1) * squareSize.y) / 2) * 0.01f;

        startPosition.x = (midWidthPosition != 0) ? midWidthPosition * - 1 : midWidthPosition;
        startPosition.y +=  midWidthHeight;

        return startPosition;
    }

    private void SpawnGridSquares()
    {
        if (currentGameData != null)
        {
            // Уменьшите общий масштаб для всех GridSquare
            var squareScale = GetSquareScale(new Vector3(0.5f, 0.5f, 0.1f)); // Было 1.5f

            foreach (var squares in currentGameData.selectedBoardData.Board)
            {
                foreach (var squareLetter in squares.Row)
                {
                    var normalLetterData = alphabetData.AlphabetNormal.Find(data => data.letter == squareLetter);
                    var selectedLetterData = alphabetData.AlphabetTrue.Find(data => data.letter == squareLetter);
                    var correctLetterData = alphabetData.AlphabetFalse.Find(data => data.letter == squareLetter);

                    if (normalLetterData.image == null || selectedLetterData.image == null)
                    {
                        Debug.LogError("error");
#if UNITY_EDITOR
                        if (UnityEditor.EditorApplication.isPlaying)
                        {
                            UnityEditor.EditorApplication.isPlaying = false;
                        }
#endif
                    }
                    else
                    {
                        squareList.Add(Instantiate(grideSquarePrefad));
                        var gridSquareComponent = squareList[squareList.Count - 1].GetComponent<GrideSquare>();
                        gridSquareComponent.SetSprite(normalLetterData, correctLetterData, selectedLetterData);
                        squareList[squareList.Count - 1].transform.SetParent(this.transform);
                        squareList[squareList.Count - 1].transform.position = new Vector3(0f, 0f, 0f);

                        // Применяем уменьшенный масштаб
                        squareList[squareList.Count - 1].transform.localScale = squareScale;

                        squareList[squareList.Count - 1].GetComponent<GrideSquare>().SetIndex(squareList.Count - 1);
                    }
                }
            }
        }
    }

    // В методе GetSquareScale можно уменьшить минимальный масштаб
    private Vector3 GetSquareScale(Vector3 defaultScale)
    {
        var finalScale = defaultScale;
        var adjustment = 0.01f;

        while (ShouldScaleDown(finalScale))
        {
            finalScale.x -= adjustment;
            finalScale.y -= adjustment;

            // Установите минимальный масштаб меньше (например, 0.3 вместо 0.1)
            if (finalScale.x <= 0.3f || finalScale.y <= 0.3f)
            {
                finalScale.x = 0.3f;
                finalScale.y = 0.3f;
                return finalScale;
            }
        }

        return finalScale;
    }

    private bool ShouldScaleDown(Vector3 targetScale)
    {
        var squareRect = grideSquarePrefad.GetComponent<SpriteRenderer>().sprite.rect;
        var squareSize = new Vector2(0f, 0f);
        var startPosition = new Vector2(0f, 0f);

        squareSize.x = (squareRect.width * targetScale.x) + squareOffset;
        squareSize.y = (squareRect.height * targetScale.y) + squareOffset;

        var midWidthPosition = ((currentGameData.selectedBoardData.Columns * squareSize.x) / 2) * 0.01f;
        var midHeightPosition = ((currentGameData.selectedBoardData.Rows * squareSize.y) / 2) * 0.01f;

        startPosition.x = (midWidthPosition != 0) ? midWidthPosition * -1 : midWidthPosition;
        startPosition.y = midHeightPosition;

        return startPosition.x < GetHalfScreenWidth() * -1 || startPosition.y > topPosition;
    }

    private float GetHalfScreenWidth()
    {
        float heidht = Camera.main.orthographicSize * 2;
        float width = (1.7f * heidht) * Screen.width / Screen.height;

        return width / 2;
    }
}
