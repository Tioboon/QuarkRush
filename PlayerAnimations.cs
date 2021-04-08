using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public Animator _animator;
    public AnimationClip mortalClip;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void ChangeToGrap()
    {
        _animator.Play("Grap");
    }

    public void ChangeToFlying()
    {
        _animator.Play("Flying");
    }

    public void ChangeToFront()
    {
        _animator.Play("Front");
    }

    public void ChangeToBack()
    {
        _animator.Play("Back");
    }

    public void ChangeToMortal()
    {
        _animator.Play("Mortal");
    }
}
