﻿#pragma warning disable 0168
#pragma warning disable 0219
#pragma warning disable 0414

using UnityEngine;

using UnityEngine.Networking;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameManager : NetworkBehaviour
{
    public static GameManager singleton = null;
    public static List<PlayerManager> m_players = new List<PlayerManager>();
    public bool quit = false;
    private WaitForSeconds m_Wait;
    private int activePlayer;


    public static void AddPlayer(GameObject player, string name)
    {
        PlayerManager pm = new PlayerManager();
        pm.m_Instance = player;
        pm.m_Name = name;        
        pm.Setup();
        m_players.Add(pm);        
    }

    public void RemovePlayer(GameObject p)
    {
        PlayerManager toRemove = null;
        foreach (var tmp in m_players)
        {
            if (tmp.m_Instance == p)
            {
                toRemove = tmp;
                break;
            }
        }

        if (toRemove != null)
            m_players.Remove(toRemove);
    }

    void Awake()
    {
        singleton = this;
    }

    [ServerCallback]
    private void Start()
    {
        //print("start loop");
        m_Wait = new WaitForSeconds(1);
        StartCoroutine(GameLoop());
    }
    [SyncVar]
    public bool hasStarted = false;
    IEnumerator GameLoop()
    {
        yield return StartCoroutine(GameStart());
        yield return StartCoroutine(PlayerTurn());
       
        while(!quit)
        {
            //if (m_players.Count < 2) quit = true;
            //print("loop");
            yield return m_Wait;
        }
        Prototype.NetworkLobby.LobbyManager.s_Singleton.ServerReturnToLobby();
    }
    public PlayerManager activePlayerManager;
    IEnumerator GameStart()
    {
        print("Game Started");
        activePlayer = 0;
        activePlayerManager = m_players[activePlayer];        
        if (m_players.Count > 1)
        {
            Rect Left = new Rect(0, 0, .5f, 1);
            Rect Right = new Rect(.5f, 0, .5f, 1);
            m_players[0].m_PlayerCamera.rect = Left;
            m_players[0].m_PlayerUICamera.rect = Left;
            m_players[1].m_PlayerCamera.rect = Right;
            m_players[1].m_PlayerUICamera.rect = Right;
            hasStarted = true;
        }
        yield return null;
    }

    IEnumerator PlayerTurn()
    {
        activePlayerManager.Start();
        while (m_players[activePlayer].IsTakingTurn)
        {
            Debug.Log("Current Player is " + m_players[activePlayer].m_Name);
            yield return null;            
        }
        activePlayer += 1;
        if (activePlayer >= m_players.Count)
            activePlayer = 0;

        activePlayerManager = m_players[activePlayer];

        yield return null;
        yield return StartCoroutine(PlayerTurn());        
    }

   
}


