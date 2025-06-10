using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// 한 스테이지의 정보를 담는 클래스
[System.Serializable]
public class StageInfo
{
    public int stageKey;// 스테이지 고유 키
    public WaveData[] waves;// 해당 스테이지의 웨이브 데이터

    public StageInfo(int stageKey, WaveData[] waves)
    {
        this.stageKey = stageKey;
        this.waves = waves;
    }
}


// 한 웨이브의 정보를 담는 클래스
[System.Serializable]
public class WaveData
{
    public MonsterSpawnData[] monsters;// 몬스터 생성 정보 배열
    public bool hasBoss;// 보스 존재 여부
    public string bossType;// 보스 타입 (없으면 빈 문자열)


    public WaveData(MonsterSpawnData[] monsters, bool hasBoss, string bossType)
    {
        this.monsters = monsters;
        this.hasBoss = hasBoss;
        this.bossType = bossType;
    }
}

[System.Serializable]
public class MonsterSpawnData
{
    public string monsterType;// 몬스터 타입 (예: Goblin)
    public int spawnCount;// 생성할 몬스터 개수

    public MonsterSpawnData(string monsterType, int spawnCount)
    {
        this.monsterType = monsterType;
        this.spawnCount = spawnCount;
    }
}

// 스테이지와 웨이브 데이터를 저장하는 정적 클래스
public static class StageData
{
    public static readonly StageInfo[] Stages = new StageInfo[]
    {
        // 스테이지 0 정보
        new StageInfo(0, new WaveData[]
        {
            // 웨이브 1: Goblin 1마리, 보스 없음
            new WaveData(new MonsterSpawnData[]
            {
                new MonsterSpawnData("Goblin", 1),
            }
            ,false,""),

            new WaveData(new MonsterSpawnData[]
            {
                new MonsterSpawnData("Goblin", 3),
            }
            ,false,""),

            new WaveData(new MonsterSpawnData[]
            {
                new MonsterSpawnData("Goblin", 2),
                new MonsterSpawnData("Goblin 1", 2),
                new MonsterSpawnData("MaskedDoc", 2),
            }
            ,true,"Orc_Shaman"),
        }
        ),


        new StageInfo(1, new WaveData[]
        {
            new WaveData(new MonsterSpawnData[]
            {
                new MonsterSpawnData("Goblin", 5),
            }
            ,false,""),

            new WaveData(new MonsterSpawnData[]
            {
                new MonsterSpawnData("Goblin", 7),
            }
            ,false,""),

            new WaveData(new MonsterSpawnData[]
            {
                new MonsterSpawnData("Goblin", 3),
                new MonsterSpawnData("Goblin 1", 3),
                new MonsterSpawnData("MaskedDoc", 3),
            }
            ,true,"Chort"),
        }
        ),
    };
}
