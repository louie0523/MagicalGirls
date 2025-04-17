using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public enum CurrentStatus
{
    NoFight,
    Fight,
};

public class FightManager : MonoBehaviour
{
    public static FightManager instance;
    
    public CurrentStatus status;

    public List<GameObject> Fights_units = new List<GameObject>();




    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && status == CurrentStatus.NoFight)
        {
            status = CurrentStatus.Fight;
            StartBattle();
        }
    }

    public void StartBattle()
    {
        foreach(GameObject obj in Fights_units)
        {
            DontDestroyOnLoad (obj);
        }

        SceneManager.LoadScene("Battle");
    }
}
