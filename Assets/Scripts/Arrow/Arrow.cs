using System;
using System.Collections;
using UnityEngine;

namespace Archerio {
    public class Arrow : MonoBehaviour {
        [SerializeField] private float _speed;
        [SerializeField] private float _lifeTime;

        public event Action<Arrow> OnReleaseEvent;

        private Vector3 _direction;
        private bool _isActive;
        private float _damage;
        private Coroutine _arrowFlyCoroutine;

        private void Update() {
            if (!_isActive) return;

            transform.position += _speed * Time.deltaTime * _direction;             
        }

        public void Construct(Vector3 shootPosition, Vector3 targetPosition, float damage) {
            gameObject.SetActive(true);

            Vector3 moveDirection = new(targetPosition.x - shootPosition.x, 0, targetPosition.z - shootPosition.z);
            _direction = moveDirection.normalized;
            transform.position = shootPosition;

            float angle = GetAngle(_direction);
            transform.eulerAngles = new Vector3(0, -angle, 90);

            _isActive = true;
            _damage = damage;

            _arrowFlyCoroutine = StartCoroutine(ArrowFly(_lifeTime));
        }

        public void ResetArrow() {
            if (_arrowFlyCoroutine != null) StopCoroutine(_arrowFlyCoroutine);

            _isActive = false;
            gameObject.SetActive(false);
            transform.localPosition = Vector3.zero;
        }

        private void OnTriggerEnter(Collider other) {
            if (other.TryGetComponent<EnemyModel>(out var enemy)) {
                enemy.TakeDamage(_damage);
                ResetArrow();
            }
        }

        private float GetAngle(Vector3 direction) {
            direction = direction.normalized;
            float n = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;

            return n;
        }

        private IEnumerator ArrowFly(float lifeTime) {
            yield return new WaitForSeconds(lifeTime);
            Release();
        }

        private void Release() {
            OnReleaseEvent?.Invoke(this);
        }

    }
}
