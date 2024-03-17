using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRandomSelect : MonoBehaviour
{
    [SerializeField]private List<Mesh> BossModel;
    [SerializeField] private SkinnedMeshRenderer BossMesh;
    private void OnEnable() => BossMesh.sharedMesh = BossModel[Random.Range(0, BossModel.Count)];    
}
