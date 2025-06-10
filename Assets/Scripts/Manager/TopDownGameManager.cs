using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerController player { get; private set; }// 플레이어 컨트롤러 (읽기 전용 프로퍼티)
    private ResourceController _playerResourceController;


    [SerializeField] private int currentStageIndex = 0;
    [SerializeField] private int currentWaveIndex = 0;// 현재 웨이브 번호

    private EnemyManager enemyManager;// 적 생성 및 관리하는 매니저

    private UIManager uiManager;
    public static bool isFirstLoading = true;

    private CameraShake cameraShake;

    private StageInstance currentStageInstance;

    private void Awake()
    {
        Instance = this;

        player = FindObjectOfType<PlayerController>();
        player.Init(this);


        uiManager = FindObjectOfType<UIManager>();

        enemyManager = GetComponentInChildren<EnemyManager>();
        enemyManager.Init(this);

        _playerResourceController = player.GetComponent<ResourceController>();
        _playerResourceController.RemoveHealthChangeEvent(uiManager.ChangePlayerHP);
        _playerResourceController.AddHealthChangeEvent(uiManager.ChangePlayerHP);

        cameraShake = FindObjectOfType<CameraShake>();
        MainCameraShake();
    }


    public void MainCameraShake()
    {
        cameraShake.ShakeCamera(1, 1, 1);
    }

    private void Start()
    {
        if (!isFirstLoading)
        {
            StartGame();
        }
        else
        {
            isFirstLoading = false;
        }
    }
    public void StartGame()
    {
        uiManager.SetPlayGame();
        //StartNextWave();
        //StartStage();
        LoadOrStartNewStage();

    }

    void StartNextWave()
    {
        currentWaveIndex += 1;// 웨이브 인덱스 증가
        // 5웨이브마다 난이도 증가 (예: 1~4 → 레벨 1, 5~9 → 레벨 2 ...)
        enemyManager.StartWave(1 +  currentWaveIndex / 5);
        uiManager.ChangeWave(currentWaveIndex);
    }

    public void EndOfWave()
    {
        //StartNextWave();
        StartNextWaveInStage();
    }

    public void GameOver()
    {
        enemyManager.StopWave();
        uiManager.SetGameOver();
        StageSaveManager.ClearSavedStage();
    }


    ///stage 설정
    
    private void LoadOrStartNewStage()
    {
        StageInstance savedInstance = StageSaveManager.LoadStageInstance();

        if (savedInstance != null)
        {
            currentStageInstance = savedInstance;
        }
        else
        {
            currentStageInstance = new StageInstance(0, 0);
        }
        StartStage(currentStageInstance);

    }
    public void StartStage(StageInstance stageInstance)
    {
        currentStageIndex = stageInstance.stageKey;
        currentWaveIndex = stageInstance.currentWave;

        StageInfo stageInfo = GetStageInfo(stageInstance.stageKey);

        if(stageInfo == null)
        {
            Debug.Log("스테이지 정보가 없습니다.");
            StageSaveManager.ClearSavedStage();
            currentStageInstance = null;
            return;
        }

        stageInstance.SetStageInfo(stageInfo);

        uiManager.ChangeWave(currentStageIndex + 1);
        enemyManager.StartStage(currentStageInstance);
        StageSaveManager.SaveStageInstance(currentStageInstance);
    }

    public void StartNextWaveInStage()
    {
        //StageInfo stageInfo = GetStageInfo(currentWaveIndex);
        //if(stageInfo.waves.Length - 1 > currentWaveIndex)
        if(currentStageInstance.CheckEndOfWave())
        {
            currentStageInstance.currentWave++;
            StartStage(currentStageInstance);// 다음 웨이브 시작
        }
        else
        {
            CompleteStage();// 모든 웨이브 완료 시 스테이지 완료 처리
        }
    }

    public void CompleteStage()
    {
        StageSaveManager.ClearSavedStage();

        if(currentStageInstance == null) return;

        currentStageInstance.stageKey += 1;
        currentStageInstance.currentWave = 0;
        StartStage(currentStageInstance);
    }



    private StageInfo GetStageInfo(int stageKey)
    {
        foreach(var stage in StageData.Stages)
        {
            if(stage.stageKey == stageKey) return stage;
        }
        return null;// 해당 스테이지 정보가 없으면 null 반환
    }
}
