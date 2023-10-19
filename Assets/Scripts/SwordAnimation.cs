using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAnimation : MonoBehaviour
{
    private Animator animator;

    private float currentAttackCombo = 0;
    private float maxAttackCombo = 3;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //Debug.Log(currentAttackCombo);

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("AttackDone"))
        {
            currentAttackCombo = 0;
            animator.SetFloat("AttackCombo", currentAttackCombo);
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("BackToIdle2"))
        {
            currentAttackCombo = 0;
            animator.SetFloat("AttackCombo", currentAttackCombo);
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("BackToIdle3"))
        {
            currentAttackCombo = 0;
            animator.SetFloat("AttackCombo", currentAttackCombo);
        }
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

    public void TriggerAttackCombo()
    {
        currentAttackCombo = Mathf.Clamp(currentAttackCombo + 1, 0, maxAttackCombo);

        animator.SetFloat("AttackCombo", currentAttackCombo);
    }

}
