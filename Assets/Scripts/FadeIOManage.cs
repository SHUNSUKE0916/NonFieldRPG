using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FadeIOManage : MonoBehaviour
{
    public float fadeTime = 1f;

    //シングルトン化＝＞シーン間のオブジェクトの共有

    public static FadeIOManage instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] CanvasGroup canvasGroup;

    
    public void FadeOut()
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.DOFade(1, fadeTime)
            .OnComplete(() => canvasGroup.blocksRaycasts = false);
    }

    public void FadeIn()
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.DOFade(0, fadeTime)
            .OnComplete(() => canvasGroup.blocksRaycasts = false);
    }

    public void FadeOutToIn(TweenCallback action/*actionとして何が行われるのかは
                                                 scenetransitionの
        void Load(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        SoundManager.instance.PlayBGM(sceneName);
    }*/)
    {
        canvasGroup.blocksRaycasts = true;
        //fadeoutが終わったらfadeinを実行する
        canvasGroup.DOFade(1, fadeTime)
            .OnComplete(() => {action(); FadeIn(); });
    }
}
