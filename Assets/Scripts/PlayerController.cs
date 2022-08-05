using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))]

public class PlayerController : MonoBehaviour
{
	public Transform target;
	public Joystick joystick;
	public bool isMobileControlsEnabled;

	protected Animator animator;

	private float val = 2;
  private float speed = 0;
  private float direction = 0;
	private Vector3 dir;
  private Locomotion locomotion = null;

	void Awake()
	{
		joystick = GameObject.Find("Movement Joystick").GetComponent<Joystick>();
	}
	void Start ()
	{
        animator = GetComponent<Animator>();
        locomotion = new Locomotion(animator);
	}

	void Update ()
	{

		if (Input.GetButton ("Jump"))
        {
			animator.SetBool ("Jump", true);
		}
		else
		{
			animator.SetBool("Jump", false);
		}

		if (Input.GetButton ("Fire2"))
        {
			//Look in inverse position of Camera
			transform.rotation = Quaternion.LookRotation(transform.position - new Vector3(target.position.x, 0, target.position.z) );

		}

		bool isRun = Input.GetKey(KeyCode.LeftShift);

		if (isRun)
        {
			val = 6;
		}
        else
        {
			val = 2;
		}

        if (animator && Camera.main)
		{
            Move(transform,Camera.main.transform, ref speed, ref direction);
			locomotion.Do(speed * val, direction * 180);
		}
	}

	void Move(Transform root, Transform camera, ref float speed, ref float direction)
    {
        Vector3 rootDirection = root.forward;
        float horizontal;
        float vertical;
				if (isMobileControlsEnabled)
    {
      //for touch input
      if (Mathf.Abs(joystick.Horizontal) > 0.3f)
        horizontal = joystick.Horizontal;
      else
        horizontal = 0;

			vertical = joystick.Vertical;
    }
    else
    {
      horizontal = Input.GetAxisRaw("Horizontal");
      vertical = Input.GetAxisRaw("Vertical");
		}

        Vector3 stickDirection = new Vector3(horizontal, 0, vertical);

        // Get camera rotation.

        Vector3 CameraDirection = camera.forward;
        CameraDirection.y = 0.0f; // kill Y
        Quaternion referentialShift = Quaternion.FromToRotation(Vector3.forward , CameraDirection);

        // Convert joystick input in Worldspace coordinates
        Vector3 moveDirection = referentialShift * stickDirection ;

		Vector2 speedVec =  new Vector2(horizontal, vertical);
		speed = Mathf.Clamp(speedVec.magnitude, 0, 1);


        if (speed > 0.01f) // dead zone
        {
			Vector3 axis = Vector3.Cross(rootDirection, moveDirection);
			direction = Vector3.Angle(rootDirection, moveDirection) / 180.0f * (axis.y < 0 ? -1 : 1);

        }
        else
		{


			direction = 0.0f;


		}
    }
}
