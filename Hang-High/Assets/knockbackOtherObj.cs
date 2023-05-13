using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knockbackOtherObj : MonoBehaviour
{
    [SerializeField] float knockbackForce = 100;

    bool knockbackOther;
    Rigidbody otherRigidbody;

    private void OnCollisionStay(Collision collision)
    {
        otherRigidbody = collision.transform.root.GetChild(0).GetComponent<Rigidbody>();
        StartCoroutine(applyKnockbackForTime());
    }

    private IEnumerator applyKnockbackForTime()
    {
        knockbackOther = true;
        yield return new WaitForSeconds(0.15f);
        knockbackOther = false;
    }

    private void FixedUpdate()
    {
        if (knockbackOther)
        {
            Vector3 dir = (otherRigidbody.position - transform.position).normalized;
            otherRigidbody.velocity += dir * knockbackForce * Time.deltaTime;
        }
    }
}
