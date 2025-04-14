using UnityEngine;


[CreateAssetMenu(fileName = "New Unit", menuName = "Game/Unit")]
public class UnitData : ScriptableObject
{
    public int Unit_id = 0;
    public string Unit_name;
    public int MaxHp = 100;
    public int Attack = 20;
    public int Magic = 0;
    public int Physical_Defense = 50;
    public int Magical_Defense = 25;
    public int speed = 30;
    public float MaxStamina = 100f;
    public enum Teams
    {
        Player,
        Enemy,
        None,
    };
    public Teams teams;

    public string Unit_Explanation;

}
