using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCollide : MonoBehaviour
{
    GameManager gm;

    public string triggerTag;

    ShipData sd;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        sd = GetComponentInParent<ShipData>();
    }

    void OnCollisionEnter(Collision collision) 
    {
        if (collision.collider.tag == triggerTag) {

            var asteroid = collision.collider.GetComponentInParent<Asteroid>();

            if (asteroid != null) {
                switch (asteroid.size) {
                    case 0:
                        sd.health -= 0.111112f;
                        break;
                    case 1:
                        sd.health -= 0.3334f;
                        break;
                    case 2: 
                        sd.health -= 1.0f;
                        break;
                    default:
                        sd.health -= 1.0f;
                        break;
                }

                if (sd.health <= 0) {
                    GameObject explosion = Instantiate(gm.shipExplosionPrefab, transform.parent.position, Random.rotation);
                    explosion.transform.localScale = 0.8f * explosion.transform.localScale;

                    gm.shipHitSound.Play();

                    Destroy(transform.parent.gameObject);
                }   
                else {
                    GameObject explosion = Instantiate(gm.shipExplosionPrefab, transform.parent.position, Random.rotation);
                    explosion.transform.localScale = Mathf.Pow(2, asteroid.size) * explosion.transform.localScale;
                    explosion.transform.localScale = 0.15f * explosion.transform.localScale;

                    gm.shipHitSound.Play();
                }
                Destroy(collision.collider.transform.parent.gameObject); 
            }



        }

    }

}
