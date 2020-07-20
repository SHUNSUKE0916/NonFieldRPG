using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playeranager : MonoBehaviour
{
    public int hp;
    public int at;

    //攻撃関数
    public int Attack(EnemuManager enemy)
    {
        int damage =  enemy.Damage(at);
        return damage;
    }

    //ダメージを受ける関数
    public int Damage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
        }
        return damage;
    }
}
