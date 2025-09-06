using System;
using UnityEngine;
using UnityEngine.UI;

public class SoulInformation : UISelectable
{
    [SerializeField] private Image MainImage;
    [SerializeField] private Button SoulButton;
    [SerializeField] private GameObject selection;

    [HideInInspector] public SoulItem soulItem;

    public void SetSoulItem(SoulItem _soulItem, Action OnSoulClick = null)
    {
        soulItem = _soulItem;
        MainImage.sprite = soulItem.Avatar;
        if (OnSoulClick != null) SoulButton.onClick.AddListener(() => OnSoulClick());
    }

    public override void ToggleTransition(bool state)
    {
        base.ToggleTransition(state);
        selection.SetActive(state);
    }
}