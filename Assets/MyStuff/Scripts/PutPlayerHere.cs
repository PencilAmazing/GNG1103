using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutPlayerHere : MonoBehaviour
{
    public GameObject Player;
    public GameObject OtherCamera;

    IEnumerator Delayed()
    {
        yield return new WaitForSeconds(0.1f);

        Player.transform.position = this.transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Delayed());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
