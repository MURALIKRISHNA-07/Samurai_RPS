using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    // Animation clips for the player
    public AnimationClip lowAttackClip;
    public AnimationClip midAttackClip;
    public AnimationClip highAttackClip;
    public AnimationClip defendLowAttackClip;
    public AnimationClip defendMidAttackClip;
    public AnimationClip defendHighAttackClip;
    public AnimationClip idleClip;
    public AnimationClip hurtClip;

    // Animation clips for the enemy
    public AnimationClip showHighClip;
    public AnimationClip showLowClip;
    public AnimationClip showMidClip;

    public void PlayAnimation(Animator animator, PlayerAction action)
    {
        // Play the appropriate animation clip based on the action
        switch (action)
        {
            case PlayerAction.LowAttack:
                PlayAnimationClip(animator, lowAttackClip);
                break;
            case PlayerAction.MidAttack:
                PlayAnimationClip(animator, midAttackClip);
                break;
            case PlayerAction.HighAttack:
                PlayAnimationClip(animator, highAttackClip);
                break;
            case PlayerAction.DefendLowAttack:
                PlayAnimationClip(animator, defendLowAttackClip);
                break;
            case PlayerAction.DefendMidAttack:
                PlayAnimationClip(animator, defendMidAttackClip);
                break;
            case PlayerAction.DefendHighAttack:
                PlayAnimationClip(animator, defendHighAttackClip);
                break;
            case PlayerAction.Idle:
                PlayAnimationClip(animator, idleClip);
                break;
            default:
                break;
        }
    }

    public void ShowHintAnimation(Animator animator, PlayerAction hintAction)
    {
        // Play the appropriate animation clip for showing the hint
        switch (hintAction)
        {
            case PlayerAction.LowAttack:
                PlayAnimationClip(animator, showLowClip);
                break;
            case PlayerAction.MidAttack:
                PlayAnimationClip(animator, showMidClip);
                break;
            case PlayerAction.HighAttack:
                PlayAnimationClip(animator, showHighClip);
                break;
            default:
                break;
        }
    }

    public void PlayHurtAnimation(Animator animator)
    {
        // Play the hurt animation clip
        PlayAnimationClip(animator, hurtClip);
    }

    private void PlayAnimationClip(Animator animator, AnimationClip clip)
    {
        if (clip != null && animator != null)
        {
            animator.CrossFade(clip.name, 0.1f); // Adjust the second parameter for the duration of the crossfade
        }
        else
        {
            Debug.LogWarning("Animation clip or animator is null.");
        }
    }
}
