using System;
using UnityEngine;

[CreateAssetMenu(fileName = "InputIconsData", menuName = "Data/InputIconsData", order = 0)]
public class InputIconsData : ScriptableObject
{
    #region VARIABLES

    [SerializeField] private DeviceInputIcons[] _deviceInputIcons;

    #endregion

    #region PROPERTIES

    public DeviceInputIcons[] Icons => _deviceInputIcons;

    #endregion

    #region STRUCTS

    [Serializable]
    public struct DeviceInputIcons
    {
        public InputDeviceType deviceType;
        public string deviceName;
        public InputIcon[] icons;
    }

    [Serializable]
    public struct InputIcon
    {
        public string bindName;
        public Sprite icon;
    }

    #endregion
}
