using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    public int Magic;
    public int Physical_Defense;
    public int Magical_Defense;
    public int speed;
    public string Unit_Explanation;
    [Header("가진 아이템")]
    public List<Item> Items = new List<Item>();
    [Header("전투")]
    public Unit Target;
    public bool isAlive = true;

    private void Start()
    {
        if(unitData != null)
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
        } else
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
        Magic = unitData.Magic;
        Physical_Defense = unitData.Physical_Defense;
        Magical_Defense = unitData.Magical_Defense;
        speed = unitData.speed;
        Unit_Explanation = unitData.Unit_Explanation;
    }

    public void FighterMe()
    {
        FightManager.instance.Fights_units.Add(gameObject);
    }

    public void Damage(int damage)
    {
        Hp -= damage;
        if(Hp <= 0 && isAlive)
        {
            isAlive = false;
            Hp = 0;
        }
    }

    public void AttackEnemy(Unit Target)
    {
        Target.Damage(Attack);
        Debug.Log(gameObject.name + "가 " + Target.gameObject.name + "에게 " + Attack + "의 피해를 입힙니다.");
        BattleSystem.instance.ProessTurn();
    }


    public void EnemyAttack()
    {
        if(!BattleSystem.instance.BattleEnd)
        {
            int rand = Random.Range(0, BattleSystem.instance.Turn.Count);
            while (BattleSystem.instance.Turn[rand].team == Team.Enemy && !BattleSystem.instance.BattleEnd)
            {
                rand = Random.Range(0, BattleSystem.instance.Turn.Count);
            }
            BattleSystem.instance.Turn[rand].Damage(Attack);
            Debug.Log(gameObject.name + "가 " + BattleSystem.instance.Turn[rand].gameObject.name + "에게 " + Attack + "의 피해를 입힙니다.");
            BattleSystem.instance.ProessTurn();
        }
    }

}
