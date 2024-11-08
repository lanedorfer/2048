using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public int X { get; private set; }
    public int Y { get; private set; }
    
    public int Value { get; private set; }
    public int Numbers => isEmpty ? 0 : (int)Mathf.Pow(2, Value);
    public bool isEmpty => Value == 0;
    public bool HasMerged { get; private set; }

    public const int MaxValue = 11;

    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI numbers;

    private CellAnimation currentAnimation;

    public void SetValue(int x, int y, int value, bool updateUI = true)
    {
        X = x;
        Y = y;
        Value = value;

        if (updateUI)
        {
           UpdateCell(); 
        }
    }

    public void IncreaseValue()
    {
        if (Time.timeScale == 0) return; 

        if (Value < MaxValue)
        {
            Value++;
            HasMerged = true;

            GameController.Instance.AddNumbers(Numbers);
        }
    }

    public void ResetFlags()
    {
        HasMerged = false;
    }

    public void MergeWithCell(Cell otherCell)
    {
        CellAnimationController.Instance.SmoothTransition(this, otherCell, true);
        otherCell.IncreaseValue();
        SetValue(X, Y, 0);
    }

    public void MoveToCell(Cell target)
    {
        CellAnimationController.Instance.SmoothTransition(this, target, false);
        target.SetValue(target.X, target.Y, Value, false);
        SetValue(X, Y, 0);
    }

    public void UpdateCell()
    {

        if (numbers != null)
        {
            numbers.text = isEmpty ? string.Empty : Numbers.ToString();
            numbers.color = Value <= 2 ? ColorManager.Instance.NumbersDarkColor : ColorManager.Instance.NumbersLightColor;
        }

        if (image != null)
        {
            if (Value >= 0 && Value < ColorManager.Instance.CellColors.Length)
            {
                Color cellColor = ColorManager.Instance.CellColors[Value];
                cellColor.a = 1f; 
                image.color = cellColor;
            }
        }
    }

    public void SetAnimation(CellAnimation animation)
    {
        currentAnimation = animation;
    }

    public void CancelAnimation()
    {
        if (currentAnimation != null)
        {
            currentAnimation.Destroy();
        }
    }
}
