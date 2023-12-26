using UnityEngine;

namespace Archerio {

    public class Archer : MonoBehaviour {
        [SerializeField] private float _moveSpeed;

        [SerializeField] private GameInput gameInput;

        private void Update() {
            Vector2 inputVector = gameInput.GetMovementVectorNormalized();
            Vector3 moveDirection = new(inputVector.x, 0f, inputVector.y);  
            
            if (!CanMove(moveDirection)) {
                Vector3 moveDirectionX = new Vector3(moveDirection.x, 0, 0).normalized;

                if (CanMove(moveDirectionX)) moveDirection = moveDirectionX;   
                else {
                    Vector3 moveDirectionZ = new Vector3(0, 0, moveDirection.z).normalized;

                    if (CanMove(moveDirectionZ)) moveDirection = moveDirectionZ;
                }
            }
            
            if (CanMove(moveDirection)) transform.position += _moveSpeed * Time.deltaTime * moveDirection;
            
            float rotateSpeed = 10f;
            transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
        }
    

        private bool CanMove(Vector3 moveDirection) {
            float playerRadius = .7f;
            float playerHeight = 2f;
            float moveDistance = _moveSpeed * Time.deltaTime;

            bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirection, moveDistance);

            return canMove;
        }
    }
}
