using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : UiView
{
    [Header("Inventory Elements")]
    [SerializeField]
    private SoulInformation SoulItemPlaceHolder;
    [SerializeField] private RectTransform _contentParent;
    [SerializeField] private ScrollRect _scrollRect;

    [SerializeField] private Text Description;
    [SerializeField] private Text Name;
    [SerializeField] private Image Avatar;
    [SerializeField] private Button UseButton;
    [SerializeField] private Button DestroyButton;

    public GameObject _currentSelectedGameObject;
    private SoulInformation _currentSoulInformation;
    private Vector2 _paddingOfInventoryElement = new Vector2(5f, 5f);

    private void Start()
    {
        InitializeInventoryItems();
    }

    private void InitializeInventoryItems()
    {
        for (int i = 0, j = SoulController.Instance.Souls.Count; i < j; i++)
        {
            SoulInformation newSoul = Instantiate(SoulItemPlaceHolder.gameObject, _contentParent).GetComponent<SoulInformation>();
            newSoul.SetSoulItem(SoulController.Instance.Souls[i], () => SoulItem_OnClick(newSoul));
        }

        InitializeElements();
        UIGridNeighbours.SetChildNeighbours(_contentParent.transform);
        TryInitCurrentSelectable();
    }

    private void ClearSoulInformation()
    {
        Description.text = "";
        Name.text = "";
        Avatar.sprite = null;
        SetupUseButton(false);
        SetupDestroyButton(false);
        _currentSelectedGameObject = null;
        _currentSoulInformation = null;
    }

    public void SoulItem_OnClick(SoulInformation soulInformation)
    {
        _currentSoulInformation = soulInformation;
        _currentSelectedGameObject = soulInformation.gameObject;
        SetupSoulInformation(soulInformation.soulItem);
        TryRefreshScrollRect();
    }

    private void SetupSoulInformation(SoulItem soulItem)
    {
        Description.text = soulItem.Description;
        Name.text = soulItem.Name;
        Avatar.sprite = soulItem.Avatar;
        SetupUseButton(soulItem.CanBeUsed);
        SetupDestroyButton(soulItem.CanBeDestroyed);
    }

    private void SelectElement(int index)
    {

    }

    private void TryRefreshScrollRect()
    {
        if (_currentSoulInformation == null)
            return;
        if (_currentSelectedGameObject == null)
            return;
        if (_scrollRect == null)
            return;

        _scrollRect.ScrollToMakeVisible((RectTransform)_currentSelectedGameObject.transform, _paddingOfInventoryElement);
    }

    private void CantUseCurrentSoul()
    {
        PopUpInformation popUpInfo = new PopUpInformation { DisableOnConfirm = true, UseOneButton = true, Header = "CAN'T USE", Message = "THIS SOUL CANNOT BE USED IN THIS LOCALIZATION" };
        GUIController.Instance.ShowPopUpMessage(popUpInfo);
    }

    private void UseCurrentSoul(bool canUse)
    {
        //Double check
        if (!canUse)
        {
            CantUseCurrentSoul();
        }
        else
        {
            //USE SOUL
            GameEvents.OnSoulItemUsed?.Invoke(_currentSoulInformation);
            RemoveCurrentSoul();
        }
    }

    private void DestroyCurrentSoul()
    {
        RemoveCurrentSoul();
    }

    private void RemoveCurrentSoul()
    {
        if (_currentSoulInformation != null)
        {
            _currentSelectedGameObject.transform.SetParent(null);
            Destroy(_currentSelectedGameObject);

            RemoveSelectable(_currentSoulInformation);

            if (CurrentSelected == null)
                ClearSoulInformation();

            UIGridNeighbours.SetChildNeighbours(_contentParent.transform);
        }
    }

    private void SetupUseButton(bool active)
    {
        UseButton.onClick.RemoveAllListeners();
        if (active)
        {
            bool isInCorrectLocalization = GameControlller.Instance.IsCurrentLocalization(_currentSoulInformation.soulItem.UsableInLocalization);
            PopUpInformation popUpInfo = new PopUpInformation
            {
                DisableOnConfirm = isInCorrectLocalization,
                UseOneButton = false,
                Header = "USE ITEM",
                Message = "Are you sure you want to USE: " + _currentSoulInformation.soulItem.Name + " ?",
                Confirm_OnClick = () => UseCurrentSoul(isInCorrectLocalization)
            };
            UseButton.interactable = isInCorrectLocalization;
            UseButton.onClick.AddListener(() => GUIController.Instance.ShowPopUpMessage(popUpInfo));
        }
        //TMP refresh
        UseButton.gameObject.SetActive(false);
        if (active)
            UseButton.gameObject.SetActive(active);
    }

    private void SetupDestroyButton(bool active)
    {
        DestroyButton.onClick.RemoveAllListeners();
        if (active)
        {
            PopUpInformation popUpInfo = new PopUpInformation
            {
                DisableOnConfirm = true,
                UseOneButton = false,
                Header = "DESTROY ITEM",
                Message = "Are you sure you want to DESTROY: " + Name.text + " ?",
                Confirm_OnClick = () => DestroyCurrentSoul()
            };
            DestroyButton.onClick.AddListener(() => GUIController.Instance.ShowPopUpMessage(popUpInfo));
        }
        //TMP refresh
        DestroyButton.gameObject.SetActive(false);
        if (active)
            DestroyButton.gameObject.SetActive(active);
    }
}