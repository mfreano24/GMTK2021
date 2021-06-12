using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton Implementation
    private static GameManager m_instance;

    public static GameManager Instance
    {
        get
        {
            if(m_instance == null)
            {
                Debug.Log("No instance of GameManager in the scene!");
            }
            return m_instance;
        }
    }

    private void Awake()
    {
        if(m_instance != null)
        {
            Debug.LogWarning("Already an instance of GameManager in here- destroying this one.");
            Destroy(this);
        }

        m_instance = this;
    }
    #endregion

    bool gameOver = false;
    private void Start()
    {
        //default game logic, not the tutorial level probably

    }

    IEnumerator GameLoop()
    {
        //TODO: GameStart(), GameDuring(), GameEnd()
        yield return null;
    }

    IEnumerator GameStart()
    {
        //ready, go text or something
        yield return new WaitForSeconds(3.0f);
    }

    IEnumerator GameDuring()
    {
        //primary game loop should be happening here, basically waituntil the player dies
        yield return new WaitUntil(() => gameOver);
    }

    IEnumerator GameEnd()
    {
        //TODO: you lose screen
        yield return null;
    }


    public void PlayerDeath()
    {
        Debug.Log("You fell (and died)!");
    }


    IEnumerator BoulderSpawns()
    {
        yield return null;
    }
    
}
