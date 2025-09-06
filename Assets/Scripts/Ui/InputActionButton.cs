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

    private InputAction _actionResolved;

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

    private void Start()
    {
        RefreshIcon();
    }

    private void OnEnable()
    {
        ResolveRuntimeAction();
        AttachEvents();
        RefreshIcon();
    }

    private void OnDisable()
    {
        DetachEvents();
    }

    #endregion

    #region METHODS

    private void RefreshIcon()
    {
        if (_buttonActionIcon == null)
            return;

        if (InputManager == null)
        {
            _buttonActionIcon.gameObject.SetActive(false);
            return;
        }
        else
            _buttonActionIcon.gameObject.SetActive(true);

        Sprite newInputSprite = InputManager.InpuIconsController.GetActionIcon(_actionResolved);
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

    private void ResolveRuntimeAction()
    {
        _actionResolved = null;
        if (_inputAction != null && _inputAction.action != null)
        {
            var binds = InputMapController.Input;
            _actionResolved = binds?.asset?.FindAction(_inputAction.action.id) ?? _inputAction.action;
        }
    }

    private void AttachEvents()
    {
        if (_actionResolved != null)
            switch (_trigger)
            {
                case TriggerPhase.STARTED:
                    _actionResolved.started += HandleInputAction;
                    break;
                case TriggerPhase.PERFORMED:
                    _actionResolved.performed += HandleInputAction;
                    break;
                default:
                    break;
            }

        GameEvents.OnInputDeviceChanged += HandleDeviceChanged;
    }

    private void DetachEvents()
    {
        if (_actionResolved != null)
            switch (_trigger)
            {
                case TriggerPhase.STARTED:
                    _actionResolved.started -= HandleInputAction;
                    break;
                case TriggerPhase.PERFORMED:
                    _actionResolved.performed -= HandleInputAction;
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
