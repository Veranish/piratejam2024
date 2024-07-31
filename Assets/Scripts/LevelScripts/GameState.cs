using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public GameObject playerRef;
    public GameObject carriageRef;
    public GameObject shadowPrefab;
    // Start is called before the first frame update
    public ShadowScript[] ShadowList; // List of shadows. 
    public Transform[] spawnPoints;
    public float spawnTimer; //How long between spawns
    private float timeTillSpawn; //Used to track how much time until the next spawn (actively changes)

    public bool[] litLamps; //Keeps track of how many lamps are lit.

    void Start()
    {
        //Setup the level
    }

    // Update is called once per frame
    void Update()
    {
        timeTillSpawn -= Time.deltaTime;
        if(timeTillSpawn < 0.0f)
        {
            SpawnShadows();//Spawn the shadow
            timeTillSpawn = spawnTimer; //Reset the timer
            Debug.Log("Spawning Shadow!");
        }
    }

    void SpawnShadows()
    {
        Transform spawnT = spawnPoints[Random.Range(0, spawnPoints.Length - 1)];
        GameObject inst = Instantiate(shadowPrefab, spawnT);
        inst.GetComponent<ShadowScript>().NewTarget(carriageRef.transform);
    }

    public void CarriageArrived()
    {
        //Calculate how many lit lanterns there are for final score.

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
