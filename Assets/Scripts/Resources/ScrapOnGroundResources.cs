using UnityEngine;
using System.Collections;

public class ScrapOnGroundResources : MonoBehaviour 
{
    // chips logos
    public Sprite Remains1;
    public Sprite Remains2;
    public Sprite Remains3;
    public Sprite Remains4;
    public Sprite Remains5;

    // ======================================================================================================================================== //
    void Start () 
    {

    }
    // ======================================================================================================================================== //
    void Update () 
    {

    }
    // ======================================================================================================================================== //
    public Sprite GetRandomSprite()
    {
        int rand = Random.Range(0, 5);

        if (rand == 0)
            return Remains1;
        else if (rand == 1)
            return Remains2;
        else if (rand == 2)
            return Remains3;
        else if (rand == 3)
            return Remains4;
        else
            return Remains5;
    }
    // ======================================================================================================================================== //
}
