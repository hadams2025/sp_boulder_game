using UnityEngine;

//Script applied to game camera, making it a third person camera that rotates in sync with the player rotation
public class camScript : MonoBehaviour
{
    public float turnSpeed = 4.0f;
    public GameObject target;
    private float targetDistance;
    public float minTurnAngle = -90.0f;
    public float maxTurnAngle = 0.0f;
    private float rotX;

    //get the distance between the player object and camera object
    void Start()
    {
        targetDistance = Vector3.Distance(transform.position, target.transform.position);
    }
    void LateUpdate()
    {
        // get the mouse inputs
        float y = Input.GetAxis("Mouse X") * turnSpeed * Time.deltaTime;
        rotX += Input.GetAxis("Mouse Y") * turnSpeed * Time.deltaTime;
        // clamp the vertical rotation
        rotX = Mathf.Clamp(rotX, minTurnAngle, maxTurnAngle);
        // rotate the camera
        transform.eulerAngles = new Vector3(-rotX, transform.eulerAngles.y + y, 0);
        // move the camera position
        transform.position = target.transform.position - (transform.forward * targetDistance);
        //get the eulerangles of the object and store them in targetEulerAngles
        Vector3 targetEulerAngles = transform.eulerAngles;
        //set the x and z values of targetEulerAngles to 0 because we only care about rotating on the Y axis
        targetEulerAngles.x = 0;
        targetEulerAngles.z = 0;
        //set the playerObject's rotation to be the same as targetEulerAngles
        target.transform.eulerAngles = targetEulerAngles;
    }
}
