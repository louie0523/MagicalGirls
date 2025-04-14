using UnityEngine;
using System.Collections.Generic;


public enum CurrentStatus
{
    NoFight,
    Fight,
};

public class FightManager : MonoBehaviour
{
    public static FightManager instance;
    
    public CurrentStatus status;

    public List<Unit> Fights_units = new List<Unit>();




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
}
