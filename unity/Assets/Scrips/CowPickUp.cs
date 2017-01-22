using UnityEngine;

public class CowPickUp : MonoBehaviour
{
    // TODO actually place on the floor.
    float initialYPos;
    public bool isBeingHold;
    public PlayerController playerController;

    private void OnTriggerEnter(Collider other)
    {
        if (isBeingHold) return;

        if (other.CompareTag("Player"))
        {
            initialYPos = transform.parent.position.y;
            playerController = other.GetComponent<PlayerController>();
            if (playerController != null) playerController.CanPickCow(this, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var playerController = other.GetComponent<PlayerController>();
            if (playerController != null) playerController.CanPickCow(this, false);
        }
    }

    public void DropAt(Vector3 pos)
    {
        pos.y = initialYPos;
        transform.parent.position = pos;
        transform.parent.SetParent(null);
        isBeingHold = false;
		playerController = null;
    }

    public void GrabAt(Vector3 pos, Transform newParent)
    {
        isBeingHold = true;
        transform.parent.position = pos;
        transform.parent.SetParent(newParent);
    }
}
