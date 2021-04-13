using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//StageUIを管理（ステージ数のUIと進行ボタン＆街に戻るボタン）
public class StageUIManager : MonoBehaviour
{
    public Text stageText;
    public GameObject nextButton;
    public GameObject townButton;
    public GameObject stageClearImage;

    private void Start()
    {
        stageClearImage.SetActive(false);
    }

    public void UpdateUI(int currentStage)
    {
        stageText.text = string.Format("ステージ：{0}", currentStage + 1);

    }

    public void ButtonsState(bool isTrue)
    {
        nextButton.SetActive(isTrue);
        townButton.SetActive(isTrue);
    }

    public void ShowCleartext()
    {
        stageClearImage.SetActive(true);
        nextButton.SetActive(false);
        townButton.SetActive(true);
    }
}
