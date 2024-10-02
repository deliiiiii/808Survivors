using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ObjectPool))]
public class Tile : MonoBehaviour
{
    [SerializeField]
    int countInGrid = 10;
    public int CountInGrid { get => countInGrid; }
}
