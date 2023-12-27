using UnityEngine;

namespace Archerio {
    [RequireComponent(typeof(ArcherModel))]
    [RequireComponent(typeof(ArcherView))]
    public class ArcherController : MonoBehaviour {
        [SerializeField] private GameInput gameInput;

        private ArcherModel  _model;
        private ArcherView _view;

        private void Awake() {
            _model = GetComponent<ArcherModel>();
            _view = GetComponent<ArcherView>();
            _view.Construct(_model);
        }

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
            
            if (CanMove(moveDirection)) transform.position += _model.MoveSpeed * Time.deltaTime * moveDirection;

            _model.IsRun = moveDirection != Vector3.zero;
            
            transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * _model.RotateSpeed);
        }
    

        private bool CanMove(Vector3 moveDirection) {
            float playerRadius = .5f;
            float playerHeight = 2.3f;
            float moveDistance = _model.MoveSpeed * Time.deltaTime;

            bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirection, moveDistance);

            return canMove;
        }
    }
}
