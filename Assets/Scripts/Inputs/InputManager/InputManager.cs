using UnityEngine;

namespace Gameplay.Inputs
{
    public class InputManager : MonoSingleton<InputManager>
    {
        #region VARIABLES

        [SerializeField] private InputMapController _mapController;
        [SerializeField] private DeviceDetectionController _deviceDetectionController;

        #endregion

        #region PROPERTIES

        public InputMapController MapController => _mapController;
        public DeviceDetectionController DeviceDetectionController => _deviceDetectionController;

        #endregion

        #region UNITY_METHODS

        private void Start()
        {
            MapController.Initialzie();
            DeviceDetectionController.Initialize();
        }

        private void OnDestroy()
        {
            MapController.CleanUp();
            DeviceDetectionController.CleanUp();
        }

        #endregion

        #region METHODS

        #endregion
    }
}