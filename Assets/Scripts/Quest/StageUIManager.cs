using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//StageUIを管理(ステージ数のUI/進行ボタン/街に戻るボタン)
public class StageUIManager : MonoBehaviour
{
    [SerializeField] Text stageText;
    [SerializeField] GameObject NextButton;
    [SerializeField] GameObject TownButton;
    [SerializeField] GameObject Tresure; //なぜGameObjectなのかというと、objrctの表示、非表示を扱いたいから

    private void Start()
    {
        Tresure.SetActive(false);
    }

    public void UpdateUI(int currentstage)
    {
        stageText.text = string.Format("ステージ：{0}", currentstage + 1);
    }

    public void HideButton()
    {
        NextButton.SetActive(false);
        TownButton.SetActive(false);
    }

    public void ShowButton()
    {
        NextButton.SetActive(true);
        TownButton.SetActive(true);
    }

    public void ShowClearText()
    {
        Tresure.SetActive(true);
        NextButton.SetActive(false);
        TownButton.SetActive(true);
    }
}
