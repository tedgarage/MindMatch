
using UnityEngine;
// [ExecuteInEditMode]
public class AnimateOffset : MonoBehaviour
{

    public Material mat;
    public Vector2 speed;
    public bool animate = false;
    Vector2 textureOffset = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        if (mat == null)
        {
            mat = GetComponent<MeshRenderer>().materials[1];
        }
        else
        {
            mat.mainTextureOffset = textureOffset;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (animate)
        {
            textureOffset += (Time.deltaTime * speed);
            mat.mainTextureOffset = textureOffset;
            //mat.SetTextureOffset("_MainTexture", textureOffset);

        }
    }
}
