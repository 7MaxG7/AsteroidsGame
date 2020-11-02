using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrapper : MonoBehaviour {
    float colliderRadius;
    void Start() {
        colliderRadius = GetComponent<CircleCollider2D>().radius;
    }

    void OnBecameInvisible() {
        Vector2 position = transform.position;

        float screenZ = -Camera.main.transform.position.z;
        Vector3 bottomLeftScreen = new Vector3(0, 0, screenZ);
        Vector3 topRightScreen = new Vector3(Screen.width, Screen.height, screenZ);
        Vector3 bottomLeftWorld = Camera.main.ScreenToWorldPoint(bottomLeftScreen);
        Vector3 topRightWorld = Camera.main.ScreenToWorldPoint(topRightScreen);

        if (position.x < bottomLeftWorld.x - colliderRadius || position.x > topRightWorld.x + colliderRadius) {
            position.x *= -1;
        }
        if (position.y < bottomLeftWorld.y - colliderRadius || position.y > topRightWorld.y + colliderRadius) {
            position.y *= -1;
        }
        transform.position = position;
    }
}
