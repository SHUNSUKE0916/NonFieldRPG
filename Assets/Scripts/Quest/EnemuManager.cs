using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

//敵（ステータス/クリックの検出）を管理する
public class EnemuManager : MonoBehaviour
{
    public int hp;
    public int at;
    public new string name;
    [SerializeField] GameObject hitEffect;

    //関数登録
    Action tapaction; //クリックされた時に実行したい関数（外部から設定したい）


    //攻撃関数
    public int Attack(Playeranager player)
    {
       int damage =  player.Damage(at);
        return damage;
    }

    //ダメージを受ける関数
    public int Damage(int damage)
    {
        Instantiate(hitEffect, this.transform/*親要素の指定*/, false);
        transform.DOShakePosition(0.3f/*何秒*/, 0.5f/*強さ*/, 20/*振動回数*/, 0/*ランダム*/, false, true );
;        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
        }
        return damage;
    }

    //tapactionに関数を登録する関数をつくる
    public void AddEventListenerOnTap(Action action)
    {
        tapaction = action;
    }


    public void OnClick()
    {
        Debug.Log("Clicked");
        tapaction();
    }
}
