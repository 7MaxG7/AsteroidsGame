using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {
    [SerializeField] Sprite asteroid_1;
    [SerializeField] Sprite asteroid_2;
    [SerializeField] Sprite asteroid_3;
    Rigidbody2D rb_comp;
    CircleCollider2D cc_comp;
    SpriteRenderer sr_comp;
    float standartRadius;
    float angleMoveDeviation = ConfigurationUtils.AsteroidAngleMoveDeviation;
    float minMagnitude = ConfigurationUtils.MinAsteroidMagnitude;
    float maxMagnitude = ConfigurationUtils.MaxAsteroidMagnitude;
    float difficultyMagnitudeIncreaseMultyplicator = ConfigurationUtils.DifficultyMagnitudeMultiplicator;

    public void Initialize() {
        rb_comp = GetComponent<Rigidbody2D>();
        cc_comp = GetComponent<CircleCollider2D>();
        sr_comp = GetComponent<SpriteRenderer>();
        ChooseAsteroidSprite();
        standartRadius = cc_comp.radius;

        TEventManager<int>.AddListener(EventEnum.IncreaseDifficultyEvent, IncreaseAsteroidsSpeed);
        TEventManager<int>.AddListener(EventEnum.GameOver, ResetAsteroidsDifficulty);
    }

    void ChooseAsteroidSprite() {
        int asterSpriteChooser = Random.Range(0, 3);
        switch (asterSpriteChooser) {
            case 0:
                sr_comp.sprite = asteroid_1;
                break;
            case 1:
                sr_comp.sprite = asteroid_2;
                break;
            case 2:
                sr_comp.sprite = asteroid_3;
                break;
        }
    }
    public void Push(Direction moveDirection) {
        float angle = Random.Range(-angleMoveDeviation / 2 * Mathf.Deg2Rad, angleMoveDeviation / 2 * Mathf.Deg2Rad);
        switch (moveDirection) {
            case Direction.Up:
                angle += 90 * Mathf.Deg2Rad;
                break;
            case Direction.Left:
                angle += 180 * Mathf.Deg2Rad;
                break;
            case Direction.Down:
                angle += 270 * Mathf.Deg2Rad;
                break;
        }
        StartMoving(angle);
    }
    public void StartMoving(float angle) {
        float magnitude = Random.Range(minMagnitude, maxMagnitude);
        rb_comp.AddForce(
            new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * magnitude, ForceMode2D.Impulse
        );
    }
    void IncreaseAsteroidsSpeed(int noValue) {
        minMagnitude *= (1 + difficultyMagnitudeIncreaseMultyplicator);
        maxMagnitude *= (1 + difficultyMagnitudeIncreaseMultyplicator);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        //if (collision.gameObject.tag == "bullet") {
        ObjectsPool.ReturnObjectToPool(collision.gameObject);

        Vector3 newScale = transform.localScale;
        if (newScale.x == 1) {
            newScale.x /= 2f;
            newScale.y /= 2f;
            List<GameObject> asterList_tmp = new List<GameObject>(2) { gameObject };
            asterList_tmp.Add(ObjectsPool.GetAsteroidFromPool(transform.position));
            rb_comp.velocity = Vector2.zero;
            foreach (GameObject aster in asterList_tmp) {
                aster.transform.localScale = newScale;
                aster.GetComponent<CircleCollider2D>().radius /= 2;
                aster.GetComponent<Asteroid>().StartMoving(Random.Range(0, 2 * Mathf.PI));
            }
        } else {
            ObjectsPool.ReturnObjectToPool(gameObject);
        }
        AudioManager.Play(AudioClipName.AsteroidHit);
        //}
    }
    public void RestoreSmallAsteroid() {
        if (transform.localScale.x < 1) {
            transform.localScale = new Vector3(1, 1, 1);
            cc_comp.radius = standartRadius;
        }
    }
    void ResetAsteroidsDifficulty(int noValue) {
        minMagnitude = ConfigurationUtils.MinAsteroidMagnitude;
        maxMagnitude = ConfigurationUtils.MaxAsteroidMagnitude;
    }
}
