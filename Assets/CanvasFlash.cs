using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFlash : MonoBehaviour
{

    #region Singleton Implementation
    private static CanvasFlash m_instance;

    public static CanvasFlash Instance
    {
        get
        {
            if (m_instance == null)
            {
                Debug.Log("No instance of GameManager in the scene!");
            }
            return m_instance;
        }
    }

    private void Awake()
    {
        if (m_instance != null)
        {
            Debug.LogWarning("Already an instance of GameManager in here- destroying this one.");
            Destroy(this);
        }

        m_instance = this;
    }
    #endregion

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Flash()
    {
        anim.SetTrigger("Flash");
    }
}
