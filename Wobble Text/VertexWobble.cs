using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeformingLetters : MonoBehaviour
{
    TMP_Text textMesh;
    Mesh mesh;

    Vector3[] vertices;

    [Header("Math deform values")]
    public float sin = 3.3f;
    public float cos = 2.5f;

    void Start()
    {
        textMesh = GetComponent<TMP_Text>();
    }

    void Update()
    {
        textMesh.ForceMeshUpdate();
        mesh = textMesh.mesh;
        vertices = mesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 offset = Wobble(Time.time + i);

            vertices[i] = vertices[i] + offset;
        }

        mesh.vertices = vertices;
        textMesh.canvasRenderer.SetMesh(mesh);
    }

    Vector2 Wobble(float time) {
        return new Vector2(Mathf.Sin(time*sin), Mathf.Cos(time*cos));
    }
}