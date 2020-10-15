using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{
    public static ObjectPoolingManager Instance { get {return instance; } }
    public GameObject bulletPrefab;
    public int bulletAmount = 20; // amount of pre-instantiated bullets
    
    private List<GameObject> bullets; // reference list to pre-instantiated bullets
    private static ObjectPoolingManager instance;
    
    // setup method is run before start
    void Awake(){
        instance = this;

        //Preload bullets
        bullets = new List<GameObject>(bulletAmount);

        for (int i = 0; i < bulletAmount; i++){
            GameObject prefabInstance = Instantiate (bulletPrefab);
            prefabInstance.transform.SetParent (transform);
            prefabInstance.SetActive (false);
            bullets.Add (prefabInstance);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


public GameObject GetBullet(){
foreach (GameObject bullet in bullets){
    if (!bullet.activeInHierarchy){
        bullet.SetActive (true);
        return bullet;
    }
}
            GameObject prefabInstance = Instantiate (bulletPrefab);
            prefabInstance.transform.SetParent (transform);
            bullets.Add (prefabInstance);
            return prefabInstance;

}

    // Update is called once per frame
    void Update(){
    }
}
