using UnityEngine;

namespace Archerio {
    public class ArcherView : MonoBehaviour {
        private Animator _animator;
        private ArcherModel _model;

        private const string IS_RUN = "IsRun";
        private const string IS_ATTACK = "IsAttack";

        private void Update() {
            _animator.SetBool(IS_RUN, _model.IsRun);
            _animator.SetBool(IS_ATTACK, _model.IsAttack);
        }

        public void Construct(ArcherModel model) {
            _animator = GetComponent<Animator>();
            _model = model;
        }
    }
}