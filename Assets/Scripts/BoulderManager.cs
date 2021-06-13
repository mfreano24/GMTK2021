using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderManager : MonoBehaviour
{
    #region Singleton Implementation
    private static BoulderManager m_instance;

    public static BoulderManager Instance
    {
        get
        {
            if (m_instance == null)
            {
                Debug.Log("No instance of BoulderManager in the scene!");
            }
            return m_instance;
        }
    }
    #endregion
    //Seconds between boulders should start at around 3.5 seconds and max difficulty should be 1
    //warning time should start at around 3 seconds then max difficulty should be 1 second
    // special event chance should start around 5% and then max difficulty should be 40%? maybe?
    [Header("Timers and Difficulty Settings")]
    public float secondsBetweenBoulders;
    public float warningTime;
    public float specialEventChance;

    //Might want to object pool instead of instantiating- also be sure to have 12 or less boulders at once?
    [Header("Boulder Spawns and Prefabs")]
    public Transform[] spawnPoints;
    public GameObject[] boulders;
    public GameObject[] warningSigns;

    //want to store the current point benchmarks so that we can decrease the timers
    [Header("Point Benchmarks")]
    public int[] pointBenchmarks;
    public float[] climbRates;
    int currIndex = 0;
    bool atMaxDifficulty = false;

    List<GameObject> freeObjects = new List<GameObject>();
    List<GameObject> inUseObjects = new List<GameObject>();

    Queue<int> dropIndices;

    Coroutine boulderCoroutine;

    private void Awake()
    {
        if (m_instance != null)
        {
            Debug.LogWarning("Already an instance of BoulderManager in here- destroying this one.");
            Destroy(this);
        }

        m_instance = this;
        freeObjects = new List<GameObject>(boulders);

        foreach(GameObject e in freeObjects)
        {
            e.SetActive(false);
        }

        foreach(GameObject e in warningSigns)
        {
            e.SetActive(false);
        }

        dropIndices = new Queue<int>();
        Physics2D.IgnoreLayerCollision(13, 14); //ignore collisions with the boundaries.
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine(RowOfBouldersEvent());
        }
    }


    public void StartBoulders()
    {
        boulderCoroutine = StartCoroutine(BoulderSpawner());
    }

    public void StopBoulders()
    {
        StopCoroutine(boulderCoroutine);
    }


    IEnumerator BoulderSpawner()
    {
        while (true)
        {
            Debug.Log("Waiting for the next boulder...");
            yield return new WaitForSeconds(secondsBetweenBoulders);
            //this is where we can roll a random int
            int rollHundred = UnityEngine.Random.Range(1, 101);
            if(rollHundred <= specialEventChance)
            {
                //doing a special event
                StartCoroutine(RowOfBouldersEvent());
            }
            else
            {
                WarnForBoulder();
                yield return new WaitForSeconds(warningTime);
                DropBoulder();
                if (!atMaxDifficulty)
                {
                    CheckForUpdates();
                }
            }
            
            
        }
        
    }

    void WarnForBoulder()
    {
        Debug.Log("Warning!");
        int dropIndex = UnityEngine.Random.Range(0, 18);

        warningSigns[dropIndex].SetActive(true);

        dropIndices.Enqueue(dropIndex);


    }

    void DropBoulder()
    {
        //TODO: make special drop events
        //either line of boulders or shooting star
        GameObject boulder = RequestRandomBoulder();
        boulder.SetActive(true);
        int drop = dropIndices.Dequeue();
        boulder.transform.position = spawnPoints[drop].position + new Vector3(0, 0, -3);
        warningSigns[drop].SetActive(false);
    }

    void CheckForUpdates()
    {
        if(GameManager.Instance.distanceClimbed >= pointBenchmarks[currIndex])
        {
            currIndex++;
            Debug.Log("At index " + currIndex + " now. Climb rate is now " + climbRates[currIndex]);
            GameManager.Instance.climbRate = climbRates[currIndex];
            secondsBetweenBoulders -= 0.25f;
            warningTime -= 0.25f;
            specialEventChance += 5.0f;

            if(currIndex == 1)
            {
                AudioManager.Instance.PlayMusic("Gameplay2", true);
            }

            else if (currIndex == 4)
            {
                AudioManager.Instance.PlayMusic("Gameplay3", true);
            }

            else if (currIndex == 7)
            {
                AudioManager.Instance.PlayMusic("Gameplay4", true);
            }

            if (currIndex == 11)
            {
                atMaxDifficulty = true;
            }
        }
    }


    GameObject RequestRandomBoulder()
    {
        int ind = UnityEngine.Random.Range(0, freeObjects.Count);
        Debug.Log("ind of random boulder = " + ind);
        GameObject ret = freeObjects[ind];
        freeObjects.RemoveAt(ind);
        inUseObjects.Add(ret);
        return ret;
    }

    public void ReturnBoulder(GameObject boulder)
    {
        freeObjects.Add(boulder);
        inUseObjects.Remove(boulder);
        boulder.SetActive(false);
    }


    IEnumerator RowOfBouldersEvent()
    {
        int dropIndex = UnityEngine.Random.Range(0, 18);
        for(int i = 0; i < 5; i++)
        {
            warningSigns[(dropIndex + i)%18].SetActive(true);
            dropIndices.Enqueue((dropIndex + i) % 18);
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(3.5f); //always do 3.5 seconds for this

        for (int i = 0; i < 5; i++)
        {
            DropBoulder();
            yield return new WaitForSeconds(0.1f);
        }



    }


    
}
