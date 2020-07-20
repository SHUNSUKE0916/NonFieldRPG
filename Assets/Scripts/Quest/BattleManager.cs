using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//PlayerとEnemyの対戦の管理
public class BattleManager : MonoBehaviour
{
    [SerializeField] PlayerUIManager PlayerUI;
    [SerializeField] EnemyUIManager enemyUI;
    [SerializeField] Playeranager player;
    [SerializeField] EnemuManager enemy;
    [SerializeField] QuestManager questManager;
    [SerializeField] Transform cameraObj;

    private void Start()
    {
        enemyUI.gameObject.SetActive(false);
        //StartCoroutine(SampleCal());
        PlayerUI.SetUpUI(player);
    }

    //サンプルコルーチン
    /*IEnumerator SampleCal()
    {
        Debug.Log("コルーチン開始");
        yield return new WaitForSeconds(2f);//何秒待機するか
        Debug.Log("2秒経過");
             
    }*/

    //初期設定
    public void SetUp(EnemuManager enemyManager)
    {
        SoundManager.instance.PlayBGM("Battle");
        enemyUI.gameObject.SetActive(true);
        enemy = enemyManager;
        enemyUI.SetUpUI(enemy);
        PlayerUI.SetUpUI(player);

        enemy.AddEventListenerOnTap(PlayerAttack);

        //enemy.transform.DOMove(new Vector3(0, 10, 0), 5f);
        //5秒かけて０、１０、０に移動させなさい
    }

    void PlayerAttack()
    {
        StopAllCoroutines();//連打するといけないので念のため入れるやつ
        
        SoundManager.instance.PlaySE(1);
        Debug.Log("アタック音");
        //PlayerがEnemyに攻撃
        int damage = player.Attack(enemy);
        enemyUI.UpdateUI(enemy);
        DialogTextManager.instance.SetScenarios(new string[]
        { "プレイヤーのこうげき！！\nモンスターに"+damage+"ダメージをあたえた！" });
        if (enemy.hp <= 0)
        {
            StartCoroutine(EndBattle());
        }
        else
        {
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(2f);
       
      
        SoundManager.instance.PlaySE(1);
        cameraObj.DOShakePosition(0.3f/*何秒*/, 0.5f/*強さ*/, 20/*振動回数*/, 0/*ランダム*/, false, true);
        //EnemyがPlayerを攻撃
        int damege = enemy.Attack(player);
        PlayerUI.UpdateUI(player);

        
        if (player.hp <= 0)
        {
            //プレイヤーがやられてしまった場合
            DialogTextManager.instance.SetScenarios(new string[] { "やられてしまった...。\n街にもどろう...。" });
            yield return new WaitForSeconds(2f);
            questManager.PlayerDown();
            

        }
        else if (player.hp <= 10)
        {
            DialogTextManager.instance.SetScenarios(new string[] { "モンスターのこうげき！！\nHPが少なくなってきた...。" });
        }
        else
        {
            DialogTextManager.instance.SetScenarios(new string[]
            { "モンスターのこうげき！！\nプレイヤーは"+damege+"ダメージをうけた" });
        }

       


    }

    IEnumerator EndBattle()
    {
        yield return new WaitForSeconds(1f);


        enemyUI.gameObject.SetActive(false);
        Destroy(enemy.gameObject);
        SoundManager.instance.PlayBGM("Quest");
        questManager.EndBattle();
        if(player.hp <= 10)
        {
            DialogTextManager.instance.SetScenarios(new string[] { "なんとかモンスターをたおせたようだ...。" });
        }
        else
        {
            DialogTextManager.instance.SetScenarios(new string[] { "モンスターをたおした！！" });
        }
    }

}
