using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject G1;
    public GameObject G2;
    public GameObject G3;
    public GameObject G4;

    public GameObject GameStart;
    public GameObject ControlSet;
    public GameObject SettingSet;
    public GameObject AudioSet;
    public GameObject Unit;
    public GameObject Result;

    public AudioSource bgmPlayer;
    public AudioSource[] sfxPlayer;
    public AudioClip[] sfxClip;

    public enum sfx
    {
        Fist, Sword, Shoot, Tower, ZombieAttack, BossAttack, LastBossAttack,
        SpawnLastBoss, SpawnZombie, Win, Lose, Buy, StartGameAudio
    };
    int sfxCursor;

    public int Cost;
    public int MaxCost;
    public Text SC;
    float delay = 1.8f;
    bool start = false;

    void Start()
    {
        bgmPlayer.Play();
    }

    void Update()
    {

        if (start == true)
        {
            delay -= Time.deltaTime;

            if (Cost < MaxCost)
            {
                if (delay <= 0)
                {
                    Cost = Cost + 1;
                    delay = 1.8f;
                }
            }

            SC.text = Cost.ToString() + "G";
        }
    }


    public void Click1G()
    {
        if (Cost >= 1)
        {
            Instantiate(G1);
            Cost = Cost - 1;
            SfxPlay(sfx.Buy);
        }

    }

    public void Click2G()
    {
        if (Cost >= 2)
        {
            Instantiate(G2);
            Cost = Cost - 2;
            SfxPlay(sfx.Buy);
        }
    }

    public void Click3G()
    {
        if (Cost >= 3)
        {
            Instantiate(G3);
            Cost = Cost - 3;
            SfxPlay(sfx.Buy);
        }
    }

    public void Click4G()
    {
        if (Cost >= 4)
        {
            Instantiate(G4);
            Cost = Cost - 4;
            SfxPlay(sfx.Buy);
        }
    }



    public void gameStartBtn()
    {
        GameStart.gameObject.SetActive(false);
        Unit.gameObject.SetActive(true);
        ControlSet.gameObject.SetActive(true);
        start = true;
        Time.timeScale = 1f;
        SfxPlay(sfx.StartGameAudio);

    }

    public void Setting()//설정버튼
    {
        Time.timeScale = 0f;
        ControlSet.gameObject.SetActive(false);
        SettingSet.gameObject.SetActive(true);
    }

    public void Resume()//계속하기
    {
        Time.timeScale = 1f;
        ControlSet.gameObject.SetActive(true);
        SettingSet.gameObject.SetActive(false);
    }

    public void Decibel()//오디오설정
    {
        SettingSet.gameObject.SetActive(false);
        AudioSet.gameObject.SetActive(true);
    }

    public void bgmVolume(float volume)//오디오설정 내부 - BGM
    {
        bgmPlayer.volume = volume;
    }

    public void BacktoSetting()//오디오에서 설정으로
    {
        SettingSet.gameObject.SetActive(true);
        AudioSet.gameObject.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene("Forest");
    }

    public void escape()//종료하기
    {
        Application.Quit();
    }

    public void SfxPlay(sfx type)
    {
        switch (type)
        {
            case sfx.Buy:
                sfxPlayer[sfxCursor].clip = sfxClip[Random.Range(0, 3)];
                break;

            case sfx.StartGameAudio:
                sfxPlayer[sfxCursor].clip = sfxClip[3];
                break;
            case sfx.Sword:
                sfxPlayer[sfxCursor].clip = sfxClip[Random.Range(4, 7)];
                break;
            case sfx.Lose:
                sfxPlayer[sfxCursor].clip = sfxClip[7];
                bgmPlayer.Stop();
                break;
            case sfx.Win:
                sfxPlayer[sfxCursor].clip = sfxClip[8];
                bgmPlayer.Stop();
                break;
            case sfx.SpawnLastBoss:
                bgmPlayer.clip = sfxClip[9];
                bgmPlayer.Play();
                break;

        }

        sfxPlayer[sfxCursor].Play();
        sfxCursor = (sfxCursor + 1) % sfxPlayer.Length;
    }

}
