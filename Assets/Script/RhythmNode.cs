using UnityEngine;

public class RhythmNode : MonoBehaviour
{
    public float fallSpeed = 5f; // �������� �ӵ�
    public float laneOffset = 0f; // �� ������ y ��ġ�� ���� ������
    public RectTransform rectTransform;
    public KeyCode key;
    public bool IsCheking = false;
    public RectTransform tartgetPostion;
    public int MyNum = 0;
    public Unit Player;

    private int laneIndex;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        // ��尡 �������� �ִϸ��̼� (UI������ anchoredPosition�� ���)
        rectTransform.anchoredPosition += Vector2.right * fallSpeed * Time.deltaTime;
        ImTaltheJU();
    }

    public void JudgeInput(KeyCode inputKey)
    {
        if (!IsCheking) return;

        IsCheking = false;

        if (inputKey == key)
        {
            float distance = Mathf.Abs(rectTransform.position.x - tartgetPostion.position.x);
            Player.StartCoroutine(Player.AttackBattleEnemy());
            if (distance <= 0.35f)
                Debug.Log("Perfect! : " + distance);
            else if (distance <= 1f)
                Debug.Log("Good! : " + distance);
            else
                Debug.Log("Miss! : " + distance);
        }
        else
        {
            Debug.Log("Ʋ�� Ű ����!");
        }

        RhyhmAttackManager.instance.CurrentNode++;
        RhyhmAttackManager.instance.AttackEnd();
        Destroy(gameObject);
    }

    public void ImTaltheJU()
    {
        float distance = rectTransform.position.x - tartgetPostion.position.x;
        if (distance >= 1.75f && IsCheking)
        {
            Debug.Log("��尡 �������� �����ƽ��ϴ�!");
            IsCheking = false;
            RhyhmAttackManager.instance.CurrentNode++;
            RhyhmAttackManager.instance.AttackEnd();
            Destroy(gameObject);
        }
    }


    // ��尡 �������� ���� ����
    public void StartKeySet(int laneIndex, float nodeSpeed, RectTransform target, int Num, Unit player)
    {
        Player = player;
        MyNum = Num;
        this.laneIndex = laneIndex;
        this.fallSpeed = nodeSpeed;
        tartgetPostion = target;
        
        switch(this.laneIndex)
        {
            case 0:
                key = KeyCode.Q; break;
            case 1:
                key = KeyCode.W; break;
            case 2:
                key = KeyCode.E; break;
        }
        IsCheking = true;
    }
}
