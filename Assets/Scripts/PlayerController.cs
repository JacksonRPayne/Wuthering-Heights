using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 5.0f;
    public float mouseSensitivity = 2.0f;
    public Transform cam;

    private InteractableObject selectedObject;
    private Rigidbody rb;
    private Vector3 movement = Vector3.zero;

	void Start () {
        rb = GetComponent<Rigidbody>();
        Cursor.visible = false;
	}
	
	void Update () {

        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        float rotY = Input.GetAxis("Mouse X") *mouseSensitivity;
        float rotX = Input.GetAxis("Mouse Y")*-mouseSensitivity;

        rb.MoveRotation(rb.rotation*Quaternion.Euler(new Vector3(0,rotY,0)));
        cam.Rotate(new Vector3(rotX, 0, 0));

        Vector3 moveHorizontal = inputX * transform.right;
        Vector3 moveVertical = inputY * transform.forward;

        movement = (moveHorizontal + moveVertical).normalized * speed;


        RaycastHit hit;
        Physics.Raycast(transform.position, cam.forward, out hit);
        InteractableObject hitObject = null;

        if (hit.collider != null)
        {
            hitObject = hit.transform.GetComponent<InteractableObject>();
        }

        if(hitObject != selectedObject)
        {
            if(selectedObject !=null)
                selectedObject.UnHighlight();

            if (hitObject != null)
            {
                selectedObject = hitObject;
                selectedObject.Highlight();
            }
            else
                selectedObject = null;
        }

        if(Input.GetMouseButtonDown(0))
        {
            if (Manager.Instance.textBoxActive)
                Manager.Instance.DisableTextBox();
            else if(selectedObject != null)
                selectedObject.DisplayQuote();
        }

	}

    private void FixedUpdate()
    {
        if(movement != Vector3.zero)
            rb.MovePosition(transform.position + movement * Time.fixedDeltaTime);
    }
}
