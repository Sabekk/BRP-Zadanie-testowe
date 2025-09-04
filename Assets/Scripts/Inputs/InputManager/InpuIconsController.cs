using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static InputIconsData;

namespace Gameplay.Inputs
{
    //TODO Remove MonoBehaviour and get InputIconsData from database
    public class InpuIconsController : MonoBehaviour
    {
        #region VARIABLES

        [SerializeField] private InputIconsData _inputIconsData;

        private Dictionary<InputDeviceType, InputIconsSetting> _iconSettings;

        #endregion

        #region PROPERTIES

        private InputManager Manager => InputManager.Instance;
        private InputDeviceType CurrentDevice => Manager.DeviceDetectionController.CurrentDeviceType;
        private InputIconsSetting CurrentSetting => _iconSettings[CurrentDevice];

        #endregion

        #region METHODS

        public void Initialize()
        {
            InitializeSettings();
        }

        public void LateInitialzie()
        {
            AttachEvents();
        }

        public void CleanUp()
        {
            DetachEvents();
        }

        public Sprite GetActionIcon(InputAction action)
        {
            if (CurrentSetting == null)
                return null;

            string bindActionName = GetActionBindingName(action);

            if (string.IsNullOrEmpty(bindActionName))
                return null;

            return CurrentSetting.TryGetInputIcon(bindActionName);
        }

        private void AttachEvents()
        {

        }

        private void DetachEvents()
        {

        }


        private void InitializeSettings()
        {
            _iconSettings = new Dictionary<InputDeviceType, InputIconsSetting>();

            foreach (var inputIcons in _inputIconsData.Icons)
            {
                _iconSettings[inputIcons.deviceType] = new InputIconsSetting(inputIcons);
            }
        }

        private string GetActionBindingName(InputAction action)
        {
            if (action == null || action.bindings.Count <= 0)
                return string.Empty;

            var index = action.GetBindingIndex();

            if (0 >= index && index <= action.bindings.Count - 1)
                return string.Empty;

            var path = action.bindings[index].effectivePath;
            return InputControlPath.ToHumanReadableString(path, InputControlPath.HumanReadableStringOptions.OmitDevice);
        }

        #endregion

        #region CLASSES

        internal class InputIconsSetting
        {
            #region VARIABLES

            private DeviceInputIcons _deviceInputIcons;
            private Dictionary<string, Sprite> _icons;

            #endregion

            #region PROPERTIES

            #endregion

            #region CONSTRUCTORS

            public InputIconsSetting() { }
            public InputIconsSetting(DeviceInputIcons deviceInputIcons)
            {
                _deviceInputIcons = deviceInputIcons;
                Initialize();
            }

            #endregion

            #region METHODS

            private void Initialize()
            {
                _icons = new Dictionary<string, Sprite>();
                foreach (var inputIcon in _deviceInputIcons.icons)
                    _icons[inputIcon.bindName] = inputIcon.icon;
            }

            public Sprite TryGetInputIcon(string bindName)
            {
                if (_icons.TryGetValue(bindName, out Sprite bindIcon))
                {
                    return bindIcon;
                }

                return null;
            }

            #endregion
        }

        #endregion
    }
}