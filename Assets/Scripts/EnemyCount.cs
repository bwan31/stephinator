using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCount : MonoBehaviour
{

    public static GameObject[] creeping;
    public static GameObject[] shooting;
    public static GameObject[] sniping;

    public int creep;
    public int shoot;
    public int snipe; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        creeping = GameObject.FindGameObjectsWithTag("creep");
        creep = creeping.Length;
        
        
    }
}
