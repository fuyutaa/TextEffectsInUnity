using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordWobbleRGB : MonoBehaviour
{
    TMP_Text textMesh;
    Mesh mesh;

    Vector3[] vertices;

    List<int> wordIndexes;
    List<int> wordLengths;

    public bool modifyingColor;
    public Gradient rainbow;

    [Header("Math movement values")]
    public float hMovementSin = 3.3f;
    public float vMovementCos = 2.5f;

    void Start()
    {
        textMesh = GetComponent<TMP_Text>();

        wordIndexes = new List<int>{0};
        wordLengths = new List<int>();

        string s = textMesh.text;
        for (int index = s.IndexOf(' '); index > -1; index = s.IndexOf(' ', index + 1))
        {
                wordLengths.Add(index - wordIndexes[wordIndexes.Count - 1]);
                wordIndexes.Add(index + 1);
        }
        wordLengths.Add(s.Length - wordIndexes[wordIndexes.Count - 1]);
    }

    void Update()
    {
        textMesh.ForceMeshUpdate();
        mesh = textMesh.mesh;
        vertices = mesh.vertices;

        Color[] colors = mesh.colors;

        for (int w = 0; w < wordIndexes.Count; w++)
        {
            int wordIndex = wordIndexes[w];
            Vector3 offset = Wobble(Time.time + w);

            for (int i = 0; i < wordLengths[w]; i++)
            {
                TMP_CharacterInfo c = textMesh.textInfo.characterInfo[wordIndex+i];

                int index = c.vertexIndex;

                if(modifyingColor) // If color modification is activated
                {
                    colors[index] = rainbow.Evaluate(Mathf.Repeat(Time.time + vertices[index].x*0.001f, 1f));
                    colors[index + 1] = rainbow.Evaluate(Mathf.Repeat(Time.time + vertices[index + 1].x*0.001f, 1f));
                    colors[index + 2] = rainbow.Evaluate(Mathf.Repeat(Time.time + vertices[index + 2].x*0.001f, 1f));
                    colors[index + 3] = rainbow.Evaluate(Mathf.Repeat(Time.time + vertices[index + 3].x*0.001f, 1f));
                }

                // Movement
                vertices[index] += offset;
                vertices[index + 1] += offset;
                vertices[index + 2] += offset;
                vertices[index + 3] += offset;                
            }
        }

        

        mesh.vertices = vertices;
        mesh.colors = colors;
        textMesh.canvasRenderer.SetMesh(mesh);
    }

    Vector2 Wobble(float time) {
        return new Vector2(Mathf.Sin(time*hMovementSin), Mathf.Cos(time*vMovementCos));
    }
}
