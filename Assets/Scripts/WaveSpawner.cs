using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Transform ennemyPrefab;
    public Transform spawnPoint;
    public float timeBetweenWave = 5f;
    private float countdown = 3f;
    public Text waveCountdownTimer;

    private int waveIndex = 0;
    void Update()
    {
        if(countdown <=0f){
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWave;
        }

        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountdownTimer.text = string.Format("{0:00.00}", countdown);
    }

    IEnumerator SpawnWave(){

        waveIndex++;
        PlayerStats.rounds++;

        for(int i = 0; i < waveIndex; i++){
            SpawnEnnemy();
            yield return new WaitForSeconds(0.5f);
        } 
    }

    void SpawnEnnemy(){
        Instantiate(ennemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
