using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class VertexDTO
{
    public long x;
    public long y;
    public long z;
    public InsectType insect;
    public bool highlighted;
}