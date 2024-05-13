using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteRightAnimator : MonoBehaviour
{
    [SerializeField] private NoteRight noteRight;
    private Animator animator;
    private const string IS_HIT = "IsHit";

    private void Awake()
    {
        animator = GetComponent<Animator>();

    }

    private void Update()
    {
        animator.SetBool(IS_HIT, noteRight.IsHit());
    }
}
