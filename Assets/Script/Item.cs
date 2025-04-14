using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Game/Item")]
public class Item : ScriptableObject
{
    public int item_id = 0;
    public string Item_name;
    public Sprite Icon;
    public int item_Hp;
    public int item_Atk;
    public int item_Magic;
    public int item_Phy_Depense;
    public int item_Mag_Depense;
    public int item_Speed;
    public string item_Explanation;
}
