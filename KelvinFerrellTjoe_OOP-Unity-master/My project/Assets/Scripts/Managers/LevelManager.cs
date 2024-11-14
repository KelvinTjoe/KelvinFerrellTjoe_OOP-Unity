using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    public Player player;
    void Awake() {
        if (Instance == null) { Instance = this;  }
        else { Destroy(gameObject); }
        player = FindObjectOfType<Player>();
    }

    public void LoadScene(string sceneName)
    {
       
    }
    
}
