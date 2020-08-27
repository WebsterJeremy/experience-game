using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.AI;

public enum FSMState { Idle, Move, Attack, FindCover, Dead }

public class Enemy : Character
{
    #region AccessVariables


    [Header("Attack")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private float playerRealizeRange = 10f;
    [SerializeField] private GameObject meleeAttackRange;
    [SerializeField] private FSMState currentState = FSMState.Idle;

    [Header("Setup")]
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private LayerMask attackLayerMask;

    #endregion
    #region PrivateVariables

    protected GameObject target;
    protected Area area;

    protected WaitForSeconds Delay500 = new WaitForSeconds(0.5f);
    protected WaitForSeconds Delay250 = new WaitForSeconds(0.25f);

    protected NavMeshAgent navMeshAgent;

    protected float distance = 100f;
    protected bool canAttack = false;
    protected bool behindCover = false;

    protected float attackTime = 2f;
    protected float attackTimeCalc = 0f;

    #endregion
    #region Initlization


    protected override void Start()
    {
        base.Start();

        target = GameObject.FindGameObjectWithTag("Player");

//        StartCoroutine(FSM());
//        StartCoroutine(CalcCoolTime());

        navMeshAgent = GetComponent<NavMeshAgent>();
    }


    #endregion
    #region Getters & Setters

    public Area Area { get { return area; } }

    #endregion
    #region Main

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Area>() != null)
        {
            area = other.GetComponent<Area>();
        }
    }

    protected override void UpdateHealthbar()
    {

    }

    protected override void Death()
    {
        base.Death();

        if (dead) return;

        if (area != null)
        {
            area.EnemyKilled(this.gameObject);
        } 

//        Instantiate(GameplayController.Instance.enemyDeathEffect, transform.position, Quaternion.Euler(90, 0, 0));

        Destroy(this.gameObject, 1.3f);
    }


    #endregion
    #region AI


    protected virtual IEnumerator FSM() // Finite State Machine
    {
        yield return null;

        while (area == null ||!area.Entered)
        {
            yield return Delay500;
        }

        while (currentState != FSMState.Dead)
        {
            yield return StartCoroutine(currentState.ToString());
        }
    }

    protected virtual IEnumerator Idle()
    {
        yield return null;
        if (dead) yield break;

        if (animator != null && animator.GetCurrentAnimatorStateInfo(0).IsName("IDLE"))
        {
            animator.SetTrigger("IDLE");
        }

        currentState = FSMState.Attack;
        currentState = FSMState.Idle;
        currentState = FSMState.Move;
    }

    protected virtual IEnumerator Attack()
    {
        yield return null;
        if (dead) yield break;

        navMeshAgent.stoppingDistance = 0f;
        navMeshAgent.isStopped = true;
        navMeshAgent.SetDestination(target.transform.position);

        yield return Delay250;
        if (dead) yield break;

        navMeshAgent.isStopped = false;
        navMeshAgent.speed = 30f;
        canAttack = false;

        if (animator != null && !animator.GetCurrentAnimatorStateInfo(0).IsName("ATTACK"))
        {
            animator.SetFloat("AttackSpeed", 1f);
            animator.SetTrigger("ATTACK");
        }

        yield return Delay250;
        if (dead) yield break;

        navMeshAgent.speed = moveSpeed;
        navMeshAgent.stoppingDistance = attackRange;
        currentState = FSMState.Idle;
    }

    protected virtual IEnumerator Move()
    {
        yield return null;
        if (dead) yield break;

        if (animator != null && !animator.GetCurrentAnimatorStateInfo(0).IsName("MOVE") &&
            Vector3.Distance(transform.position, target.transform.position) > attackRange)
        {
            animator.SetTrigger("MOVE");
        }

        if (AttackPattern() && canAttack)
        {
            currentState = FSMState.Attack;
        }
        else if (distance > playerRealizeRange)
        {
            navMeshAgent.SetDestination(transform.position - Vector3.forward * 5f);
        }
        else
        {
            navMeshAgent.SetDestination(target.transform.position);
        }
    }

    protected virtual bool AttackPattern()
    {
        if (target == null) return false;

        Vector3 targetDir = new Vector3(target.transform.position.x - transform.position.x, 0f, target.transform.position.z - transform.position.z);

        Physics.Raycast(new Vector3(transform.position.x, 0.5f, transform.position.z), targetDir, out RaycastHit hit, 30f, attackLayerMask);
        distance = Vector3.Distance(target.transform.position, transform.position);

        if (hit.transform != null) return false;
        if (hit.transform.CompareTag("Player") && distance < attackRange)
        {
            return true;
        }

        return false;
    }

    protected virtual IEnumerator CalcCoolTime()
    {
        while (true)
        {
            yield return null;

            if (!canAttack)
            {
                attackTimeCalc -= Time.deltaTime;

                if (attackTimeCalc <= 0)
                {
                    attackTimeCalc = attackTime;
                    canAttack = true;
                }
            }
        }
    }

    protected void AttackTarget()
    {
        if (target != null)
        {
            if (Vector3.Distance(target.transform.position, transform.position) < attackRange)
            {
                Character character = target.GetComponent<Character>();

                if (character != null)
                {
                    DMGInfo dmgInfo = new DMGInfo(this.gameObject, Random.Range(10, 15), 0.5f);

                    character.TakeDamage(dmgInfo);
//                    meleeAttackRange.SetActive(false);
                }
            }
        }
    }

    #endregion
}
