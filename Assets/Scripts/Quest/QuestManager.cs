using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//クエスト全体を管理
public class QuestManager : MonoBehaviour
{
    [SerializeField] StageUIManager StageUI;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] BattleManager battleManager;
    [SerializeField] SceneTransitionManager SceneTransitionManager;
    [SerializeField] GameObject questBG;


    //的に遭遇するテーブル：-1なら遭遇しない、０なら遭遇
    int[] encountTable = { -1, -1, 0, -1, 0, -1 };

    int NowStage = 0; //現在のステージの進行度

    private void Start()
    {
        StageUI.UpdateUI(NowStage);
        DialogTextManager.instance.SetScenarios(new string[] {"森に辿りついた。"});
        
    }

    IEnumerator Searching()
    {
        DialogTextManager.instance.SetScenarios(new string[] { "探索中..." });

        //デカくする=>元のサイズに戻す
        questBG.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 2f)
            .OnComplete(() => questBG.transform.localScale = new Vector3(1, 1, 1));
        //フェードアウト
        SpriteRenderer questBGSpriteRenderer = questBG.GetComponent<SpriteRenderer>();
        //2秒かけて消える=>0秒かけて１にする
        questBGSpriteRenderer.DOFade(0, 2f)
            .OnComplete(() => questBGSpriteRenderer.DOFade(1, 0));
        //２秒間処理を待機させる
        yield return new WaitForSeconds(2f);

        NowStage++; //進行度増加

        //進行度をUIに反映
        StageUI.UpdateUI(NowStage);

        if (encountTable.Length <= NowStage)
        {
            Debug.Log("Stage Clear!!");
            QuestClear();
        }
        else if (encountTable[NowStage] == 0)
        {
            EncountEnemy();


        }
        else
        {
            StageUI.ShowButton();
        }

    }

    //NextButtonを押した時にステージを増やしていく関数
    public void OnNextButton()
    {
        SoundManager.instance.PlaySE(0);
        StageUI.HideButton();
        StartCoroutine(Searching());
    }

    void EncountEnemy()
    {
        DialogTextManager.instance.SetScenarios(new string[] { "モンスターがあらわれた！！" });
        StageUI.HideButton();
        GameObject enemyObj = Instantiate(enemyPrefab);
        EnemuManager enemy = enemyObj.GetComponent<EnemuManager>();
        battleManager.SetUp(enemy);
    }

    public void EndBattle()
    {
        StageUI.ShowButton();
    }

    void QuestClear()
    {
        DialogTextManager.instance.SetScenarios(new string[] { "宝箱をてにいれた！！ \n街にもどろう！" });

        SoundManager.instance.StopBGM();
        SoundManager.instance.PlaySE(2);

        //クエストクリア！って表示する
        //街に戻るボタンのみ表示する
        StageUI.ShowClearText();


        // SceneTransitionManager.LoadTo("Town");
    }

    public void OnBackTown()
    {
        SoundManager.instance.PlaySE(0);
    }

    public void PlayerDown()
    {
        SceneTransitionManager.LoadTo("Town");
    }
}
