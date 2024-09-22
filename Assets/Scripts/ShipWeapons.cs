using UnityEngine;
using GAP_LaserSystem;
using System.Collections.Generic;

public class ShipWeapons : MonoBehaviour
{
    private List<LaserScript> laserScripts = new List<LaserScript>();

    public GameObject laser;
    public List<GameObject> playerFirePoints;
    public GameObject targetPoint;
    private GameObject moveablePoint;
    public float laserSize = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveablePoint = new GameObject("MoveablePoint");
        moveablePoint.transform.position = targetPoint.transform.position;

        // Create and configure laser instances for each fire point
        foreach (var firePoint in playerFirePoints)
        {
            GameObject newLaser = Instantiate(laser);
            LaserScript laserScript = newLaser.GetComponent<LaserScript>();
            laserScript.useTrail = false;
            laserScript.firePoint = firePoint;
            laserScript.size = laserSize;
            laserScript.endPoint = moveablePoint;
            newLaser.SetActive(false); // Initially set lasers to inactive
            laserScripts.Add(laserScript);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (var laserScript in laserScripts)
            {
                laserScript.gameObject.SetActive(true);
                laserScript.EnableLaser();
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            foreach (var laserScript in laserScripts)
            {
                laserScript.DisableLaserCaller(laserScript.disableDelay);
            }
        }
        RefreshLasers();
    }

    private void RefreshLasers()
    {
        foreach (var firePoint in playerFirePoints)
        {
            RaycastHit hit;
            Vector3 direction = targetPoint.transform.position - firePoint.transform.position;
            if (Physics.Raycast(firePoint.transform.position, direction, out hit))
            {
                // Check if the hit object is not the original target object
                if (hit.collider.gameObject != moveablePoint)
                {
                    // If the raycast hits an object other than the target, set the new endpoint to the hit point
                    moveablePoint.transform.position = hit.point;
                }
                else
                {
                    // If the hit object is the target, keep the original endpoint
                    moveablePoint.transform.position = targetPoint.transform.position;
                }
            }
            else
            {
                // If no object is hit, keep the original endpoint
                moveablePoint.transform.position = targetPoint.transform.position;
            }
        }

        // Update laser endpoints
        foreach (var laserScript in laserScripts)
        {
            laserScript.endPoint = moveablePoint;
            laserScript.UpdateLaser();
        }
    }
}
