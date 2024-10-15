using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public int Numbers { get; private set; }
    public static bool GameStarted { get; private set; }
    [SerializeField] private TextMeshProUGUI gameResult;
    [SerializeField] private TextMeshProUGUI numbersText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        gameResult.text = "";
        SetNumbers(0);
        GameStarted = true;

        Field.Instance.GenerateField();
    }

    public void Win()
    {
        GameStarted = false;
        gameResult.text = "You Win";
    }

    public void Lose()
    {
        GameStarted = false;
        gameResult.text = "You Lose";
    }

    public void AddNumbers(int numbers)
    {
        SetNumbers(Numbers + numbers);
    }

    public void SetNumbers(int numbers)
    {
        Numbers = numbers;
        numbersText.text = Numbers.ToString();
    }
}
