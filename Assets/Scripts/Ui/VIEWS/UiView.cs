using Gameplay.Inputs;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiView : MonoBehaviour
{
    [Header("UI VIEW elements")]
    [SerializeField]
    private bool UnpauseOnClose = false;

    [SerializeField] private bool CloseOnNewView = true;
    [SerializeField] private Button BackButon;
    [SerializeField] private List<UISelectable> _selectables;
    [SerializeField] private bool _autoCollectAllSelectables = true;

    private UiView _parentView;

    public bool IsTopOnView => UIController == null ? false : UIController.TopView == this;
    protected InputManager InputManager => InputManager.Instance;
    protected InputBindsController Binds => InputManager == null ? null : InputManager.InputBindsController;
    protected DeviceDetectionController DeviceDetector => InputManager == null ? null : InputManager.DeviceDetectionController;
    protected GUIController UIController => GUIController.Instance;
    protected UISelectable CurrentSelected { get; set; }


    public virtual void Awake()
    {
        if (_autoCollectAllSelectables)
        {
            _selectables = new List<UISelectable>();
            _selectables.AddRange(GetComponentsInChildren<UISelectable>());
        }

        _selectables.ForEach(x => x.SetUiView(this, SetCurrentSelected));

        BackButon.onClick.AddListener(() => DisableView_OnClick(this));
    }

    public virtual void OnEnable()
    {
        TryInitCurrentSelectable();
        GameEvents.OnViewOpened?.Invoke(this);
        AttachEvents();
    }

    public virtual void OnDisable()
    {
        GameEvents.OnViewClosed?.Invoke(this);
        DetachEvents();
    }

    protected virtual void AttachEvents()
    {
        GameEvents.OnViewOpened += HandleViewOpened;
        GameEvents.OnViewClosed += HandleViewClosed;

        RefreshEventsOfTopView();
    }

    protected virtual void DetachEvents()
    {
        GameEvents.OnViewOpened -= HandleViewOpened;
        GameEvents.OnViewClosed -= HandleViewClosed;

        RefreshEventsOfTopView();
    }

    protected virtual void AttachEventsOfTopView()
    {
        if (Binds != null)
        {
            Binds.UiInputs.OnBackInput += HandleBackInput;
            Binds.UiInputs.OnUINavigation += HandleUINavigation;
        }
    }

    protected virtual void DetachEventsOfTopView()
    {
        if (Binds != null)
        {
            Binds.UiInputs.OnBackInput -= HandleBackInput;
            Binds.UiInputs.OnUINavigation -= HandleUINavigation;
        }
    }

    private void RefreshEventsOfTopView()
    {
        if (IsTopOnView && isActiveAndEnabled)
        {
            AttachEventsOfTopView();
        }
        else
            DetachEventsOfTopView();
    }

    private void TryInitCurrentSelectable()
    {
        if (CurrentSelected != null)
            return;
        if (_selectables == null)
            return;
        if (_selectables.Count == 0)
            return;

        CurrentSelected = _selectables[0];
        CurrentSelected.OnSelect();
    }

    private void SetCurrentSelected(UISelectable newSelected)
    {
        if (CurrentSelected != null)
            CurrentSelected.OnDeselect();
        CurrentSelected = newSelected;
    }

    public void ActiveView_OnClick(UiView viewToActive)
    {
        viewToActive.SetParentView(this);
        viewToActive.ActiveView();
        this.ActiveView(!CloseOnNewView);
    }

    private void DisableView_OnClick(UiView viewToDisable)
    {
        viewToDisable.DisableView();
    }

    public void DestroyView_OnClick(UiView viewToDisable)
    {
        viewToDisable.DestroyView();
    }

    public void SetParentView(UiView parentView)
    {
        _parentView = parentView;
    }

    public void ActiveView(bool active)
    {
        this.gameObject.SetActive(active);
    }

    public void ActiveView(Action onBackButtonAction = null)
    {
        if (onBackButtonAction != null) BackButon.onClick.AddListener(() => onBackButtonAction());

        if (!gameObject.activeSelf) this.ActiveView(true);
    }

    public void DisableView()
    {
        if (_parentView != null)
        {
            _parentView.ActiveView();
        }

        if (UnpauseOnClose) GameControlller.Instance.IsPaused = false;

        this.ActiveView(false);
    }

    public void DestroyView()
    {
        if (_parentView != null)
        {
            _parentView.ActiveView();
        }

        Destroy(this.gameObject);
    }

    public void DisableBackButton()
    {
        BackButon.gameObject.SetActive(false);
    }

    public Button GetBackButton()
    {
        return BackButon;
    }


    private void HandleViewOpened(UiView uiView)
    {
        RefreshEventsOfTopView();
    }

    private void HandleViewClosed(UiView uiView)
    {
        RefreshEventsOfTopView();
    }

    protected virtual void HandleBackInput()
    {
        BackButon.onClick?.Invoke();
    }

    protected virtual void HandleUINavigation(Vector2 direction)
    {
        if (CurrentSelected == null)
            TryInitCurrentSelectable();

        if (CurrentSelected == null)
            return;

        UISelectable newSelectable = CurrentSelected.GetNeighbour(direction);

        if (newSelectable)
            newSelectable.OnSelect();
    }
}