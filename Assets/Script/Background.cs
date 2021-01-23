using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {

    public Gradient[] gradients;

    public float scale;

    public Texture2D GenerateNebula(int width, int height)
    {
        Texture2D t = new Texture2D(width, height, TextureFormat.ARGB32, false);
        Gradient gradient = gradients[Random.Range(0, gradients.Length - 1)];

        float offsetX = Random.Range(0, 99999);
        float offsetY = Random.Range(0, 99999);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float xCoord = (float)x / width * scale + offsetX;
                float yCoord = (float)y / height * scale + offsetY;
                float sample = Mathf.PerlinNoise(xCoord, yCoord);

                t.SetPixel(x,y, gradient.Evaluate(sample));


                
                
            }
        }

        t = Smooth(t);

        t.Apply();

        return t;

    }


    private Texture2D Smooth(Texture2D t)
    {
        float offsetX = Random.Range(0, 99999);
        float offsetY = Random.Range(0, 99999);

        for (int x = 0; x < t.width; x++)
        {
            for (int y = 0; y < t.height; y++)
            {
                float xCoord = (float)x / t.width * (scale*4) + offsetX;
                float yCoord = (float)y / t.height * (scale*4) + offsetY;
                float sample = Mathf.PerlinNoise(xCoord, yCoord);

                Color c = t.GetPixel(x, y);

                c.a = sample;

                t.SetPixel(x, y, c);
            }
        }

        return t;
    }

    private Texture2D SmoothEdges(Texture2D t)
    {
        const float procent = 0.05f;

        Vector2 topRight = new Vector2(t.width - (t.width * procent),t.height - (t.height * procent));
        Vector2 bottomLeft = new Vector2(t.width * procent,t.height * procent);
        Vector2 TopLeft = new Vector2(bottomLeft.x, topRight.y);
        Vector2 bottomRight = new Vector2(topRight.x, bottomLeft.y);

        for (int x = 0; x < t.width; x++)
        {
            for (int y = 0; y < t.height; y++)
            {
                
            }
        }

        t.Apply();

        return t;
    }



}
