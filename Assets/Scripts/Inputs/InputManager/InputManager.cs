using UnityEngine;

namespace Gameplay.Inputs
{
    public class InputManager : MonoSingleton<InputManager>
    {
        #region VARIABLES

        [SerializeField] private InputMapController _mapController;
        [SerializeField] private DeviceDetectionController _deviceDetectionController;
        [SerializeField] private InpuIconsController _inpuIconsController;

        #endregion

        #region PROPERTIES

        public InputMapController MapController => _mapController;
        public DeviceDetectionController DeviceDetectionController => _deviceDetectionController;
        public InpuIconsController InpuIconsController => _inpuIconsController;

        #endregion

        #region UNITY_METHODS

        //For future -> controllers will no longer be a monobehavior
        protected override void Awake()
        {
            base.Awake();
            MapController.Initialzie();
            DeviceDetectionController.Initialize();
            InpuIconsController.Initialize();
        }

        private void Start()
        {
            MapController.LateInitialzie();
            DeviceDetectionController.LateInitialzie();
            InpuIconsController.LateInitialzie();
        }

        private void OnDestroy()
        {
            MapController.CleanUp();
            DeviceDetectionController.CleanUp();
            InpuIconsController.CleanUp();
        }

        #endregion

        #region METHODS

        #endregion
    }
}