using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Resource", menuName = "Storages/NewResource", order = 2)]
public class ResourceStorage : ScriptableObject
{
    public int _coin, _key;
    public float _xp;
}
