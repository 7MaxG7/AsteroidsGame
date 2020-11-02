using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : TEventInvoker<int> {
    [SerializeField] GameObject hudObj;
    Rigidbody2D rb_comp;
    Vector2 thrustDirection = new Vector2(1, 0);
    float ThrustForce = ConfigurationUtils.ShipThrustForce;
    float RotateDegreesPerSecond = ConfigurationUtils.ShipRotationSpeed;
    bool fireDone = false;

	void Start() {
        rb_comp = GetComponent<Rigidbody2D>();
        events.Add(EventEnum.ShipDestroyedEvent, new IntEvent());
        TEventManager<int>.AddInvoker(EventEnum.ShipDestroyedEvent, this);
	}
	
	void Update() {
        float rotationInput = Input.GetAxis("Rotate");
        float fireInput = Input.GetAxis("Fire1");
        if (rotationInput != 0) {
            float rotationAmount = RotateDegreesPerSecond * Time.deltaTime;
            if (rotationInput < 0) {
                rotationAmount *= -1;
            }
            transform.Rotate(Vector3.forward, rotationAmount);

            float zRotation = transform.eulerAngles.z * Mathf.Deg2Rad;
            thrustDirection.x = Mathf.Cos(zRotation);
            thrustDirection.y = Mathf.Sin(zRotation);
        }

        if (fireInput > 0 && !fireDone) {
            fireDone = true;
            GameObject myBullet = ObjectsPool.GetBulletFromPool(transform.position);
            myBullet.GetComponent<Bullet>().ApplyForce(thrustDirection);
            AudioManager.Play(AudioClipName.PlayerShot);
        } else if (fireInput == 0) {
            fireDone = false;
        }
	}

    void FixedUpdate() {
        if (Input.GetAxis("Thrust") != 0) {
            rb_comp.AddForce(ThrustForce * thrustDirection, ForceMode2D.Force);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        events[EventEnum.ShipDestroyedEvent].Invoke(0);
        AudioManager.Play(AudioClipName.PlayerDeath);
        Vector2 position = transform.position;
        Destroy(gameObject);
        Instantiate(Resources.Load<GameObject>(@"prefabs\ExplosionPref"), position, Quaternion.identity);
    }
}
