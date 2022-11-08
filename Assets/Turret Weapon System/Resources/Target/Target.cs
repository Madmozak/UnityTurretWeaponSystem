using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float maxXRange;
    public float maxYRange;
    public float maxZRange;
    public float speed;
    public Vector3 placeToMove;
    
    
    // Start is called before the first frame update
    void Start()
    {
        placeToMove = generatePlaceToMove();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, placeToMove, speed * Time.deltaTime);
        if(Vector3.Distance(transform.position, placeToMove) < 1)
        {
            placeToMove = generatePlaceToMove();
        }
    }

    private Vector3 generatePlaceToMove(){
        float x = Random.Range(-maxXRange, maxXRange);
        float y = Random.Range(-maxYRange, maxYRange);
        float z = Random.Range(-maxZRange, maxZRange);
        return new Vector3(x,y,z);
    }
}
