using System;
using System.Collections.Generic;
using UnityEngine;

public class GUIController : MonoSingleton<GUIController>
{
    public event Action OnOpenedViewsChanged;

    [SerializeField] private GameObject DisableOnStartObject;
    [SerializeField] private RectTransform ViewsParent;
    [SerializeField] private GameObject InGameGuiObject;
    [SerializeField] private PopUpView PopUp;
    [SerializeField] private PopUpScreenBlocker ScreenBlocker;

    private List<UiView> _openedViews = new List<UiView>();

    public bool AnyViewOpen => _openedViews.Count > 0;
    public UiView TopView => _openedViews.Count > 0 ? _openedViews[0] : null;

    protected override void Awake()
    {
        base.Awake();
        DisableOnStartObject.SetActive(false);
        AttachEvents();
    }

    private void Start()
    {
        if (ScreenBlocker) ScreenBlocker.InitBlocker();
    }

    private void OnDestroy()
    {
        DetachEvents();
    }

    private void AttachEvents()
    {
        GameEvents.OnViewOpened += HandleViewOpened;
        GameEvents.OnViewClosed += HandleViewClosed;
    }

    private void DetachEvents()
    {
        GameEvents.OnViewOpened -= HandleViewOpened;
        GameEvents.OnViewClosed -= HandleViewClosed;
    }

    private void ActiveInGameGUI(bool active)
    {
        InGameGuiObject.SetActive(active);
    }

    public void ShowPopUpMessage(PopUpInformation popUpInfo)
    {
        PopUpView newPopUp = Instantiate(PopUp, ViewsParent) as PopUpView;
        newPopUp.ActivePopUpView(popUpInfo);
    }

    public void ActiveScreenBlocker(bool active, PopUpView popUpView)
    {
        if (active) ScreenBlocker.AddPopUpView(popUpView);
        else ScreenBlocker.RemovePopUpView(popUpView);
    }


    #region IN GAME GUI Clicks

    public void InGameGUIButton_OnClick(UiView viewToActive)
    {
        viewToActive.ActiveView(() => ActiveInGameGUI(true));

        ActiveInGameGUI(false);
        GameControlller.Instance.IsPaused = true;
    }

    public void ButtonQuit()
    {
        Application.Quit();
    }

    private void HandleViewOpened(UiView uiView)
    {
        _openedViews.Remove(uiView);
        _openedViews.Add(uiView);

        OnOpenedViewsChanged?.Invoke();
    }

    private void HandleViewClosed(UiView uiView)
    {
        _openedViews.Remove(uiView);

        OnOpenedViewsChanged?.Invoke();
    }

    #endregion
}