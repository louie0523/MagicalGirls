using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public enum IsPlayer
{
    Npc,
    Player,
};
public enum Team
{
    Player,
    Enemy,
    None,
};
[System.Serializable]
public class MyName
{
    public bool isMyname = false;
    public string Myname;
}
public class Unit : MonoBehaviour
{
    [Header("플레이어 여부(리듬 공격 여부")]
    public IsPlayer isPlayer;
    [Header("ID 및 식별명")]
    public UnitData unitData;
    public Team team;
    public int position = 0;
    public int Unit_id;
    public string Unit_name;
    public MyName myname;
    [Header("스테이터스")]
    public int MaxHp;
    public int Hp;
    public float MaxStamina;
    public float Stamina;
    public int Attack;
    public int AttackPoint = 10;
    public int sence;
    public int Magic;
    public int Physical_Defense;
    public int Magical_Defense;
    public int speed;
    public string Unit_Explanation;
    [Header("가진 아이템")]
    public List<Item> Items = new List<Item>();
    [Header("전투")]
    public Animator animator;
    public bool isAlive = true;
    public Slider hpBar;
    public GameObject WeaponPoint;
    public Animator EfFect;
    public GameObject Effecter;
    public Unit AttackTartget;

    private void Start()
    {
        if (unitData != null)
        {
            StatusSetting();
            FighterMe();
        }
    }

    public void StatusSetting()
    {
        if (unitData.teams == UnitData.Teams.Player)
        {
            team = Team.Player;
        }
        else
        {
            team = Team.Enemy;
        }
        Unit_id = unitData.Unit_id;
        Unit_name = unitData.Unit_name;
        MaxHp = unitData.MaxHp;
        Hp = MaxHp;
        MaxStamina = unitData.MaxStamina;
        Stamina = MaxStamina;
        Attack = unitData.Attack;
        sence = unitData.sense;
        Magic = unitData.Magic;
        Physical_Defense = unitData.Physical_Defense;
        Magical_Defense = unitData.Magical_Defense;
        speed = unitData.speed;
        Unit_Explanation = unitData.Unit_Explanation;
        animator = transform.GetChild(0).GetComponent<Animator>();
        if (team == Team.Enemy)
        {
            HpBarEnemy();
        }
        if (Effecter != null)
        {
            Effecter.SetActive(false);
        }

    }


    public void FighterMe()
    {
        FightManager.instance.Fights_units.Add(gameObject);
    }

    public void Damage(int damage)
    {
        Hp -= damage;
        if (Hp <= 0 && isAlive)
        {
            isAlive = false;
            Hp = 0;
        }
        SettingHPbar();
    }

    public void Heal(int heal)
    {
        Hp += heal;
        if (Hp > MaxHp)
        {
            Hp = MaxHp;
        }
        SettingHPbar();
    }

    public void BattleSetting()
    {
        Rigidbody2D rb2D = GetComponent<Rigidbody2D>();
        rb2D.bodyType = RigidbodyType2D.Kinematic;
    }

    public void MyHpBar(Slider slider)
    {
        hpBar = slider;
        SettingHPbar();
    }

    public void SettingHPbar()
    {
        hpBar.value = (float)Hp / MaxHp;
    }

    public void AttackEnemy(Unit Target)
    {
        StartCoroutine(AttackRoutine(Target));
    }

    private IEnumerator AttackRoutine(Unit Target)
    {
        AttackTartget = Target;
        CapsuleCollider2D capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        capsuleCollider2D.enabled = false;
        Vector3 originalPos = transform.position;
        Vector3 targetPos = new Vector3(Target.transform.position.x + 5f, transform.position.y, transform.position.z);
        animator.SetTrigger("Dash");
        // 앞으로 이동 (1초)
        float duration = 1f;
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(originalPos, targetPos, t / duration);
            yield return null;
        }


        int Nodspeed = Target.sence - sence;
        if(isPlayer == IsPlayer.Player)
        {
            RhyhmAttackManager.instance.attackStart(this ,Nodspeed, AttackPoint, Target.transform.position);
            RhyhmAttackManager.instance.isAttack = true;
        } else
        {
            AttackBattleEnemy();
        }


        while(RhyhmAttackManager.instance.isAttack)
        {
            yield return null;
        }

        StartCoroutine(attackEnd(targetPos, originalPos, capsuleCollider2D));
    }

    IEnumerator attackEnd(Vector3 targetPos, Vector3 originalPos, CapsuleCollider2D capsuleCollider2D)
    {
        Debug.Log("공격 종료");
        yield return new WaitForSeconds(0.25f);
        Effecter.SetActive(false);

        animator.SetTrigger("BackDash");
        float t = 0f;
        float duration = 0.2f;
        while (t < duration)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(targetPos, originalPos, t / duration);
            yield return null;
        }

        capsuleCollider2D.enabled = true;
        // 완벽히 복귀 보정
        transform.position = originalPos;

        // 턴 진행
        BattleSystem.instance.ProessTurn();
    }


    public IEnumerator AttackBattleEnemy()
    {
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.15f);
        Effecter.SetActive(true);
        EfFect.SetTrigger("Effect");
        // 공격 처리
        AttackTartget.Damage(Attack / 10);
        Debug.Log(gameObject.name + "가 " + AttackTartget.gameObject.name + "에게 " + Attack + "의 피해를 입힙니다.");
        yield return new WaitForSeconds(0.1f);
        if(Effecter.activeSelf)
        {
            Effecter.SetActive(false);
        }
    }


    void HpBarEnemy()
    {
        hpBar = transform.Find("Canvas").transform.GetChild(0).GetComponent<Slider>();
        SettingHPbar();
    }

    public IEnumerator EnemyAttack()
    {
        if (!BattleSystem.instance.BattleEnd)
        {
            yield return new WaitForSeconds(1f);

            // 플레이어 리스트
            List<Unit> possibleTargets = BattleSystem.instance.Turn.FindAll(u => u.team == Team.Player && u.isAlive);

            if (possibleTargets.Count > 0)
            {
                Unit target = possibleTargets[Random.Range(0, possibleTargets.Count)];
                target.Damage(Attack);
                Debug.Log(gameObject.name + "가 " + target.gameObject.name + "에게 " + Attack + "의 피해를 입힙니다.");
            }

            BattleSystem.instance.ProessTurn();
        }
    }

}