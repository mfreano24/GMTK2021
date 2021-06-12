using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public float climbRate = 100.0f;
    public Text distanceText;
    [HideInInspector] public float distanceClimbed = 0;
    bool gameOver = false;
    bool gamePlaying = false;

    bool gameWon = false;

    float DIST_EARTH_MOON = 384472282f;

    private void Start()
    {
        //default game logic, not the tutorial level probably
        StartCoroutine(GameLoop());

    }

    private void Update()
    {
        if (gamePlaying)
        {
            distanceClimbed += 100.0f * Time.deltaTime;

            if(distanceClimbed >= DIST_EARTH_MOON)
            {
                gameWon = true;
                gameOver = true;
            }

            distanceText.text = Mathf.FloorToInt(DIST_EARTH_MOON - distanceClimbed).ToString();
        }
    }

    IEnumerator GameLoop()
    {
        //TODO: GameStart(), GameDuring(), GameEnd()

        yield return GameStart();
        Debug.Log("Game start is finished");
        yield return GameDuring();
        Debug.Log("Game During is finished");
        yield return GameEnd();
    }

    IEnumerator GameStart()
    {
        //ready, go text or something
        yield return new WaitForSeconds(3.0f);
    }

    IEnumerator GameDuring()
    {
        BoulderManager.Instance.StartBoulders();
        gamePlaying = true;
        //primary game loop should be happening here, basically waituntil the player dies
        yield return new WaitUntil(() => gameOver);
        gamePlaying = false;
        BoulderManager.Instance.StopBoulders();
    }

    IEnumerator GameEnd()
    {
        //TODO: you lose screen
        //maybe an actual screen with a "Restart" and "Menu" button?

        if (gameWon)
        {
            Debug.Log("You made it back to the stars!");
        }
        else
        {

        }
        yield return new WaitForSeconds(5.0f);
    }


    public void PlayerDeath()
    {
        Debug.Log("You fell (and died)!");
        gameOver = true;
    }

    
}
