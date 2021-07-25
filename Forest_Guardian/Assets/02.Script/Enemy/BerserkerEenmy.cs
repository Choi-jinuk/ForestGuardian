using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BerserkerEenmy : Character
{
    
    [Header("Animation")]
    public Animator anim;

    [Header("Attack Speed")]
    public float AttackSpeed = 5f;

    [Header("Move Speed")]
    private GameObject spirit;
    private bool isAttack;
    private float elpasedTime = 5f;

    [Header("StageManager")]
    public StageManager stageManager;

    bool isDie = false;

    private void OnEnable()
    {
        //죽는 메소드가 여러번 호출되는 오류, 한번만 처리하도록 플래그 생성
        isDie = false;
        
        //임시test
        isAttack = false;
        hp = 100; //초기 hp설정
    }
    private void Update()
    {
        if (isAttack == true && spirit != null)
        {
            DontRun();
            Attack();
        }
        else
        {
            Move();
            Run();
        }
    }
    private void Move()
    {
        this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(0f, 0f), Time.deltaTime * MoveSpeed);
    }
    private void Attack()
    {
        if (elpasedTime > AttackSpeed)
        {
            anim.SetTrigger("isAttack");
            elpasedTime = 0f;
            Invoke("AttackDamage", 2.0f);
        }
        elpasedTime += Time.deltaTime;
    }

    private void AttackDamage()
    {
        spirit.GetComponent<Character>().Damage(damageLevel);
    }

    public override void Die()
    {
        anim.SetTrigger("isDie");
        Invoke("setfalse", 1.5f);
    }

    public override void Damage(int damage)
    {
        if (!damageOn) return;
        if (damage == 0) return;

        hp -= damage;
        anim.SetTrigger("isHit");

        if (hp <= 0 && !isDie)
        {
            isDie = true;
            Die();
        }
    }

    public void Run()
    {
        if (isMoving == false)
        {
            isMoving = true;
            anim.SetBool("isRun", true);
        }
    }

    public void DontRun()
    {
        if (isMoving == true)
        {
            isMoving = false;
            anim.SetBool("isRun", false);
        }
    }

    private void setfalse()
    {
        stageManager.ReduceEnemyCount();
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spirit"))
        {
            isAttack = true;
            spirit = collision.gameObject;
        }
    }
}
