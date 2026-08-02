using System.Collections;
using UnityEngine;
using Unity.Netcode;

// Basic player movement script (network-ready).
// PC controls (local): Z = forward, Q = left, D = right, S = backward, Space = jump, LeftControl = slide
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : NetworkBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float slideDuration = 0.5f;

    private Rigidbody rb;
    private bool isSliding = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Only let the owning client control this object.
        if (!IsOwner) return;

        Vector3 dir = Vector3.zero;
        if (Input.GetKey(KeyCode.Q)) dir += Vector3.left;
        if (Input.GetKey(KeyCode.D)) dir += Vector3.right;
        if (Input.GetKey(KeyCode.Z)) dir += Vector3.forward;
        if (Input.GetKey(KeyCode.S)) dir += Vector3.back;

        if (!isSliding)
        {
            transform.Translate(dir.normalized * moveSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && !isSliding)
        {
            StartCoroutine(Slide());
        }

        // Attack placeholder: mapped to C in original plan; keep a simple key for local testing
        if (Input.GetKeyDown(KeyCode.C))
        {
            var fighter = GetComponent<Fighter>();
            if (fighter != null)
                fighter.LightAttack();
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    private IEnumerator Slide()
    {
        isSliding = true;
        float originalSpeed = moveSpeed;
        moveSpeed *= 1.8f;
        yield return new WaitForSeconds(slideDuration);
        moveSpeed = originalSpeed;
        isSliding = false;
    }
}
