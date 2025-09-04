using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DeviceNamesData", menuName = "Data/DeviceNamesData", order = 0)]
public class DeviceNamesData : ScriptableObject
{
    #region VARIABLES

    [SerializeField] private PossibleDeviceNames[] _deviceNames;

    #endregion

    #region PROPERTIES

    public PossibleDeviceNames[] DeviceNames => _deviceNames;

    #endregion

    #region STRUCTS

    [Serializable]
    public struct PossibleDeviceNames
    {
        public InputDeviceType deviceType;
        public string[] names;
    }

    #endregion

}
