using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimation : MonoBehaviour
{
    public Animator animator;

    private void Awake()
    {
        animator= GetComponent<Animator>();
    }

    private void Start()
    {
        animator.enabled= false;
    }
}
