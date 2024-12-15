using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class GameManager : MonoBehaviour
{ 
    public static GameManager instance;

    public float life;
    public float maxLife;
    public int nextSpawnPoint;
    public GameData gameData;
    private int ranura;



    private void Awake()
    {
        if (instance == null)
        {

            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void LoadData ()
    {
        
    }

   
}

