using System.Collections;
using UnityEngine;

public class DeployMissile : MonoBehaviour
{
    public GameObject missilePrefab;
    public float spawnTime = 1f;
    private Vector2 screenBounds;
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(missileWave());
    }
    private void spawnEnemy()
    {
        GameObject a = Instantiate(missilePrefab) as GameObject;
        a.transform.position = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), Random.Range(0, screenBounds.y));
        a.transform.Rotate(Vector3.forward * Random.Range(0, 360));
    }
    IEnumerator missileWave()
    {
        while(true)
        {
            yield return new WaitForSeconds(spawnTime);
            spawnEnemy();
        }
    }
}
