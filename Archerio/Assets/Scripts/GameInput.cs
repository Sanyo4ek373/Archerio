using UnityEngine;

namespace Archerio {
    public class GameInput : MonoBehaviour {
        private InputSystem playerInputActions;
        
        private void Awake() {
            playerInputActions = new InputSystem();
            playerInputActions.Player.Enable();
        }

        public Vector2 GetMovementVectorNormalized() {
            Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
            inputVector = inputVector.normalized;

            return inputVector;
        }  
    }
}