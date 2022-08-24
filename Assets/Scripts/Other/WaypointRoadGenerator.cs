using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointRoadGenerator : MonoBehaviour
{
     private Vector3[] waypoints;
     public Transform WaypointHolder;
     public float RoadWidth = 10;
     private Vector3 roadWidth;
     private List<Vector3> vertecies = new List<Vector3>();
     public Material mat;
     public GameObject pointer;

    private void Start()
    {
        roadWidth = Vector3.right * RoadWidth/2;

        waypoints = new Vector3[WaypointHolder.transform.childCount];
        for (int i = 0; i < WaypointHolder.transform.childCount; i++)
        {
            waypoints[i] = WaypointHolder.transform.GetChild(i).position;
        }
        //StartCoroutine(CalculatePoints());
        CalculatePoints();
        VisualizePoints();
    }

    void Update()
    {
        
    }

    private void GenerateRoadObject()
    {
        GameObject roadGO = new GameObject("road");
        Mesh mesh = new Mesh();
        MeshFilter MF = roadGO.AddComponent<MeshFilter>() as MeshFilter;
        MeshRenderer MR = roadGO.AddComponent<MeshRenderer>() as MeshRenderer;
        MeshCollider col = roadGO.AddComponent<MeshCollider>() as MeshCollider;

        MF.mesh = mesh;

        mesh.vertices = vertecies.ToArray();
        mesh.triangles = CalculateTries().ToArray();

        mesh.RecalculateNormals();
        MR.sharedMaterial = mat;
        col.sharedMesh = mesh;
    }
    
    private List<int> CalculateTries()
    {
        List<int> t = new List<int>();

        for (int i = 0; i < vertecies.Count - 3; i += 3)
        {
            t.Add(i + 3);
            t.Add(i + 2);
            t.Add(1 + 0);
            t.Add(i + 1);
            t.Add(i + 3);
            t.Add(i + 0);
        }

        return t;

    }

    void VisualizePoints()
    {
        foreach (var item in vertecies)
        {
            Instantiate(pointer, item, Quaternion.identity);
        }
    }

    private void CalculatePoints()
    {

       // vertecies.Add(waypoints[0] + roadWidth); // right
        //vertecies.Add(waypoints[0] - roadWidth); // left

        for (int i = 0; i < waypoints.Length - 2; i+=2)
        {


            Vector3 p1 = waypoints[i];
            Vector3 p2 = waypoints[i +1];
            Vector3 p3 = waypoints[i +2];
            

            for (int j = 0; j < 5; j++)
            {
                print(j*0.2f);
                Vector3 q1 = Vector3.Lerp(p1, p2, j*0.2f);
                Vector3 q2 = Vector3.Lerp(p2, p3, j*0.2f);
                Vector3 q3 = Vector3.Lerp(q1, q2, j*0.2f);
                vertecies.Add(q3 - roadWidth);
        

                vertecies.Add(q3 + roadWidth);
            
                
            }

            vertecies.Add(p3 - roadWidth);
            vertecies.Add(p3 + roadWidth);

            GenerateRoadObject();
        }
    }
}
