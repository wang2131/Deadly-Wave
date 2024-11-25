using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetected : MonoBehaviour
{
    [SerializeField] private float DetectRadius;

    private Collider2D[] _collider = new Collider2D[1];

    [SerializeField]
    private LayerMask groundLayer;

    private void Awake() {
        
    }

    public bool isGrounded => Physics2D.OverlapCircleNonAlloc(transform.position, DetectRadius, _collider, groundLayer) != 0;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, DetectRadius);
    }
}
