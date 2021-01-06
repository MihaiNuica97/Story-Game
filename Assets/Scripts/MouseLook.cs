using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    // Update is called once per frame
    // void Update()
    // {

    //     //Get the Screen positions of the object
    //     Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

    //     //Get the Screen position of the mouse
    //     Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

    //     //Get the angle between the points
    //     float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

    //     //Ta Daaa
    //     transform.rotation = Quaternion.Euler(new Vector3(0f, -angle, 0f));
    // }

    // float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    // {
    //     return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    // }
    // void Update()
    // {
    //     RaycastHit hit;
    //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    //     if (Physics.Raycast(ray, out hit, 100))
    //     {
    //         transform.LookAt(hit.point);
    //     }
    // }
    private void Update()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane p = new Plane(Vector3.up, transform.position);
        if (p.Raycast(mouseRay, out float hitDist))
        {
            Vector3 hitPoint = mouseRay.GetPoint(hitDist);
            transform.LookAt(hitPoint);
        }
    }
}


