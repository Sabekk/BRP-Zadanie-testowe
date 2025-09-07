using System;
using UnityEngine;
using UnityEngine.UI;

public class SoulInformation : UISelectable
{
    [SerializeField] private Image MainImage;
    [SerializeField] private Button SoulButton;

    [HideInInspector] public SoulItem soulItem;

    private Action OnSoulClick;

    public void SetSoulItem(SoulItem _soulItem, Action OnSoulClick = null)
    {
        soulItem = _soulItem;
        MainImage.sprite = soulItem.Avatar;
        this.OnSoulClick = OnSoulClick;

        SoulButton.onClick.AddListener(() =>
        {
            OnSelect();
        });
    }

    public override void OnSelect()
    {
        base.OnSelect();
        OnSoulClick?.Invoke();
    }

    public override bool CanBeSelected()
    {
        return soulItem != null && isActiveAndEnabled;
    }
}