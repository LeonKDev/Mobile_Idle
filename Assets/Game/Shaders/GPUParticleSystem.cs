using UnityEngine;

public class GPUParticleSystem : MonoBehaviour
{
    struct Particle
    {
        public Vector2 position;
        public Vector2 velocity;
        public float radius;
    }

    public int particleCount = 1000000;
    public ComputeShader computeShader;
    public Mesh particleMesh;
    public Material particleMaterial;

    private ComputeBuffer particleBuffer;
    private int kernelHandle;
    private uint threadGroupSize;

    private Bounds bounds;
    //  private Bounds bounds = new Bounds(Vector3.zero, Vector3.one * 10f);

    void Start()
    {
        float worldSize = 1000f; // Adjust this value based on your game world size
        bounds = new Bounds(Vector3.zero, Vector3.one * worldSize);

        // Create particle buffer
        particleBuffer = new ComputeBuffer(particleCount, sizeof(float) * 5);

        // Initialize particles
        Particle[] particles = new Particle[particleCount];
        for (int i = 0; i < particleCount; i++)
        {
            particles[i].position = Random.insideUnitCircle;
            particles[i].velocity = Random.insideUnitCircle * 0.1f;
            particles[i].radius = 0.02f;
        }
        particleBuffer.SetData(particles);

        // Get kernel
        kernelHandle = computeShader.FindKernel("CSMain");

        // Get thread group size
        computeShader.GetKernelThreadGroupSizes(kernelHandle, out threadGroupSize, out _, out _);

        // Set buffer
        computeShader.SetBuffer(kernelHandle, "particles", particleBuffer);
        particleMaterial.SetBuffer("particles", particleBuffer);
    }

    void Update()
    {
        bounds = new Bounds(Vector3.zero, Vector3.one * 100f); // Debug: Force large area

        // Run compute shader
        computeShader.SetFloat("deltaTime", Time.deltaTime);
        computeShader.Dispatch(kernelHandle, Mathf.CeilToInt(particleCount / (float)threadGroupSize), 1, 1);

        // Render particles
        Graphics.DrawMeshInstancedProcedural(particleMesh, 0, particleMaterial, bounds, particleCount);
    }

    void OnDestroy()
    {
        // Release buffer
        if (particleBuffer != null) particleBuffer.Release();
    }
}
