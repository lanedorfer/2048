using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;  

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public int Numbers { get; private set; }
    public static bool GameStarted { get; private set; }
    [SerializeField] private TextMeshProUGUI numbersText;
    [SerializeField] private Image gameResultImage;  
    [SerializeField] private Sprite winSprite;  
    [SerializeField] private Sprite loseSprite;  

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
        gameResultImage.enabled = false; 
        SetNumbers(0);
        GameStarted = true;

        Field.Instance.GenerateField();
    }

    public void Win()
    {
        GameStarted = false;
        gameResultImage.sprite = winSprite; 
        gameResultImage.enabled = true;  
    }

    public void Lose()
    {
        GameStarted = false;
        gameResultImage.sprite = loseSprite; 
        gameResultImage.enabled = true; 
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
