using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private LayerMask interactableLayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject gameObj = collision.gameObject;
        if(LayerChecker.IsInMask(gameObj, interactableLayer))
        {
            var interactable = gameObj.GetComponent<Interactable>();
            interactable.OnInteract();
        }
    }
}
