using System.Runtime.ExceptionServices;
using UnityEngine;

namespace KartGame.KartSystems {

    public class KeyboardInput : BaseInput
    {

        public int playerID = 1;
        private string _turnInputName = "Horizontal";
        private string _accelerateButtonName = "Accelerate";
        private string _brakeButtonName = "Brake";

        public override InputData GenerateInput() {
            return new InputData
            {
                Accelerate = Input.GetButton(_accelerateButtonName),
                Brake = Input.GetButton(_brakeButtonName),
                TurnInput = Input.GetAxis(_turnInputName+playerID)
            };
        }
    }
}
