using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SceneSingleton<GameManager>
{
    private bool isEnd = false;
    public event Action onEndStage;

    private bool isPaused = false;
    public event Action<bool> onPauseGame;

    private float playTime = 0;
    public Action<int> onUpdatePlayTime;

    private int gainedGold = 0;
    public Action<int> onUpdateGainGold;

    private void Start()
    {
        // UI Scene 불러오기
        SceneManager.LoadScene("GameUI", LoadSceneMode.Additive);
        
        // BGM 바꾸기
        AudioManager.Instance.SetBGM(AudioManager.BgmTrack.Forest);

        // Pool 관련 초기화
        PoolManager.Instance.InitPoolManager();
    }

    private void FixedUpdate() 
    {
        if(isEnd)
        {
            return;
        }

        playTime += Time.fixedDeltaTime;
        onUpdatePlayTime?.Invoke(Mathf.FloorToInt(playTime));
    }

    public void HandlePauseGame()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
            onPauseGame?.Invoke(false);
            AudioManager.Instance.ControlBGM(AudioManager.BgmState.Resume);
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
            onPauseGame?.Invoke(true);
            AudioManager.Instance.ControlBGM(AudioManager.BgmState.Pause);
        }
    }

    public void GainGold(int gold)
    {
        gainedGold += gold;
        onUpdateGainGold?.Invoke(gainedGold);
    }

    public void ClearStage(bool isClear)
    {
        // 게임 중지
        isEnd = true;

        // BGM 스탑
        AudioManager.Instance.ControlBGM(AudioManager.BgmState.Stop);

        // UnitController 정지
        // PlayerController 정지
        onEndStage?.Invoke();
        
        // 클리어 등급 결정
        var stageInfo = GameDataBase.Instance.GetCurrentStageInfo();
        int clearGrade = 0;

        if(isClear)
        {
            Debug.Log(stageInfo.timeLimit1 + " : " + stageInfo.timeLimit2 + " : " + playTime);

            clearGrade = 1;
            if(playTime <= stageInfo.timeLimit1)
            {
                clearGrade = 3;
            }
            else if(playTime <= stageInfo.timeLimit2)
            {
                clearGrade = 2;
            }

            // 스테이지 클리어 정보 저장
            PlayerDataManager.SaveStageClear(GameDataBase.Instance.currentStageId, clearGrade);
        }

        // 골드 획득 저장
        PlayerDataManager.AddCurrency(gainedGold);

        // 결과 팝업
        Observer.Instance.RaiseStageResultPopup(isClear, Mathf.RoundToInt(playTime), gainedGold, clearGrade);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        // 이벤트 해제
        onEndStage = null;
        onPauseGame = null;
        onUpdateGainGold = null;
        onUpdatePlayTime = null;        
    }
}