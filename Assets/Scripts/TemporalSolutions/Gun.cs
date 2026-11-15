using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float sensivility = 100;
    [SerializeField] private GameObject gun;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 objectpos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(objectpos.y, objectpos.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation,sensivility * Time.deltaTime);
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        gun.transform.position = mousePos;

        //float mouseX = Input.GetAxis("Mouse X") * sensivility * Time.deltaTime;
        //float mouseY = Input.GetAxis("Mouse Y") * sensivility * Time.deltaTime;
        //
        //float checkX = RotX;
        //float checkY = RotY;
        //
        //if(mouseX != 0) RotX = mouseX;
        //if (mouseY != 0) RotY = mouseY;
        //
        //float rotationer;
        //
        //rotationer = Mathf.Atan2(RotY, RotX);
        //
        //transform.rotation = Quaternion.Euler(0, 0, rotationer);
    }
}
