using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "Game/Unit")]
public class Unit : ScriptableObject
{
    public int Unit_id = 0;
    public string Unit_name;
    public int MaxHp = 100;
    public int Hp = 100;
    public int Attack = 20;
    public int Magic = 0;
    public int Physical_Defense = 50;
    public int Magical_Defense = 25;
    public int speed = 30;

    public string Unit_Explanation;


}
