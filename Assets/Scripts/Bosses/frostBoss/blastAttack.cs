using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blastAttack : MonoBehaviour {

    public Vector3 center;
    public Vector3 size;

    public Vector3 blastPosition;

    public void bAttack()
    {
        blastPosition = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(center, size);
    }
}
