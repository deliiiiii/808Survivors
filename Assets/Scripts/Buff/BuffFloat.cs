using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class BuffFloat
{
    [SerializeField]
    float baseVal;
    [SerializeField]
    List<float> baseAddVals;
    [SerializeField]
    List<float> baseMulVals;
    //List<T> finalAddVals = new();
    //List<T> finalMulVals = new();
    public BuffFloat(float baseVal)
    {
        this.baseVal = baseVal;
    }
    public float Value
    {
        get
        {
            if(baseAddVals == null)
            {
                Initialize();
            }
            float ret = baseVal;
            foreach (var baseAddVal in baseAddVals)
            {
                ret += baseAddVal;
            }
            float baseMul = 1f;
            foreach (var baseMulVal in baseMulVals)
            {
                baseMul *= baseMulVal;
            }
            ret *= baseMul;
            return ret;
        }
    }
    public void BaseAdd(float v)
    {
        baseAddVals.Add(v);
    }
    public void BaseMul(float v)
    {
        baseMulVals.Add(v);
    }
    public void Initialize()
    {
        baseAddVals = new();
        baseMulVals = new();
    }
}
