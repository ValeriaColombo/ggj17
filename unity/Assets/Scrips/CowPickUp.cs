using UnityEngine;

public class CowPickUp : MonoBehaviour
{
    // TODO actually place on the floor.
    float initialYPos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            initialYPos = transform.position.y;
            var playerController = other.GetComponent<PlayerController>();
            if (playerController != null) playerController.PickCow(this);
            //print("Pick me up, pick me up real fast, can't pick up...");
        }
    }

    public void DropAt(Vector3 pos)
    {
        pos.y = initialYPos;
        transform.position = pos;
        transform.SetParent(null);
    }

    public void PlaceAt(Vector3 pos, Transform newParent)
    {
        //print("Moooooove me");
        transform.position = pos;
        transform.SetParent(newParent);
    }
}
