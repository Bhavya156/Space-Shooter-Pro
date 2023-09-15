using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    private bool _isPlayerDead = false;

    [SerializeField]
    private GameObject[] powerups;

    private Player _player;

  

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3f);
        while (_isPlayerDead == false)
        {
            Vector3 posToSpawnEnemy = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawnEnemy, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(10f);
        while (_isPlayerDead == false)
        {
            Vector3 postoSpawnPowerup = new Vector3(Random.Range(-8f, 8f), 7, 0);
            int randomPowerups = Random.Range(0, 3);
            Instantiate(powerups[randomPowerups], postoSpawnPowerup, Quaternion.identity);
            if (_player.isThereAmmo == false)
            {
                Instantiate(powerups[3], postoSpawnPowerup, Quaternion.identity);
            }
            yield return new WaitForSeconds(Random.Range(20,26));
        }
    }

    public void OnPlayerDead()
    {
        _isPlayerDead = true;
    }
}
