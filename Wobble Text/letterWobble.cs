using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class letterWobble : MonoBehaviour
{
    TMP_Text textMesh;
    Mesh mesh;
    
    Vector3[] vertices;

    [Header("Math movement values")]
    public float hMovementCos = 3.3f;
    public float vMovementCos = 2.5f;
    /*
    - Scared text :
        - hMovementCos = 19f
        - vMovementCos = 10f
    - Down text :
        - hMovementCos = 10f
        - vMovementCos = 10f
    - Sad text :
        - hMovementCos = 7f
        - vMovementCos = 5f
    */

    void Start()
    {
        textMesh = GetComponent<TMP_Text>();
    }

    void Update()
    {
        textMesh.ForceMeshUpdate();
        mesh = textMesh.mesh;
        vertices = mesh.vertices;

        for (int i = 0; i < textMesh.textInfo.characterCount; i++)
        {
            TMP_CharacterInfo c = textMesh.textInfo.characterInfo[i];

            int index = c.vertexIndex;

            Vector3 offset = Wobble(Time.time + i);
            vertices[index] += offset;
            vertices[index + 1] += offset;
            vertices[index + 2] += offset;
            vertices[index + 3] += offset;
        }

        mesh.vertices = vertices;
        textMesh.canvasRenderer.SetMesh(mesh);
    }

    Vector2 Wobble(float time) {
        return new Vector2(Mathf.Sin(time*hMovementCos), Mathf.Cos(time*vMovementCos));
    }
}
