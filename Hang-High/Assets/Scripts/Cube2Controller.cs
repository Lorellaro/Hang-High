using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cube2Controller : MonoBehaviour
{
    [SerializeField] float upForce;
    [SerializeField] float attachForce;
    [SerializeField] int mouseNum;
    [SerializeField] GameObject clippedSFX;
    [SerializeField] GameObject jumpVFX;
    [SerializeField] GameObject clippedVFX;


    Vector3 mousePos;
    Rigidbody rb;
    bool jump;
    bool canHook = true;

    FixedJoint fixedJoint;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(mouseNum) && fixedJoint)
        {
            //Can't be spam called
            if (!jump) {
                //VFX SFX
                Instantiate(jumpVFX, transform);
            }

            //Jump
            jump = true;

            StartCoroutine(disableJump());

            Destroy(fixedJoint);
            StartCoroutine(hookCooldown());     
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }

        mousePos = Input.mousePosition;

        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        mousePos = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
        );
        mousePos = mousePos.normalized;
    }

    private IEnumerator disableJump()
    {
        yield return new WaitForSeconds(0.15f);
        jump = false;
    }

    private void FixedUpdate()
    {
        if (jump)
        {
            rb.velocity += mousePos * upForce * Time.deltaTime;
        }
    }

    void OnCollisionStay(Collision c)
    {
        //if recently hooked return
        if (!canHook) { return; }
        //if already has joint return
        if (fixedJoint) { return; }
        //if  hit self return
        if(c.gameObject.layer == LayerMask.NameToLayer("Chain") || c.gameObject.layer == LayerMask.NameToLayer("Player End")) { return; }

        fixedJoint = gameObject.AddComponent<FixedJoint>();
        fixedJoint.connectedBody = c.rigidbody;
        //fixedJoint.breakForce = 7000f;

        //Sfx VFX
        Instantiate(clippedSFX, transform);
        Instantiate(clippedVFX, transform);
    }

    private IEnumerator hookCooldown()
    {
        canHook = false;
        yield return new WaitForSeconds(0.15f);
        canHook = true;
    }
}
