using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAnimation : MonoBehaviour
{
    private Animator animator; 
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void WalkAnim(bool IsWalking)
    {
        animator.SetBool("IsWalking", IsWalking); 
    }

    public void AttackAnim1(bool Attack1)
    {
        animator.SetBool("Attack1", Attack1);
    }

    public void AttackAnim2(bool Attack2)
    {
        animator.SetBool("Attack2", Attack2);
    }

    public void AttackAnim3(bool Attack3)
    {
        animator.SetBool("Attack3", Attack3);
    }

    public void AttackEnd1()
    {
        animator.SetBool("Attack1", false);
    }

    public void AttackEnd2()
    {
        animator.SetBool("Attack2", false);
    }

    public void AttackEnd3()
    {
        animator.SetBool("Attack3", false);
    }

}
