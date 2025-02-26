﻿using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

public class UIMainBattle : MonoBehaviour
{
    public Text KillCount;
    public Slider HealthBar;

    public Slider ExpBar;
    public Text Level;
    public List<Text> BoomerangCDs;


    // Start is called before the first frame update
    IEnumerator Start()
    {
        EventManager.Instance.Subscribe("SetKillCount", UpdateKillCount);
        yield return null;
        EventManager.Instance.Subscribe("SetHealth", UpdateHealthBar);
        yield return null;
        EventManager.Instance.Subscribe("UpdateExperience", UpdateExpBar);
        yield return null;

        BoomerangCDs.AddRange(transform.Find("BoomerangCD").GetComponentsInChildren<Text>());
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBoomerangCD();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale != 0)
            {
                UIManager.Instance.Show<UISystemConfig>();
                GameManager.Instance.PauseGame();
            }
            //在UISystemConfig中调用Resume方法，否则会在波次结束后的暂停时间中继续游戏
        }
    }

    public void OnDestroy()
    {
        EventManager.Instance.Unsubscribe("SetKillCount", UpdateKillCount);
        EventManager.Instance.Unsubscribe("SetHealth", UpdateHealthBar);
        EventManager.Instance.Unsubscribe("UpdateExperience", UpdateExpBar);
    }
    public void UpdateKillCount(object[] param)
    {
        if(KillCount!=null)
            KillCount.text = param[0].ToString();
    }
    public void UpdateHealthBar(object[] param)
    {
        if(HealthBar!=null)
        {
            HealthBar.value = Player.Instance.Health / Player.Instance.MaxHealth;
        }
    }
    public void UpdateExpBar(object[] param)
    {
        if (ExpBar != null && Level != null)
        {
            if((int)param[0]>=15) 
                Level.text = string.Format("Level: {0}", "Max");
            else Level.text = string.Format("Level: {0}", param[0].ToString());
            ExpBar.value = (float)param[1] / Player.Instance.NextLevelExpArray[Player.Instance.Level];
        }

    }

    public void UpdateBoomerangCD()
    {
        for(int i = 0; i < BoomerangCDs.Count; i++)
        {
            if (i > Player.Instance.Boomerangs.Count - 1) BoomerangCDs[i].text = "OK";
            else
            {
                BoomerangCDs[i].text = ((int)Player.Instance.Boomerangs[i].LifeCycle).ToString();
            }
        }
    }
}
