using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private GameObject Enemy;
    [SerializeField] private float spawnRate;
    [SerializeField] private float horizontalDistance;
    [SerializeField] private bool noSpawn = false;
    private float timer = 0;
    public GroundScript ground;
    
    // Start is called before the first frame update
    void Start()
    {
        ground = GameObject.FindGameObjectWithTag("Ground").GetComponent<GroundScript>();
        spawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        } else if (!noSpawn) 
        {
            spawnEnemy();
            timer = 0;
        }

        if (!ground.playerIsAlive)
        {
            noSpawn = true;
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < enemies.Length; i++)
            {
                Destroy(enemies[i]);
            }
        }
    }

    private void spawnEnemy()
    {
        float furthestLeft = transform.position.x - horizontalDistance;
        float furthestRight = transform.position.x + horizontalDistance;


        Instantiate(Enemy, new Vector3(Random.Range(furthestLeft, furthestRight), transform.position.y, 0), transform.rotation);
    }

}
