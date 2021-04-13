using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//敵を管理する（ステータス、クリック演出）
public class EnemyManager : MonoBehaviour
{
    //関数登録
    Action tapAction; //クリックされたときに実行したい関数（外部から設定したい）

    public new string name;
    public int hp;
    public int at;
    public GameObject attackEffect;

    public void OnTap()
    {
        tapAction();
    }

    //tapActionに関数を登録する関数を作る
    public void AddEventListnerOnTap(Action action)
    {
        tapAction += action;
    }

    //攻撃する
    public int Attack(PlayerManager player)
    {
        int damage = player.Damage(at);
        return damage;
    }

    //ダメージを受ける
    public int Damage(int damage)
    {
        Instantiate(attackEffect, this.transform, false);

        //振動させる
        transform.DOShakePosition(0.3f, 0.5f, 20, 0, false, true);

        hp -= damage;
        if(hp <= 0)
        {
            hp = 0;
        }

        return damage;
    }
}
