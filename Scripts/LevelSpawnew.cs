using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawnew : MonoBehaviour
{
    public GameObject[] levels;

    private float levelDistance = 50f;//каждый лвл стоко)
    private float currentLevelX = 0f;

    private float incrementSpawnerX = 125f;

    private int spawnCount = 5;

    void Start()
    {
        InvokeRepeating(nameof(SpawnLevels),0f,10f);
    }

   void SpawnLevels()
    {
        int rand = 0;

        for (int i = 0; i < spawnCount; i++)
        {
            rand = Random.Range(0, levels.Length);

            GameObject go = Instantiate(levels[rand]);

            Vector3 levelTemp = Vector3.zero;
            levelTemp.x = currentLevelX;

            go.transform.position = levelTemp;

            currentLevelX += levelDistance;
            
        }
    }
}
