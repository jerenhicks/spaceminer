using UnityEngine;

public class CharacterSwap : MonoBehaviour
{
    public Transform ship;
    public Transform player;

    private PlayerMovement shipMovement;
    private PlayerMovement playerMovement;

    void Start()
    {
        shipMovement = ship.GetComponent<PlayerMovement>();
        playerMovement = player.GetComponent<PlayerMovement>();
        player.gameObject.SetActive(false);
        playerMovement.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Swap();
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
        }
    }
}
