using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MainGame.Cameras;

public class Cube2Controller : MonoBehaviour
{
    [SerializeField] float upForce;
    [SerializeField] float attachForce;
    [SerializeField] int mouseNum;
    [SerializeField] GameObject clippedSFX;
    [SerializeField] GameObject unClippedVFX;
    [SerializeField] GameObject jumpVFX;
    [SerializeField] GameObject clippedVFX;
    [SerializeField] Cube2Controller otherCubeController;
    [SerializeField] bool hasPriority;
    [SerializeField] LayerMask unAttachableLayers;

    Vector3 mousePos;
    Rigidbody rb;
    public bool jump;
    bool canHook = true;
    CameraShakeHandler cameraShakeHandler;

    FixedJoint fixedJoint;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cameraShakeHandler = CameraShakeHandler.Instance;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(mouseNum) && fixedJoint)
        {
            //Can't be spam called
            if (!jump) {
                //VFX SFX
                Instantiate(unClippedVFX, transform);
                cameraShakeHandler.BasicShake();
            }

            //Jump
            jump = true;

            StartCoroutine(disableJump());

            Destroy(fixedJoint);
            StartCoroutine(hookCooldown());     
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            //SceneManager.LoadScene(0);
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
        //only allow one hook to jump at once
        if (jump && (!otherCubeController.jump || hasPriority))
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
        //if(c.gameObject.layer == LayerMask.NameToLayer("Chain") || c.gameObject.layer == LayerMask.NameToLayer("Player End")) { return; }
        if(unAttachableLayers == (unAttachableLayers | (1 << c.gameObject.layer))) { return; }

        fixedJoint = gameObject.AddComponent<FixedJoint>();
        fixedJoint.connectedBody = c.rigidbody;
        fixedJoint.breakForce = 20000f;

        //Sfx VFX
        Instantiate(clippedSFX, transform);
        Instantiate(clippedVFX, transform);
        
        //Cam Shake
        cameraShakeHandler.BasicShake();
    }

    private IEnumerator hookCooldown()
    {
        canHook = false;
        yield return new WaitForSeconds(0.15f);
        canHook = true;
    }
}
