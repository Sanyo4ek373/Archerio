using System.Collections;
using UnityEngine;

namespace Archerio {
    [RequireComponent(typeof(ArcherModel))]
    [RequireComponent(typeof(ArcherView))]
    public class ArcherController : MonoBehaviour {
        [SerializeField] private GameInput _gameInput;
        [SerializeField] private PoolManager _poolManager;
        
        [SerializeField] private Transform _shootPosition;
        [SerializeField] private EnemyPool _enemyPool;

        private ArcherModel  _model;
        private ArcherView _view;

        private bool _isShotting = true;

        private void Awake() {
            _model = GetComponent<ArcherModel>();
            _view = GetComponent<ArcherView>();
            _view.Construct(_model);
        }

        private void Update() {
            Vector2 inputVector = _gameInput.GetMovementVectorNormalized();
            Vector3 moveDirection = new(inputVector.x, 0f, inputVector.y);  
            
            if (CanMove(moveDirection)) transform.position += _model.MoveSpeed * Time.deltaTime * moveDirection;
            _model.IsRun = moveDirection != Vector3.zero;
            
            transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * _model.RotateSpeed);

            GameObject enemy = _enemyPool.GetEnemy();

            if (enemy == null) {_model.IsAttack = false; return;}
            Transform enemyPosition = enemy.transform;
            _model.IsAttack = !_model.IsRun;

            if (!_model.IsRun) RotateToTarget(enemyPosition);

            if (_model.IsRun || !_isShotting) return;
            StartCoroutine(Shoot(_model.ReloadingTime, _poolManager, _shootPosition.transform.position, enemy.transform.position));
        }
    

        private bool CanMove(Vector3 moveDirection) {
            float playerRadius = .5f;
            float playerHeight = 2.3f;
            float moveDistance = _model.MoveSpeed * Time.deltaTime;

            bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirection, moveDistance);

            return canMove;
        }

        private IEnumerator Shoot(float waitTime, PoolManager poolManager,Vector3 shootPosition, Vector3 targetPosition) {
            _isShotting = false;

            poolManager.GetArrowFromPool().Construct(shootPosition, targetPosition, _model.Damage);

            yield return new WaitForSeconds(waitTime);

            _isShotting = true;
        }

        private void RotateToTarget(Transform target) {
            if (target == null) return;

            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _model.RotateSpeed);
        }
    }
}
