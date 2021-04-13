using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // シングルトン
    // ゲーム内に１つしか存在しないもの（音を管理するものとか）
    // 利用場所：シーン間でのデータ共有
    public static SoundManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    //シングルトン終わり
    public AudioSource audioSourceBGM; //BGMのスピーカー
    public AudioClip[] audioClipsBGM; //BGMの素材(0:Title, 1:Town ,2:Quest, 3:Battle)
    public AudioSource audioSourceSE; //SEのスピーカー
    public AudioClip[] audioClipsSE; //鳴らす素材


    //BGMを止める
    public void StopBGM()
    {
        audioSourceBGM.Stop();
    }

    public void PlayBGM(string sceneName)
    {
        audioSourceBGM.Stop();
        switch (sceneName)
        {
            case "TitleScene":
                audioSourceBGM.clip = audioClipsBGM[0];
                break;
            case "TownScene":
                audioSourceBGM.clip = audioClipsBGM[1];
                break;
            case "QuestScene":
                audioSourceBGM.clip = audioClipsBGM[2];
                break;
            case "BattleScene":
                audioSourceBGM.clip = audioClipsBGM[3];
                break;
        }
        audioSourceBGM.Play();
    }

    public void PlaySE(int index)
    {
        audioSourceSE.PlayOneShot(audioClipsSE[index]); //SEを一度だけ鳴らす
    }
}
