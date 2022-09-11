using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float movementSpeed;
    [SerializeField] public TextMeshProUGUI countText;
    [SerializeField] public GameObject winTextObject;

    private Rigidbody rb;
    private float movementX;
    private float movementZ;

    private int count = 0;
    private float cooldown = 5f;
    private bool isFinished = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetCountText();
        winTextObject.SetActive(false);
    }

    private void SetCountText()
    {
        countText.text = "Count: " + count;
        if (count >= 8)
        {
            winTextObject.SetActive(true);
            isFinished = true;
        }
    }

    private void Update()
    {
        if (isFinished)
        {
            cooldown -= Time.deltaTime;

            if (cooldown <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementZ = movementVector.y;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0, movementZ);
        rb.AddForce(movement * movementSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }
}
