using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();

    [SerializeField] 
    [Range(0f, 5f)] //A negative value is game-breaking so add a range for it. 
    float speed = 1f;
    Enemy enemy;

    void OnEnable()
    {
       FindPath();
       ReturnToStart();
       StartCoroutine(PrintWaypointName());
 
    }

    private void Start()
    {
       enemy = GetComponent<Enemy>();
    }

    void FindPath()
    {
        path.Clear();

        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("Path");

        foreach (GameObject waypoint in waypoints)
        {
            path.Add(waypoint.GetComponent<Waypoint>());
        }

        //GameObject parent = GameObject.FindGameObjectWithTag("Path");

        //foreach (Transform child in parent.transform)
        //{
        //    path.Add(child.GetComponent<Waypoint>());
        //}
    }

    void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }
    
    IEnumerator PrintWaypointName()
    {
        foreach (Waypoint waypoint in path)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = waypoint.transform.position;
            float travelPercent = 0f;

            //Rotate at correct position
            transform.LookAt(endPosition);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }
        enemy.StealGold();
        gameObject.SetActive(false);
    }
}
