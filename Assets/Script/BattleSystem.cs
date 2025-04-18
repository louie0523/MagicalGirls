using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;


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
    public bool BattleEnd = false;

    [Header("UI")]
    public TextMeshProUGUI CurrentTurner;
    public List<Slider> PlayerHPbar = new List<Slider>();

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
        HPbarSetting();
        CurrentTurnTextSet();
        
    }

    void Update()
    {
        if(!BattleEnd)
        {
            TargetEnemy();
        }
    }

    void TargetEnemy()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                Unit hitUnit = hit.collider.GetComponent<Unit>();
                Debug.Log(hitUnit.gameObject.name);
                if (hitUnit.team == Team.Enemy && Turn[CurrentTurn].team == Team.Player && turnProess && hitUnit.isAlive)
                {
                    turnProess = false;
                    Turn[CurrentTurn].AttackEnemy(hitUnit);
                }
            }
        }
    }

    void HPbarSetting()
    {
        int num = 0;
        for(int i = 0; i < PlayerHPbar.Count; i++)
        {
            if(Turn[i].team == Team.Player)
            {
                Turn[i].MyHpBar(PlayerHPbar[num]);
                num++;
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

    void CurrentTurnTextSet()
    {
        CurrentTurner.text = Turn[CurrentTurn].gameObject.name + "의 턴입니다.";
    }

    public void ProessTurn()
    {
        if (!Turn.Exists(u => u.team == Team.Player && u.isAlive))
        {
            Debug.Log("플레이어 패배");
            BattleEnd = true;
            return;
        }
        if (!Turn.Exists(u => u.team == Team.Enemy && u.isAlive))
        {
            Debug.Log("플레이어 승리");
            BattleEnd = true;
            return;
        }
        if (!BattleEnd)
        {
            turnProess = true;


            CurrentTurn++;
            if (CurrentTurn >= Turn.Count)
            {
                CurrentTurn = 0;
                TurnSetting();
            }

            while(!Turn[CurrentTurn].isAlive)
            {
                CurrentTurn++;
                TurnSetting();
            }

            CurrentTurnTextSet();
            if (Turn[CurrentTurn].team == Team.Enemy && Turn[CurrentTurn].isAlive)
            {
                StartCoroutine(Turn[CurrentTurn].EnemyAttack());
            }
        }
       
    }

}
