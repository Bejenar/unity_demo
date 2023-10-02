using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAnimator : MonoBehaviour
{
    private Animator _animator;
    private static readonly int PopTrigger = Animator.StringToHash("pop");

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Pop()
    {
        _animator.SetTrigger(PopTrigger);
    }
}
