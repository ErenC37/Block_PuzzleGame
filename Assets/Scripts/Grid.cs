using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Rotater[] aobject;
    public ShapeStorage shapeStorage;
    public int columns = 0;
    public int rows = 0;
    public float squaresGap = 0.1f;
    public GameObject gridSquare;
    public Vector2 startPosition = new Vector2(0.0f, 0.0f);
    public float squareScale = 0.5f;
    public float everySquareOffset = 0.0f;

    int shapeIndexToChange;

    private Vector2 _offset = new Vector2(0.0f, 0.0f);
    private List<GameObject> _gridSquares = new List<GameObject>();

    private LineIndicator _lineIndicator;
    

    float bonus = 1f;

    IEnumerator co;

    CountDown countDown;

    private void OnEnable()
    {
        GameEvents.CheckIfShapeCanBePlaced += CheckIfShapeCanBePlaced;
    }

    private void OnDisable()
    {
        GameEvents.CheckIfShapeCanBePlaced -= CheckIfShapeCanBePlaced;
    }

    void Awake()
    {
        shapeStorage = FindObjectOfType<ShapeStorage>();
        countDown = FindObjectOfType<CountDown>();
    }


    void Start()
    {
        _lineIndicator = GetComponent<LineIndicator>();
        CreateGrid();
    }

    private void CreateGrid()
    {
        SpawnGridSquares();
        SetGridSquaresPositions();
    }

    private void SpawnGridSquares()
    {
        //0, 1, 2, 3, 4,
        //5, 6, 7, 8, 9

        int square_index = 0;

        for (var row = 0; row < rows; ++row)
        {
            for (var column = 0; column < columns; ++column)
            {
                _gridSquares.Add(Instantiate(gridSquare) as GameObject);

                _gridSquares[_gridSquares.Count - 1].GetComponent<GridSquare>().SquareIndex = square_index;
                _gridSquares[_gridSquares.Count - 1].transform.SetParent(this.transform);
                _gridSquares[_gridSquares.Count - 1].transform.localScale =
                    new Vector3(squareScale, squareScale, squareScale);
                _gridSquares[_gridSquares.Count - 1].GetComponent<GridSquare>()
                    .SetImage(_lineIndicator.GetGridSquareIndex(square_index) % 2 == 0);
                square_index++;
            }
        }
    }

    private void SetGridSquaresPositions()
    {
        int column_number = 0;
        int row_number = 0;
        Vector2 square_gap_number = new Vector2(0.0f, 0.0f);
        bool row_moved = false;

        var square_rect = _gridSquares[0].GetComponent<RectTransform>();

        _offset.x = square_rect.rect.width * square_rect.transform.localScale.x + everySquareOffset;
        _offset.y = square_rect.rect.height * square_rect.transform.localScale.y + everySquareOffset;

        foreach (GameObject square in _gridSquares)
        {
            if (column_number + 1 > columns)
            {
                square_gap_number.x = 0;
                //diger sutuna git
                column_number = 0;
                row_number++;
                row_moved = false;
            }


            var pos_x_offset = _offset.x * column_number + (square_gap_number.x * squaresGap);
            var pos_y_offset = _offset.y * row_number + (square_gap_number.y * squaresGap);

            if (column_number > 0 && column_number % 3 == 0)
            {
                square_gap_number.x++;
                pos_x_offset += squaresGap;
            }

            if (row_number > 0 && row_number % 3 == 0 && row_moved == false)
            {
                row_moved = true;
                square_gap_number.y++;
                pos_y_offset += squaresGap;
            }

            square.GetComponent<RectTransform>().anchoredPosition =
                new Vector2(startPosition.x + pos_x_offset, startPosition.y - pos_y_offset);
            square.GetComponent<RectTransform>().localPosition = new Vector3(startPosition.x + pos_x_offset,
                startPosition.y - pos_y_offset, 0.0f);

            column_number++;
        }
    }

    public void callis(int call)
    {
        shapeIndexToChange = call;
    }


    private void CheckIfShapeCanBePlaced()
    {
        var squareIndexes = new List<int>();

        foreach (var square in _gridSquares)
        {
            var gridSquare = square.GetComponent<GridSquare>();

            if (gridSquare.Selected && !gridSquare.SquareOccupied)
            {
                squareIndexes.Add(gridSquare.SquareIndex);
                gridSquare.Selected = false;
                // gridSquare.ActiveSquare();
            }
        }

        var currentSelectedShape = shapeStorage.GetCurrentSelectedShape();
        if (currentSelectedShape == null) return; //there is no selected shape.

        if (currentSelectedShape.TotalSquareNumber == squareIndexes.Count)
        {
            foreach (var SquareIndex in squareIndexes)
            {
                _gridSquares[SquareIndex].GetComponent<GridSquare>().PlaceShapeOnBoard();
            }


            foreach (var shape in shapeStorage.shapeList)
            {
                if (shape.IsOnStartPosition() && shape.IsAnyOfShapeSquareActive())
                {
                    //shapeleft++;
                    shapeStorage.ChangeOneShape(shapeIndexToChange);
                }
            }


            GameEvents.SetShapeInactive();


            ChechkIfAnyLineIsCompleted();
        }

        else
        {
            GameEvents.MoveShapeToStartPosition();
        }
    }

    void ChechkIfAnyLineIsCompleted()
    {
        List<int[]> lines = new List<int[]>();

        //squares

        for (var square = 0; square < 9; square++)
        {
            List<int> data = new List<int>(9);
            for (var index = 0; index < 9; index++)
            {
                data.Add(_lineIndicator.square_data[square, index]);
            }

            lines.Add(data.ToArray());
        }

        var completedLines = ChechkIfSqauresAreCompleted(lines);
        if (completedLines >= 2)
        {
            //TODO: Play bonus animation.
            bonus = completedLines*0.8f;

        }

        var totalScores = 10 * completedLines*bonus;
        bonus = 1f;
        GameEvents.AddScores((int)totalScores);
        ChechkIfPlayerLost();
    }

    private int ChechkIfSqauresAreCompleted(List<int[]> data)
    {
        List<int[]> completedLines = new List<int[]>();

        var linesCompleted = 0;

        foreach (var line in data)
        {
            var lineCompleted = true;
            foreach (var squareIndex in line)
            {
                var comp = _gridSquares[squareIndex].GetComponent<GridSquare>();
                if (comp.SquareOccupied == false)
                {
                    lineCompleted = false;
                }
            }

            if (lineCompleted)
            {
                completedLines.Add(line);
            }
        }

        foreach (var line in completedLines)
        {
            var completed = false;

            foreach (var squareIndex in line)
            {
                var comp = _gridSquares[squareIndex].GetComponent<GridSquare>();
                comp.Deactivate();
                completed = true;
            }

            foreach (var squareIndex in line)
            {
                var comp = _gridSquares[squareIndex].GetComponent<GridSquare>();
                comp.ClearOccupied();
            }

            if (completed)
            {
                linesCompleted++;
            }
        }

        return linesCompleted;
    }

    private void Rotater()
    {
        for (var index = 0; index < shapeStorage.shapeList.Count; index++)
        {
            if (shapeStorage.shapeList[index].CurrentShapeData)
            {
                Rotate90(shapeStorage.shapeList[index].CurrentShapeData);
            }
        }
    }

    void Rotate90(ShapeData arr)
    {
        int n = arr.columns;


        for (int i = 0; i < n; ++i)
        {
            for (var j = 0; j < i; ++j)
            {
                (arr.board[i].column[j], arr.board[j].column[i]) = (arr.board[j].column[i], arr.board[i].column[j]);
            }
        }


        for (int i = 0; i < n; ++i)
        {
            for (int j = 0; j < n / 2; ++j)
            {
                (arr.board[i].column[j], arr.board[i].column[n - j - 1]) =
                    (arr.board[i].column[n - j - 1], arr.board[i].column[j]);
            }

            ;
        }
    }


    private int counter = 0;

    private void ChechkIfPlayerLost()
    {
        var validShapes = 0;

        for (var index = 0; index < shapeStorage.shapeList.Count; index++)
        {
            var isShapeActive = shapeStorage.shapeList[index].IsAnyOfShapeSquareActive();
            if (ChechkIfShapeCanBePlacedOnGrid(shapeStorage.shapeList[index]) && isShapeActive)
            {
                shapeStorage.shapeList[index]?.ActivateShape();
                validShapes++;
                counter = 0;
            }
        }

        if (validShapes == 0)
        {
            //GAME OVER

            if (counter < 4)
            {
                counter++;
                Rotater();
                Debug.Log("dönüş{counter}");
                ChechkIfPlayerLost();
            }
            else
            {
                GameEvents.GameOver(false);
                countDown.coUpdate();
            }

           
        }
    }

    private bool ChechkIfShapeCanBePlacedOnGrid(Shape currentShape)
    {
        var currentShapeData = currentShape.CurrentShapeData;
        var shapeColumns = currentShapeData.columns;
        var shapeRows = currentShapeData.rows;
        //all indexes of filled up squares

        List<int> originalShapeFilledUpSquares = new List<int>();
        var squareIndex = 0;

        for (var rowIndex = 0; rowIndex < shapeRows; rowIndex++)
        {
            for (var columnIndex = 0; columnIndex < shapeColumns; columnIndex++)
            {
                if (currentShapeData.board[rowIndex].column[columnIndex])
                {
                    originalShapeFilledUpSquares.Add(squareIndex);
                }

                squareIndex++;
            }
        }

        if (currentShape.TotalSquareNumber != originalShapeFilledUpSquares.Count)
            Debug.LogError("Yer Kalmadi YEEEEER");

        var squareList = GetAllSquaresCombination(shapeColumns, shapeRows);

        bool canBePlaced = false;

        foreach (var number in squareList)
        {
            bool shapeCanBePlacedOnTheBoard = true;
            foreach (var squareIndexToCheck in originalShapeFilledUpSquares)
            {
                var comp = _gridSquares[number[squareIndexToCheck]].GetComponent<GridSquare>();
                if (comp.SquareOccupied)
                {
                    shapeCanBePlacedOnTheBoard = false;
                }
            }

            if (shapeCanBePlacedOnTheBoard)
            {
                canBePlaced = true;
            }
        }

        return canBePlaced;
    }


    private void control(Shape currentShape)
    {
        Debug.Log(currentShape.CurrentShapeData);
    }


    private List<int[]> GetAllSquaresCombination(int columns, int rows)
    {
        var squareList = new List<int[]>();
        var lastColumnIndex = 0;
        var lastRowIndex = 0;

        int safeIndex = 0;

        while (lastRowIndex + (rows - 1) < 9)
        {
            var rowData = new List<int>();

            for (var row = lastRowIndex; row < lastRowIndex + rows; row++)
            {
                for (var column = lastColumnIndex; column < lastColumnIndex + columns; column++)
                {
                    rowData.Add(_lineIndicator.line_data[row, column]);
                }
            }

            squareList.Add(rowData.ToArray());

            lastColumnIndex++;
            if (lastColumnIndex + (columns - 1) >= 9)
            {
                lastRowIndex++;
                lastColumnIndex = 0;
            }

            safeIndex++;
            if (safeIndex > 100)
            {
                break;
            }
        }

        return squareList;
    }
}