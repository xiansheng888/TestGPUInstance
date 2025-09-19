using UnityEngine;
public class DrawMeshInstancedDemo : MonoBehaviour
{
    public int instanceCount;
    public float range;

    public Material material;

    private Matrix4x4[] matrices;
    private MaterialPropertyBlock block;

    public Mesh mesh;




    private void Start()
    {
        if (SystemInfo.supportsInstancing)
        {
            Setup();
        }
    }

    private void Update()
    {
        Graphics.DrawMeshInstanced(mesh, 0, material, matrices, instanceCount, block);
    }

    private void Setup()
    {
        matrices = new Matrix4x4[instanceCount];
        Vector4[] colors = new Vector4[instanceCount];
        float[] _Phi = new float[instanceCount];

        block = new MaterialPropertyBlock();

        for (int i = 0; i < instanceCount; i++)
        {
            Vector3 position = new Vector3(Random.Range(-range, range), Random.Range(-range, range), Random.Range(-range, range));
            Quaternion rotation = Quaternion.Euler(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180));
            Vector3 scale = Vector3.one * Random.Range(1, 2);

            var matrix = Matrix4x4.TRS(position, rotation, scale);

            matrices[i] = matrix;

            colors[i] = Color.Lerp(Color.red, Color.blue, Random.value);
            _Phi[i] = Random.Range(-40f, 40f);
        }

        block.SetVectorArray("_Colors", colors);
        block.SetFloatArray("_Phi", _Phi);
    }
}