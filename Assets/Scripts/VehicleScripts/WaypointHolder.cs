using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vino.Devs {
	public class WaypointHolder : MonoBehaviour
	{

		public List<Transform> points = new List<Transform>();
		public static WaypointHolder instance;

		private void Awake()
		{
			instance = this;
		}

		private void Start()
		{
			foreach (Transform p in transform)
			{
				points.Add(p);
			}
		}

		public Transform nextPoint(Transform currentPoint)
		{
			if (points.IndexOf(currentPoint) + 1 >= points.Count)
			{
				GameManager.instance.IsGameOver = true;
				return currentPoint;
			}
			return points[points.IndexOf(currentPoint) + 1];
		}

		public Transform firstPoint()
		{
			return points[0];
		}

		public Transform GetClosestPoint(Transform vehicle)
		{
			var closestDist = 100.0f;
			var indexOfClosest = 0;
			for (int i = 0; i < points.Count; i++)
			{
				var curDist = Vector3.Distance(vehicle.position, points[i].position);
				if (curDist < closestDist)
				{
					closestDist = curDist;
					indexOfClosest = i;
				}

			}
			return points[indexOfClosest];
		}
	}
}
