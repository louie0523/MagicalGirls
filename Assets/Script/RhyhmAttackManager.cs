using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class RhyhmAttackManager : MonoBehaviour
{
    public static RhyhmAttackManager instance;

    public GameObject AttackUI;
    public RectTransform[] LineTarget;
    public GameObject NodePrefab;
    public int CurrentNode = 0;
    private List<RhythmNode> activeNodes = new List<RhythmNode>();
    public int MaxNode = 0;
    public bool isAttack = false;

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
        AttackUI.SetActive(false);
    }

    private void Update()
    {
        Inputget();
    }

    void Inputget()
    {
        if (!isAttack || CurrentNode >= activeNodes.Count) return;

        if (Input.GetKeyDown(KeyCode.Q))
            activeNodes[CurrentNode].JudgeInput(KeyCode.Q);
        else if (Input.GetKeyDown(KeyCode.W))
            activeNodes[CurrentNode].JudgeInput(KeyCode.W);
        else if (Input.GetKeyDown(KeyCode.E))
            activeNodes[CurrentNode].JudgeInput(KeyCode.E);
    }

    public void attackStart(Unit player, int Nodespeed, int Count, Vector3 Target)
    {
        RectTransform UIPostion = AttackUI.GetComponent<RectTransform>();
        UIPostion.position = Target;

        if (Nodespeed <= 3) Nodespeed = 3;

        isAttack = true;
        Nodespeed += 2;
        CurrentNode = 0;
        MaxNode = Count;
        activeNodes.Clear();
        AttackUI.SetActive(true);

        for (int i = 0; i < Count; i++)
        {
            int rand = Random.Range(0, 3);
            GameObject newNode = Instantiate(NodePrefab, AttackUI.transform);
            RectTransform nodeRect = newNode.GetComponent<RectTransform>();
            Vector2 spawnPosition = LineTarget[rand].anchoredPosition + new Vector2((20 + (5 * i)) * -1, 0);
            nodeRect.anchoredPosition = spawnPosition;

            RhythmNode node = newNode.GetComponent<RhythmNode>();
            node.StartKeySet(rand, Nodespeed, LineTarget[rand], i, player);
            activeNodes.Add(node);
        }
    }

    public void AttackEnd()
    {
        if(CurrentNode >= MaxNode && isAttack)
        {
            AttackUI.SetActive(false);
            isAttack = false;
        }
    }


}
