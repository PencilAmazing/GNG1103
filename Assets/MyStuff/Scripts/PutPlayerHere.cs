using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutPlayerHere : MonoBehaviour
{
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Player.transform.position = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
