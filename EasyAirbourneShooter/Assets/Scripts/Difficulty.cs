using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulty : MonoBehaviour
{
    public static float multiplier = 1f;

    public void Easy()
    {
        multiplier = 0.9f;
    }

    public void Medium()
    {
        multiplier = 1f;
    }

    public void Hard()
    {
        multiplier = 1.1f;
    }

}
