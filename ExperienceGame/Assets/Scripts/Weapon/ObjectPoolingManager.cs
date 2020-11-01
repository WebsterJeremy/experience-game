using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{
    public static ObjectPoolingManager Instance { get {return instance; } }
    public GameObject bulletPrefab;
    public GameObject grenadePrefab;
    public int bulletAmount = 20; // amount of pre-instantiated bullets
    public int grenadeAmount = 5;
    
    private List<GameObject> bullets; // reference list to pre-instantiated bullets
    private List<GameObject> grenades;
    private static ObjectPoolingManager instance;
    
    // setup method is run before start
    void Awake(){
        instance = this;

        //Preload bullets
        bullets = new List<GameObject>(bulletAmount);
        //Preload grenades
        grenades = new List<GameObject>(grenadeAmount);

        for (int i = 0; i < bulletAmount; i++){
            GameObject prefabInstance = Instantiate (bulletPrefab);
            prefabInstance.transform.SetParent (transform);
            prefabInstance.SetActive (false);
            bullets.Add (prefabInstance);
        }

        for (int i = 0; i < grenadeAmount; i++){
            GameObject prefabInstance = Instantiate (grenadePrefab);
            prefabInstance.transform.SetParent (transform);
            prefabInstance.SetActive(false);
            grenades.Add(prefabInstance);
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
public GameObject GetGrenadeTest(){
    Debug.Log("Got Grenade");
    return null;
}

// This is not used. Had issues so we Instantiate the object in place.
public GameObject GetGrenades(){
    foreach (GameObject grenade in grenades){
        if (!grenade.activeInHierarchy){
            grenade.SetActive (true);
            return grenade;
            }
        }

    GameObject prefabInstance = Instantiate (grenadePrefab);
    prefabInstance.transform.SetParent (transform);
    grenades.Add (prefabInstance);
    return prefabInstance;
}

    // Update is called once per frame
    void Update(){
    }
}
