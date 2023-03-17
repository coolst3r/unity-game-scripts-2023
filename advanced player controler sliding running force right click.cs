// Player movement settings
public float moveSpeed = 10f;
public float wallSpeed = 5f;
public float slideSpeed = 15f;
public float slideTime = 1f;
public float gravityScale = 1f;
public float pushForce = 500f;

// Sliding state
private bool isSliding = false;
private float slideTimer = 0f;

void FixedUpdate() {
    // Check if the player is running and pressing the shift and Q keys to slide
    if (isRunning && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Q)) {
        // Start sliding if not already sliding
        if (!isSliding) {
            isSliding = true;
            slideTimer = slideTime;
            rigidbody.AddForce(transform.forward * slideSpeed, ForceMode.Impulse);
        }
        // Decrement slide timer and stop sliding when timer is up
        else if (slideTimer > 0) {
            slideTimer -= Time.fixedDeltaTime;
        }
        else {
            isSliding = false;
        }
    }
    // Stop sliding if no longer pressing shift and Q keys
    else {
        isSliding = false;
    }

    // Apply gravity scale to the player
    rigidbody.AddForce(Physics.gravity * gravityScale, ForceMode.Acceleration);

    // Check if the player is on a wall or upside down
    if (isOnWall || isUpsideDown) {
        // Calculate the movement direction perpendicular to the surface and add force
        Vector3 wallDirection = Vector3.Cross(transform.up, surfaceNormal);
        Vector3 inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 moveDirection = Vector3.ProjectOnPlane(inputDirection, surfaceNormal).normalized * moveSpeed;
        // Apply additional force when sliding
        if (isSliding) {
            moveDirection += transform.forward * slideSpeed;
        }
        // Apply additional force when moving on a wall
        if (isOnWall) {
            moveDirection += wallDirection * wallSpeed;
        }
        // Move the player in the desired direction
        rigidbody.AddForce(moveDirection, ForceMode.Impulse);
    }
    // Allow normal movement on the ground
    else {
        Vector3 inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        // Apply additional force when sliding
        if (isSliding) {
            rigidbody.AddForce(inputDirection * slideSpeed, ForceMode.Impulse);
        }
        // Move the player in the desired direction
        rigidbody.AddForce(inputDirection * moveSpeed, ForceMode.Impulse);
    }

    // Push objects forward when right-clicking
    if (Input.GetMouseButton(1)) {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit)) {
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if (rb != null) {
                rb.AddForce(transform.forward * pushForce, ForceMode.Impulse);
            }
        }
    }
}

#Note that pushing objects forward only works if the object has a Rigidbody component attached to it. The code also includes the requested change to use the Q key instead of the Left Control key for sliding.
