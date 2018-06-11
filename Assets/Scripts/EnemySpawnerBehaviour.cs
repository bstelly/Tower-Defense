using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TowerDefense
{
    public class EnemySpawnerBehaviour : MonoBehaviour
    {

        float spawnCountDown = 1.0f;
        float spawnCountDownRemaining = 5.0f;
        bool didSpawn = false;

        [System.Serializable]
        public class WaveComponent : MonoBehaviour
        {
            public GameObject enemyPrefab;
            public int num;

            [System.NonSerialized]
            public int spawned = 0;
        }

        public WaveComponent[] waveComps;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            spawnCountDownRemaining -= Time.deltaTime;
            if (spawnCountDownRemaining < 0)
            {
                spawnCountDownRemaining = spawnCountDown;

            }

            didSpawn = false;

            foreach (var wc in waveComps)
            {
                if (wc.spawned < wc.num)
                {
                    wc.spawned++;
                    Instantiate(wc.enemyPrefab, this.transform.position, this.transform.rotation);
                    didSpawn = true;
                    break;
                }
            }

            if (didSpawn == false)
            {
                if (transform.parent.childCount > 1)
                {
                    transform.parent.GetChild(1).gameObject.SetActive(true);
                }
                Destroy(gameObject);
            }
        }
    }
}
