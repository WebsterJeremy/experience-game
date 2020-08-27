using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    #region AccessVariables


    [Header("Area")]
    [SerializeField] private string displayName;
    [SerializeField] private bool entered;

    [System.Serializable]
    public struct Enemies
    {
        public GameObject prefab;
        public Transform spawnPoint;
    }
    public List<Enemies> enemiesToSpawn = new List<Enemies>();

    #endregion
    #region PrivateVariables


    private List<GameObject> enemies = new List<GameObject>();


    #endregion
    #region Initlization


    private void Start()
    {
        entered = false;
    }


    #endregion
    #region Getters & Setters

    public bool Entered { get { return entered; } }

    #endregion
    #region Main


    public void Enter()
    {
        Debug.Log("You entered area "+ this);

        if (!entered)
        {
            entered = true;
            SpawnEnemies();
        }
    }

    private void SpawnEnemies()
    {
        if (enemiesToSpawn.Count <= 0) return;

        foreach (Enemies enemyToSpawn in enemiesToSpawn)
        {
            GameObject enemy = Instantiate(enemyToSpawn.prefab, enemyToSpawn.spawnPoint.position, enemyToSpawn.spawnPoint.rotation);

            enemies.Add(enemy);
        }
    }

    public void EnemyKilled(GameObject enemy)
    {
        if (enemies.Count <= 0) return;

        if (enemies.Contains(enemy))
        {
            Rigidbody rb = enemy.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;

            Collider collider = enemy.GetComponent<Collider>();
            if (collider != null) Destroy(collider);
            enemies.Remove(enemy);

            if (enemies.Count <= 0)
            {
//                FinishArea(); // Area Finished
            }
        }
    }


    #endregion
}
