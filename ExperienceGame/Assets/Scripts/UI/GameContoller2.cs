using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameContoller2 : MonoBehaviour{

public Player2 player2;
public Text ammoText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){
    ammoText.text = "Ammo: " + player2.Ammo;    
    }
}
