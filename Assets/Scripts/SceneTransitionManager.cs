using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public void LoadTo(string sceneName)//引数のシーンを読みこむ（QuestならQuestシーン、TownならTownシーン）
    {

        FadeIOManage.instance.FadeOutToIn(() => Load(sceneName));

    }

    void Load(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        SoundManager.instance.PlayBGM(sceneName);
    }
}
