using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawn_Manager : MonoBehaviour
{   
    [SerializeField]
    private GameObject _enemyprefab;
    [SerializeField]
    private GameObject _enemycontainer;
    private bool _stopspawn = false;

    [SerializeField]
    private GameObject [] powerups;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }
    // Update is called once per frame
    void Update()
    {
         
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopspawn==false)
        {
            Vector3 newpos = new Vector3(Random.Range(-8f,8f),7,0);
            GameObject newEnemy = Instantiate(_enemyprefab,newpos,Quaternion.identity);
            newEnemy.transform.parent = _enemycontainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }

   IEnumerator SpawnPowerupRoutine()
    {
        while (_stopspawn==false)
        {
            Vector3 newpos = new Vector3(Random.Range(-8f,8f),7,0);
            int randomcall=Random.Range(0,3);
            Instantiate(powerups[randomcall],newpos,Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3,8));
        }
    }
    
    public void OnPlayerDeath()
    {
        _stopspawn=true;
    }
}
