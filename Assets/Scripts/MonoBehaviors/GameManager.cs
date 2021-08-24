using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static UnityAction m_GameEnd;
    public static UnityAction m_Death;
    public static UnityAction m_Respawn;

    public bool isLoadNewScene = false;

    [SerializeField] private string endSceneName = "Main Menu";
    [SerializeField] private string deathSceneName = "Main Menu";
    [SerializeField] private Room start;

    private Player player;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        m_Death += Death;
        m_GameEnd += GameEnd;
        m_Respawn += Respawn;

        player = FindObjectOfType<Player>();
    }

    private void GameEnd()
    {
        isLoadNewScene = true;
        SceneManager.LoadScene(endSceneName);
    }

    private void Death()
    {
        isLoadNewScene = true;
        StartCoroutine(nameof(DeathSceneLoad));
    }

    private void Respawn()
    {
        player.transform.position = new Vector3(start.transform.position.x, start.transform.position.y, 0);
    }

    IEnumerator DeathSceneLoad()
    {
        for (int i = 0; i < 20; i++)
        {
            if (i >= 19) m_Respawn();
            yield return new WaitForSeconds(0.01333f);
        }
    }

    private void OnDestroy()
    {
        m_Death = null;
        m_GameEnd = null;
    }
}
