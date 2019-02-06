using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    public float rotationSpeed = 1f;
    public float scaleSpeed = 1f;

    public Camera cam;

    public LayerMask hoverLayers;

    Transform selectedObject;
    Transform hoveredObject;
    InteractableObject hoveredObjectData = null;

    void Update () {

        RaycastHit hoverHit;
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2));
        Physics.Raycast(ray, out hoverHit, 1000, hoverLayers);
        Debug.DrawLine(ray.origin, ray.direction);


        if (hoverHit.collider != null)
        {
            hoveredObjectData = hoverHit.collider.transform.GetComponent<InteractableObject>();
            if (hoveredObjectData != null)
                Hover(hoverHit.transform);
        }
        else if (hoveredObject != null)
            UnHover();

        if (hoveredObject !=null)
        Debug.Log(hoveredObject.name);

        //If there is no touch input, reset the selectedObject
        if (Input.touchCount == 0)
            selectedObject = null;

        //If there is 2 fingers on the screen
        if (Input.touchCount == 2)
        {
            //Gets both of the touches
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            //If there isn't anything selected, it returns out of the loop
            if (selectedObject == null)
            {
                SelectObject(touch1.position);
                SelectObject(touch2.position);

                if (selectedObject == null)
                    return;
            }

            //Gets the positions of the touches last frame
            Vector2 previousPos1 = touch1.position - touch1.deltaPosition;
            Vector2 previousPos2 = touch2.position - touch2.deltaPosition;

            //Gets the distance between both fingers last frame
            float previousDistance = (previousPos1 - previousPos2).magnitude;
            //Gets the distance between both fingers this frame
            float currentDistance = (touch1.position - touch2.position).magnitude;

            //Gets the change in distance between the 2 fingers in this frame
            float distanceMagnitude = (currentDistance - previousDistance);
            //Multiplies it with scaleSpeed
            float changeAmount = distanceMagnitude * scaleSpeed;

            //Gets the SelectableObject script attached to the object
            SelectableObject objectData = selectedObject.GetComponent<SelectableObject>();
            //What the new scale will be
            Vector3 newScale = selectedObject.transform.localScale + new Vector3(changeAmount, changeAmount, changeAmount);

            //If the new scale is bigger than the max scale, set the scale to the max scale
            if (newScale.x > objectData.maximumSize)
                selectedObject.transform.localScale = objectData.maximumVector;

            //If the new scale is smaller than the min scale, set the scale to the min scale
            else if (newScale.x < objectData.minimumSize)
                selectedObject.transform.localScale = objectData.minimumVector;

            //If the new scale is within the set bounds, set it to the new scale
            else
                selectedObject.transform.localScale = newScale;
        }

        //If there is one finger on the screen
        if (Input.touchCount == 1)
        {
            //Gets the touch
            Touch touch = Input.GetTouch(0);

            //If there isn't an object selected, attempt to select a new one
            if (selectedObject == null)
            {
                SelectObject(touch.position);
            }

            //If the finger has moved and there is a selected object
            else if (touch.phase == TouchPhase.Moved)
            {
                //Get the rigidbody of the object, and add angular velocity on the y axis multiplied with the rotationSpeed modifier
                Rigidbody rb = selectedObject.GetComponent<Rigidbody>();
                rb.angularVelocity = new Vector3(0, (-touch.deltaPosition.x * rotationSpeed) / (selectedObject.transform.lossyScale.magnitude / 2), 0);
            }
        }

    }

    void SelectObject(Vector2 touchPosition)
    {
        //Get a ray from the position on the screen specified
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(new Vector3(touchPosition.x, touchPosition.y));
        //Shoot the ray
        Physics.Raycast(ray, out hit);

        //If the hit object has a collider, set it to the selected game object
        if (hit.collider != null)
            selectedObject = hit.transform;
    }

    void Hover(Transform obj)
    {
        hoveredObject = obj;
        Manager.Instance.Hover();
        //Highlight
    }

    void UnHover()
    {
        //Unhighlight
        hoveredObject = null;

        if(!Manager.Instance.buttonState)
            Manager.Instance.UnHover();
    }

    public void QuoteButtonClicked()
    {
        if(!Manager.Instance.buttonState)
            Manager.Instance.ActivateTextBox(hoveredObjectData.quote);
        else
        {
            Manager.Instance.DisableTextBox();
        }

        Manager.Instance.ToggleButtonState();
    }


}
