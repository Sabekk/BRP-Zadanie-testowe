using Gameplay.Inputs;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class InputActionButton : MonoBehaviour
{
    #region VARIABLES

    [SerializeField] private InputActionReference _inputAction;
    [SerializeField] private bool _onlyWhenTopView = true;
    [SerializeField] private Image _buttonActionIcon;
    [SerializeField] private TriggerPhase _trigger = TriggerPhase.PERFORMED;

    private Button _button;
    private UiView _parentView;

    #endregion

    #region PROPERTIES

    private GUIController UIController => GUIController.Instance;
    private InputManager InputManager => InputManager.Instance;

    #endregion

    #region UNITY_METHODS

    private void Awake()
    {
        _button = GetComponent<Button>();
        _parentView = GetComponentInParent<UiView>(true);
    }

    private void OnEnable()
    {
        AttachAction();
        //RefreshIcon();
    }

    private void OnDisable()
    {
        DetachAction();
    }

    #endregion

    #region METHODS

    private void RefreshIcon()
    {
        if (_buttonActionIcon == null)
            return;

        Sprite newInputSprite = InputManager.InpuIconsController.GetActionIcon(_inputAction.action);
        if (newInputSprite != null)
        {
            _buttonActionIcon.gameObject.SetActive(true);
            _buttonActionIcon.sprite = newInputSprite;
        }
        else
        {
            _buttonActionIcon.gameObject.SetActive(false);
        }
    }

    private void AttachAction()
    {
        switch (_trigger)
        {
            case TriggerPhase.STARTED:
                _inputAction.action.started += HandleInputAction;
                break;
            case TriggerPhase.PERFORMED:
                _inputAction.action.performed += HandleInputAction;
                break;
            default:
                break;
        }

        GameEvents.OnInputDeviceChanged += HandleDeviceChanged;
    }

    private void DetachAction()
    {
        switch (_trigger)
        {
            case TriggerPhase.STARTED:
                _inputAction.action.started -= HandleInputAction;
                break;
            case TriggerPhase.PERFORMED:
                _inputAction.action.performed -= HandleInputAction;
                break;
            default:
                break;
        }

        GameEvents.OnInputDeviceChanged -= HandleDeviceChanged;
    }


    #region HANDLERS

    private void HandleInputAction(InputAction.CallbackContext context)
    {
        if (!isActiveAndEnabled) 
            return;
        if (_button == null) 
            return;
        if (!_button.interactable) 
            return;
        if (!gameObject.activeInHierarchy)
            return;

        if (_onlyWhenTopView && UIController != null)
        {
            if (_parentView != null && UIController.TopView != _parentView)
                return;
        }

        _button.onClick?.Invoke();
    }

    private void HandleDeviceChanged(InputDeviceType deviceType)
    {
        RefreshIcon();
    }

    #endregion

    #endregion

    #region ENUMS

    public enum TriggerPhase
    {
        STARTED,
        PERFORMED
    }

    #endregion
}
