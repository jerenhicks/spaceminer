using UnityEngine;
using System.Collections.Generic;

public class CharacterSwap : MonoBehaviour
{
    public Transform ship;
    public Transform player;
    public List<GameObject> hatches = new List<GameObject>();
    public float distanceToHatch = 4.0f;
    public GameState gameState;

    private PlayerMovement shipMovement;
    private PlayerMovement playerMovement;
    private CameraRotator cameraRotator;

    void Start()
    {
        shipMovement = ship.GetComponent<PlayerMovement>();
        playerMovement = player.GetComponent<PlayerMovement>();
        player.gameObject.SetActive(false);
        playerMovement.enabled = false;
        cameraRotator = gameState.GetComponent<CameraRotator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (shipMovement.enabled || IsPlayerNearHatch())
            {
                Swap();
            }
        }
    }

    public void Swap()
    {
        if (shipMovement != null)
        {
            //ship.gameObject.SetActive(!ship.gameObject.activeSelf);
            shipMovement.enabled = !shipMovement.enabled;
        }

        if (playerMovement != null)
        {
            player.gameObject.SetActive(!player.gameObject.activeSelf);
            playerMovement.enabled = !playerMovement.enabled;

            if (playerMovement.enabled)
            {
                cameraRotator.target = player;
            }
            else
            {
                cameraRotator.target = ship;
            }
        }
    }

    private bool IsPlayerNearHatch()
    {
        foreach (GameObject hatch in hatches)
        {
            Debug.Log("Distance: " + Vector3.Distance(player.position, hatch.transform.position));
            if (Vector3.Distance(player.position, hatch.transform.position) <= distanceToHatch)
            {
                return true;
            }
        }
        return false;
    }

}
