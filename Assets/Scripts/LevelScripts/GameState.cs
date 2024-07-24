using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public GameObject playerRef;
    public GameObject carriageRef;
    // Start is called before the first frame update
    public ShadowScript[] ShadowList; // List of shadows. 

    public bool[] litLamps; //Keeps track of how many lamps are lit.

    void Start()
    {
        //Setup the level
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnShadows()
    {
        //Iterate through a list of spawns, find closest to the player?
        //Or carriage itself has shadows that spawn along it? 
    }

    public void CarriageArrived()
    {

    }

    public void GameOver()
    {

    }

    void lampLit(int id) //lamps when lit will report to the gamestate theird ID, so final tally can be kept.
    {
        litLamps[id] = true;
    }

    int checkLights()//After carriage arrives, check if all lights are lit. If not, detract from total light value. Returns count of unlit lamps.
    {
        int count = 0;
        foreach (var l in litLamps) 
        { 
            if(l == false)
            { count++; }
        }

        return count;

    }


}
