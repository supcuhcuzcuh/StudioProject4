using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Crate : Entity
{

    [SerializeField]
    private GameObject crate;

    [SerializeField]
    private List<GameObject> cratePieces = new List<GameObject>();

    [SerializeField]
    private GameObject spawnPickup;

    [SerializeField]
    private ParticleEffectManager particleEffectManager;

    void Start()
    {
        for (int i = 0; i < crate.transform.childCount; i++)
        {
            cratePieces.Add(crate.transform.GetChild(i).gameObject);          
        }
    }

    public override void OnDamaged(float damage)
    {
            health -= damage;
            if (health <= 0)
            {
                particleEffectManager.PlayParticleEffect("DestroyedEffect", transform.position);
                Instantiate(spawnPickup, transform.position, Quaternion.identity);

                foreach (GameObject cratePiece in cratePieces)
                {
                    cratePiece.GetComponent<Collider>().enabled = true;
                    cratePiece.GetComponent<Rigidbody>().isKinematic = false;
                    cratePiece.GetComponent<Rigidbody>().useGravity = true;
                }
                StartCoroutine("ClearPieces");
            }      
    }

    IEnumerator ClearPieces()
    {
        yield return new WaitForSeconds(3);
        Destroy(crate);
    }
}