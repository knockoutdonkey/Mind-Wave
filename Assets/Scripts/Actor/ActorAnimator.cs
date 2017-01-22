using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ActorAnimator : MonoBehaviour {

    private Animator _animator;

    void Awake() {
        _animator = GetComponent<Animator>();
        SetWalkingSpeed(0f);
    }

    public void SetWalkingSpeed(float speed) {
        _animator.SetFloat("Speed", speed);
    }
}
