using NUnit.Framework;
using UnityEngine;

public enum Team
{
    Player,
    Enemy,
    None,
};

public class Unit : MonoBehaviour
{
    [Header("Id Info")]
    public UnitData unitData;
    public Team team;
    public int Unit_id;
    public string Unit_name;
    [Header("Stat Info")]
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
        FightManager.instance.Fights_units.Add(this);
    }

}
