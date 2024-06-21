using UnityEngine;

public class CameraContoller : MonoBehaviour
{
    
    [SerializeField] Transform PlayerTransform;
    [SerializeField] float _distanceWithTarget = 5;

    [SerializeField] float _minVerticaleAngle = -45;
    [SerializeField] float _maxVerticaleAngle = 45;
    [SerializeField] float _rotationSpeed = 2;

    [SerializeField] bool _invertX;
    [SerializeField] bool _invertY;
    [SerializeField] Vector2 _framingOffset;

    float rotationX;
    float rotationY;
    float YValue;
    float XValue;


    void Start()
    {
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        YValue = _invertY ? -1 : 1;
        XValue = _invertX ? -1 : 1;

        rotationX += Input.GetAxis("Mouse Y") * YValue * _rotationSpeed;
        rotationX = Mathf.Clamp(rotationX, _minVerticaleAngle, _maxVerticaleAngle);

        rotationY += Input.GetAxis("Mouse X") * XValue * _rotationSpeed;

        var targetRotation = Quaternion.Euler(rotationX, rotationY, 0);
        var focusPosition = PlayerTransform.position + new Vector3(_framingOffset.x, _framingOffset.y);

        transform.position = focusPosition - targetRotation * new Vector3(0, 0, _distanceWithTarget);
        transform.rotation = targetRotation;
    }

    public Quaternion PlannerRotation => Quaternion.Euler(0, rotationY, 0);
}
