using UnityEngine;
using UnityEngine.UI;

public class PlayerHud : MonoBehaviour
{
    [SerializeField]
    private Rigidbody target;

    [Header("UI")]
    [SerializeField]
    public Text speedLabel;
    private float speed = 0.0f;

    private void Update()
    {
        speed = target.linearVelocity.magnitude * 3.6f;
        if (speedLabel != null)
        {
            speedLabel.text = "Speed: " + speed.ToString("F2") + " km/h";
        }
    }
}
