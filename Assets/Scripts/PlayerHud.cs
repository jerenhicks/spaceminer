using UnityEngine;
using UnityEngine.UI;

public class PlayerHud : MonoBehaviour
{
    [SerializeField]
    private Rigidbody target;

    [Header("UI")]
    [SerializeField]
    public Text speedLabel;
    [SerializeField]
    public Text targetLabel;
    private float speed = 0.0f;

    private void Update()
    {
        speed = target.linearVelocity.magnitude * 3.6f;
        if (speedLabel != null)
        {
            speedLabel.text = "Speed: " + speed.ToString("F2") + " km/h";
        }

        if (targetLabel != null)
        {
            string targetName = "None";
            if (GameState.selectedObject != null)
            {
                AsteroidInfo asteroidInfo = GameState.selectedObject.GetComponent<AsteroidInfo>();
                if (asteroidInfo != null)
                {
                    targetName = asteroidInfo.name;
                }
            }
            targetLabel.text = "Target: " + targetName;
        }
    }
}
