using UnityEngine;

namespace Gameplay.Inputs
{
    public class InputManager : MonoSingleton<InputManager>
    {
        #region VARIABLES

        [SerializeField] private InputBindsController _bindsController;
        [SerializeField] private DeviceDetectionController _deviceDetectionController;
        [SerializeField] private InpuIconsController _inpuIconsController;

        #endregion

        #region PROPERTIES

        public InputBindsController InputBindsController => _bindsController;
        public DeviceDetectionController DeviceDetectionController => _deviceDetectionController;
        public InpuIconsController InpuIconsController => _inpuIconsController;

        #endregion

        #region UNITY_METHODS

        //For future -> controllers will no longer be a monobehavior
        protected override void Awake()
        {
            base.Awake();
            InputBindsController.Initialzie();
            DeviceDetectionController.Initialize();
            InpuIconsController.Initialize();
        }

        private void Start()
        {
            InputBindsController.LateInitialzie();
            DeviceDetectionController.LateInitialzie();
            InpuIconsController.LateInitialzie();
        }

        private void OnDestroy()
        {
            InputBindsController.CleanUp();
            DeviceDetectionController.CleanUp();
            InpuIconsController.CleanUp();
        }

        #endregion

        #region METHODS

        #endregion
    }
}