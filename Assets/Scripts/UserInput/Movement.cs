using UnityEngine;

namespace UserInput {
    public class Movement : MonoBehaviour{
        void Update() {
            if (Input.GetKey(KeyCode.W)) {
                gameObject.transform.position += new Vector3(0, 0.05f, 0);
            }

            if (Input.GetKey(KeyCode.A)) {
                gameObject.transform.position += new Vector3(-0.05f, 0, 0);
            }
        
            if (Input.GetKey(KeyCode.S)) {
                gameObject.transform.position += new Vector3(0, -0.05f, 0);
            }
        
            if (Input.GetKey(KeyCode.D)) {
                gameObject.transform.position += new Vector3(0.05f, 0, 0);
            }
        }
    }
}