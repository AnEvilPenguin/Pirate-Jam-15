using UnityEngine;

public class KeyAnimations : MonoBehaviour
{
    public Animator Animator;

    public void SetAnimationState(int state) =>
        Animator.SetInteger("State", state);
}
