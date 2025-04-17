using UnityEngine;
using System.Collections.Generic;


public class BattleSystem : MonoBehaviour
{
    public static BattleSystem instance;
    [Header("기초 세팅")]
    public List<Transform> PlayerPostion;
    public List<Transform> EnemyPostion;

    [Header("전투 상황")]
    public List<Unit> Turn = new List<Unit>();
    public int CurrentTurn = 0;
    public bool turnProess = true;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PostionSetting();
        StartBattileSet();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log("클릭한 오브젝트: " + hit.collider.name);
            }
        }
    }

    void PostionSetting()
    {
        GameObject pivot = GameObject.Find("Unit_Position");
        Transform Enemy = pivot.transform.Find("Enemy");
        Transform Player = pivot.transform.Find("Player");
        foreach (Transform ep in Enemy)
        {
            EnemyPostion.Add(ep);
        }
        foreach (Transform pp in Player)
        {
            PlayerPostion.Add(pp);
        }
    }

    public void StartBattileSet()
    {
        foreach(GameObject obj in FightManager.instance.Fights_units)
        {
           Unit unit = obj.GetComponent<Unit>();
           Turn.Add(unit);
           Player player = obj.GetComponent<Player>();
           if(player != null)
            {
                player.enabled = false;
            }
           if(unit.team == Team.Player)
           {
                obj.transform.position = PlayerPostion[unit.position].transform.position;

           } else if(unit.team == Team.Enemy)
           {
                obj.transform.position = EnemyPostion[unit.position].transform.position;
           }
            Debug.Log(obj.name + "가 " + unit.position + "번째 위치로 이동합니다.");
        }
        TurnSetting();
    }

    void TurnSetting()
    {
        Turn.Sort((a, b) => b.speed.CompareTo(a.speed));
    }

}
