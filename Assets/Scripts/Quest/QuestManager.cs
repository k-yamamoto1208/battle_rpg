using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

//クエスト全体を管理
public class QuestManager : MonoBehaviour
{
    public StageUIManager stageUI;
    public GameObject enemyPrefab;
    public BattleManager battleManager;
    public SceneTransitionManager sceneTransitionManager;
    public GameObject questBG;

    //的に遭遇するテーブル: 1なら遭遇しない、0で遭遇
    int[] encountTable = { -1, -1, 0, -1, 0, -1 };

    int currentStage = 0;
    private void Start()
    {
        stageUI.UpdateUI(currentStage);
        DialogTextManager.instance.SetScenarios(new string[] { "洞窟についた。" });
    }


    public void OnNextButton()
    {
        SoundManager.instance.PlaySE(0);
        stageUI.ButtonsState(false);
        StartCoroutine(Searching());
    }

    IEnumerator Searching()
    {
        DialogTextManager.instance.SetScenarios(new string[] { "探索中..." });
        //背景を動かす
        questBG.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 2f) //2秒かけてズーム
            .OnComplete(() => questBG.transform.localScale = new Vector3(1, 1, 1)); //元に戻す
        //フェードアウト
        SpriteRenderer questBGSpriteRenderer = questBG.GetComponent<SpriteRenderer>();
        questBGSpriteRenderer.DOFade(0, 2f)
            .OnComplete(() => questBGSpriteRenderer.DOFade(1, 0));

        //背景を動かしている間の処理を遅延させる
        yield return new WaitForSeconds(2f);
       
        currentStage++;
        //進行度をUIに反映
        stageUI.UpdateUI(currentStage);

        if (encountTable.Length <= currentStage)
        {
            QuestClear();
        }
        else if (encountTable[currentStage] == 0)
        {
            EncountEnemy();
        }
        else
        {
            stageUI.ButtonsState(true);
        }
    }

    public void OnToTownButton()
    {
        SoundManager.instance.PlaySE(0);
    }

    void EncountEnemy()
    {
        DialogTextManager.instance.SetScenarios(new string[] { "モンスターに遭遇した！！" });
        stageUI.ButtonsState(false);
        GameObject enemyObj = Instantiate(enemyPrefab);
        EnemyManager enemyManager = enemyObj.GetComponent<EnemyManager>();
        battleManager.SetUp(enemyManager);
    }

    public void EndBattle()
    {
        stageUI.ButtonsState(true);
    }

    void QuestClear()
    {
        DialogTextManager.instance.SetScenarios(new string[] { "宝箱を手に入れた！！" });
        SoundManager.instance.StopBGM();
        SoundManager.instance.PlaySE(2);

        //クエストクリア！って表示する
        //街に戻るボタンのみ表示する
        stageUI.ShowCleartext();

        //sceneTransitionManager.LoadTo("TownScene");
    }
}
