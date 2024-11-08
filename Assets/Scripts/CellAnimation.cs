using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class CellAnimation : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI numbers;

    private float moveTime = .1f;
    private float appearTime = .1f;

    private Sequence sequence;
    public void Move(Cell from, Cell to, bool isMerging)
    {
        from.CancelAnimation();
        to.SetAnimation(this);

        image.color = ColorManager.Instance.CellColors[from.Value];
        numbers.text = from.Numbers.ToString();
        numbers.color = from.Value <= 2
            ? ColorManager.Instance.NumbersDarkColor
            : ColorManager.Instance.NumbersLightColor;

        transform.position = from.transform.position;
        sequence = DOTween.Sequence();

        sequence.Append(transform.DOMove(to.transform.position, moveTime).SetEase(Ease.InOutQuad));

        if (isMerging)
        {
            sequence.AppendCallback(() =>
            {
                image.color = ColorManager.Instance.CellColors[to.Value];
                numbers.text = to.Numbers.ToString();
                numbers.color = to.Value <= 2
                    ? ColorManager.Instance.NumbersDarkColor
                    : ColorManager.Instance.NumbersLightColor;
            });
                
            sequence.Append(transform.DOScale(1.2f, appearTime));
            sequence.Append(transform.DOScale(1f, appearTime));
        }

        sequence.AppendCallback(() =>
        {
            to.UpdateCell();
            Destroy();
        });
    }

    public void Appear(Cell cell)
    {
        cell.CancelAnimation();
        cell.SetAnimation(this);

        image.color = ColorManager.Instance.CellColors[cell.Value];
        numbers.text = cell.Numbers.ToString();
        numbers.color = cell.Value <= 2
            ? ColorManager.Instance.NumbersDarkColor
            : ColorManager.Instance.NumbersLightColor;

        transform.position = cell.transform.position;
        transform.localScale = Vector2.zero;

        sequence = DOTween.Sequence();

        sequence.Append(transform.DOScale(1.2f, appearTime * 2));
        sequence.Append(transform.DOScale(1f, appearTime * 2));
        sequence.AppendCallback(() =>
        {
            cell.UpdateCell();
            Destroy();
        });
    }

    public void Destroy()
    {
        sequence.Kill();
        Destroy(gameObject);
    }
}
