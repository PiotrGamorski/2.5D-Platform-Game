using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    [SerializeField] private GameObject _respawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other?.GetComponent<Player>();
            if (player != null)
                player.Damage();

            CharacterController characterController = other?.GetComponent<CharacterController>();
            if (characterController != null)
                characterController.enabled = false;

            other.transform.position = _respawnPoint.transform.position;

            if (characterController != null)
                StartCoroutine(CharacterControlEnabledRoutine(characterController));
        }
    }

    private IEnumerator CharacterControlEnabledRoutine(CharacterController controller)
    {
        yield return new WaitForSeconds(0.5f);
        controller.enabled = true;
    }
}
