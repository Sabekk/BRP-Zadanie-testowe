using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class DeviceDetectionController : MonoBehaviour
{
    #region VARIABLES

    [SerializeField] private InputDeviceType _currentDeviceType;
    [SerializeField] private DeviceNamesData _deviceNamesData;

    private Dictionary<string, InputDeviceType> _deviceLookup;

    #endregion

    #region PROPERTIES

    public InputDeviceType CurrentDeviceType => _currentDeviceType;

    #endregion

    #region METHODS

    public void Initialize()
    {
        BuildDeviceLookup();
        AttachEvents();
    }

    public void CleanUp()
    {
        DetachEvents();
    }

    private void BuildDeviceLookup()
    {
        _deviceLookup = new Dictionary<string, InputDeviceType>();

        foreach (var deviceNames in _deviceNamesData.DeviceNames)
        {
            var deviceType = deviceNames.deviceType;
            foreach (var deviceName in deviceNames.names)
            {
                var key = deviceName.ToLower();
                _deviceLookup[key] = deviceType;
            }
        }
    }

    private void AttachEvents()
    {
        InputSystem.onAnyButtonPress.Call(HandleAnyButtonPress);
    }

    private void DetachEvents()
    {

    }

    private void TryChangeToNewInputDeviceType(InputDeviceType newDevice)
    {
        if (CurrentDeviceType == newDevice)
            return;

        _currentDeviceType = newDevice;
        GameEvents.OnInputDeviceChanged?.Invoke(CurrentDeviceType);
    }

    private void GetDeviceTypeAndPublish(InputDevice device)
    {
        if (device is Mouse || device is Keyboard)
            TryChangeToNewInputDeviceType(InputDeviceType.Keyboard);
        else if (HasDeviceLookup(device.displayName, out InputDeviceType gamepadInputDeviceType))
            TryChangeToNewInputDeviceType(gamepadInputDeviceType);
    }

    public bool HasDeviceLookup(string inputDisplayName, out InputDeviceType deviceType)
    {
        inputDisplayName = inputDisplayName.ToLower();
        deviceType = InputDeviceType.Keyboard;

        foreach (var deviceLookup in _deviceLookup)
            if (inputDisplayName.Contains(deviceLookup.Key))
            {
                deviceType = deviceLookup.Value;
                return true;
            }

        return false;
    }

    #region HANDLERS

    private void HandleAnyButtonPress(InputControl inputControl)
    {
        InputDevice device = inputControl.device;
        GetDeviceTypeAndPublish(device);
    }

    #endregion

    #endregion
}
