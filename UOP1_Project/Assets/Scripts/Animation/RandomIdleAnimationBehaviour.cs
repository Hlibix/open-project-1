using UnityEngine;

public class RandomIdleAnimationBehaviour : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var randomIdle = Random.Range(0, 2);
        animator.SetInteger("RandomIdle", randomIdle);
    }
}