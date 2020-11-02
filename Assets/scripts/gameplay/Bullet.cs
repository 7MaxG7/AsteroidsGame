using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    float bulletLifeTime = ConfigurationUtils.BulletLifeTime;
    Timer deathTimer;
    Rigidbody2D rb_comp;

    public void Initialize() {
        rb_comp = GetComponent<Rigidbody2D>();
        deathTimer = gameObject.AddComponent<Timer>();
        deathTimer.Duration = bulletLifeTime;
        deathTimer.AddTimerFinishedListener(ReturnBulletToPool);
    }

    public void ApplyForce(Vector2 bulletDirection) {
        deathTimer.Run();
        float magnitude = 13;
        rb_comp.AddForce(bulletDirection * magnitude, ForceMode2D.Impulse);
    }
    void ReturnBulletToPool() {
        ObjectsPool.ReturnObjectToPool(gameObject);
    }
}