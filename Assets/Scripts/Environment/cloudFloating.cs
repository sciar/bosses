using UnityEngine;
using System.Collections;

public class cloudFloating : MonoBehaviour {

    // Update is called once per frame
    private void Update()
    {
        transform.position -= Vector3.forward * Time.deltaTime;
		//transform.position += new Vector3(transform.position.x - 0.001f, transform.position.y, transform.position.z - 0.001f);
    }
    
}
