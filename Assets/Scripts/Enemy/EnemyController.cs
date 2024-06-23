using System;
using System.Collections;
using UnityEngine;

namespace Archerio {
    [RequireComponent(typeof(EnemyModel))]
    [RequireComponent(typeof(EnemyView))]
    public class EnemyController : MonoBehaviour {
        [SerializeField] private EnemyPool _enemyPool;
        [SerializeField] private ArcherController _target;

        private EnemyModel _model;
        private EnemyView _view;
        private ArcherModel _archer;

        private bool _isAttack = true;
        private bool _canAttack = false;

        private void Awake() {
            _model = GetComponent<EnemyModel>();
            _view = GetComponent<EnemyView>();
            _view.Construct(_model);
            _model.OnDied += Death;
        }

        private void Update() {
            Vector3 moveDirection = _target.transform.position - transform.position;

            if (!_canAttack) {
                transform.position += _model.MoveSpeed * Time.deltaTime * moveDirection.normalized;
                transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * _model.RotateSpeed);
            }
            
            Vector3 firstEnemyPosition = _enemyPool.GetEnemy().transform.position;
            if (FindEnemyPosition(transform.position) < FindEnemyPosition(firstEnemyPosition)) _enemyPool.MakeEnemyFirst(gameObject);

            _model.IsAttack = _canAttack;
            _model.IsRun = !_canAttack;

            if(_canAttack && _isAttack) {
                StartCoroutine(Attack(_model.ReloadingTime, _archer));
            }
        }

        private float FindEnemyPosition(Vector3 selfPosition) {
            Vector3 position =_target.transform.position - selfPosition;
            float distance = Math.Abs(position.x) + Math.Abs(position.z);
            return distance;
        }

        private void Death() {
            _enemyPool.DeleteEnemy(gameObject);
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other) {
            if (other.TryGetComponent<ArcherModel>(out var archer)) {
                _archer = archer;
                _canAttack = true;
            }
        }

        private void OnTriggerExit(Collider other) {
            if (other.TryGetComponent<ArcherModel>(out _)) {
                _canAttack = false;
            }
        }

        private IEnumerator Attack(float waitTime, ArcherModel archer) {
            _isAttack = false;
            archer.TakeDamage(_model.Damage);

            yield return new WaitForSeconds(waitTime);

            _isAttack = true;
        }
    }
}
