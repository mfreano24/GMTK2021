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

    //want to store the current point benchmarks so that we can decrease the timers
    [Header("Point Benchmarks")]
    public int[] pointBenchmarks;
    public float[] climbRates;
    int currIndex = 0;
    bool atMaxDifficulty = false;

    List<GameObject> freeObjects = new List<GameObject>();
    List<GameObject> inUseObjects = new List<GameObject>();

    int dropIndex;

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

        Physics2D.IgnoreLayerCollision(13, 14); //ignore collisions with the boundaries.
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
            WarnForBoulder();
            yield return new WaitForSeconds(warningTime);
            DropBoulder();
            if (!atMaxDifficulty)
            {
                CheckForUpdates();
            }
            
        }
        
    }

    void WarnForBoulder()
    {
        Debug.Log("Warning!");
        dropIndex = UnityEngine.Random.Range(0, 18);
    }

    void DropBoulder()
    {
        //TODO: make special drop events
        //either line of boulders or shooting star
        GameObject boulder = RequestRandomBoulder();
        boulder.SetActive(true);
        boulder.transform.position = spawnPoints[dropIndex].position + new Vector3(0, 0, -3);
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

            if(currIndex == 11)
            {
                atMaxDifficulty = true;
            }
        }
    }


    GameObject RequestRandomBoulder()
    {
        int ind = UnityEngine.Random.Range(0, 18);
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


    
}
