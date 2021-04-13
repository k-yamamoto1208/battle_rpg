using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//対戦を管理
public class BattleManager : MonoBehaviour
{
    public Transform cameraObj;
    public QuestManager questManager;
    public PlayerUIManager playerUI;
    public EnemyUIManager enemyUI;
    public PlayerManager player;
    EnemyManager enemy;

    //連打防止
    private bool buttonEnabled = true;

    private void Start()
    {
        enemyUI.gameObject.SetActive(false);
    }


    //初期設定
    public void SetUp(EnemyManager enemyManager)
    {
        SoundManager.instance.PlayBGM("BattleScene");

        enemyUI.gameObject.SetActive(true);
        enemy = enemyManager;
        playerUI.SetUpUI(player);
        enemyUI.SetUpUI(enemy);

        enemy.AddEventListnerOnTap(PlayerAttack);
    }

    void PlayerAttack()
    {
        //連打防止
        if (buttonEnabled)
        {
            buttonEnabled = false;
            StartCoroutine(EnableButton());

            SoundManager.instance.PlaySE(1);

            //PlayerがEnemyに攻撃
            int damage = player.Attack(enemy);
            enemyUI.UpdateUI(enemy);
            DialogTextManager.instance.SetScenarios(new string[] {
                "プレイヤーの攻撃\nモンスターに" + damage + "ダメージ！！"
            });

            if (enemy.hp <= 0)
            {
                StartCoroutine(EndBattle());
            }
            else
            {
                //遅延呼び出し
                StartCoroutine(EnemyTurn());
            }
        }
        
    }

    //連打防止の遅延処理
    IEnumerator EnableButton()
    {
        yield return new WaitForSeconds(0.7f);
        buttonEnabled = true;
    }

    IEnumerator EnemyTurn()
    {
        //攻撃遅延
        yield return new WaitForSeconds(1.5f);

        //EnemyがPlayerに攻撃
        SoundManager.instance.PlaySE(1);
        cameraObj.DOShakePosition(0.3f, 0.5f, 20, 0, false, true);
        int damage = enemy.Attack(player);
        DialogTextManager.instance.SetScenarios(new string[] {
            "モンスターの攻撃\nプレイヤーに" + damage + "ダメージ！！" 
        });
        playerUI.UpdateUI(player);
    }

    IEnumerator EndBattle()
    {
        yield return new WaitForSeconds(1f);

        DialogTextManager.instance.SetScenarios(new string[] { "モンスターを倒した！！" });
        Destroy(enemy.gameObject);
        enemyUI.gameObject.SetActive(false);
        questManager.EndBattle();
        SoundManager.instance.PlayBGM("QuestScene");

    }

}
