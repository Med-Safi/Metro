using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lr_Testing : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private Lr_lineController line;

    private void Start()
    {
        line.SetUpLine(points);
    }
}
