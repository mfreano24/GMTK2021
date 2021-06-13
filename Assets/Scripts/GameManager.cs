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

    public Text finalDistanceText;
    public Text newBestText;
    public GameObject gameOverScreen;

    public GameObject ReadyVisual;
    public GameObject ClimbVisual;
    

    [HideInInspector] public float distanceClimbed = 0;
    bool gameOver = false;
    bool gamePlaying = false;

    bool gameWon = false;

    float DIST_EARTH_MOON = 384472282f;

    private void Start()
    {
        //default game logic, not the tutorial level probably
        gameOverScreen.SetActive(false);
        StartCoroutine(GameLoop());

    }

    private void Update()
    {
        if (gamePlaying)
        {
            distanceClimbed += climbRate * Time.deltaTime;

            if (distanceClimbed >= DIST_EARTH_MOON)
            {
                gameWon = true;
                gameOver = true;
            }

            distanceText.text = string.Format("{0:n0}", DIST_EARTH_MOON - distanceClimbed) + "m";
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
        yield return new WaitForSeconds(2.0f);
        CanvasFlash.Instance.Flash();
        ReadyVisual.SetActive(false);
        ClimbVisual.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        ClimbVisual.GetComponent<Animator>().SetTrigger("SlideOut");
        yield return new WaitForSeconds(1.0f);

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
        yield return new WaitForSeconds(2.0f);
        //move the game over screen in (animate if time)
        gameOverScreen.SetActive(true);
        gameOverScreen.GetComponent<Animator>().SetTrigger("GameEnded"); //move in
        finalDistanceText.text = string.Format("{0:n}", distanceClimbed) + "m";
        if (distanceClimbed > PlayerPrefs.GetFloat("BestDistance", -100f))
        {
            newBestText.text = "NEW PERSONAL BEST!";
            PlayerPrefs.SetFloat("BestDistance", distanceClimbed);
        }
        else
        {
            newBestText.text = "BEST: " + PlayerPrefs.GetFloat("BestDistance", -100f) + "m";
        }

       
        
    }


    public void PlayerDeath()
    {
        Debug.Log("You fell (and died)!");
        gameOver = true;
    }

    
}
