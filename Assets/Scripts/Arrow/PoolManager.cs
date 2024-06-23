using System.Collections.Generic;
using UnityEngine;

namespace Archerio {
    public class PoolManager : MonoBehaviour {
        [SerializeField] private Arrow _arrowPrefab;

        private Queue<Arrow> _arrowPool = new(); 
 
        public Arrow GetArrowFromPool() { 
            Arrow arrow = _arrowPool.Count > 0 ? _arrowPool.Dequeue() : Instantiate(_arrowPrefab, Vector3.zero, Quaternion.identity);
            arrow.OnReleaseEvent += ReturnArrowToPool;
            return arrow; 
        } 
 
        public void ReturnArrowToPool(Arrow arrow) { 
            arrow.OnReleaseEvent -= ReturnArrowToPool;
            arrow.ResetArrow(); 
            _arrowPool.Enqueue(arrow); 
        } 
    } 
}
