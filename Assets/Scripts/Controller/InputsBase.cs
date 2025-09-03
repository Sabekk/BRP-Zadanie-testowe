using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Inputs
{
    public abstract class InputsBase
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES

        protected InputBinds Binds { get; set; }

        #endregion

        #region CONSTRUCTORS

        public InputsBase() { }
        public InputsBase(InputBinds binds)
        {
            Binds = binds;
        }

        #endregion
        #region METHODS

        public abstract void Enable();
        public abstract void Disable();

        #endregion
    }
}
