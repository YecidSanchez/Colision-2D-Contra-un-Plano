using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCollision : MonoBehaviour {
    public GameObject[] sphere;
    //Guardamos las posiciones vectoriales de cada una pelota en un vector diferente
    float[] x = new float[4];
    float[] y = new float[4];
    float[] z = new float[4];
    
    //
    float[] xm = new float[] { 0.001f,-0.001f };
    float[] zm = new float[] { 0, 0, -0.001f, 0.001f };

    float planex = -1, planey = -8, planez = 3;

    bool[] up = new bool[] { false, false, false, false };
    bool[] fall = new bool[] { false, false, false, false };
    int[] bounce = new int[] { 500, 500, 500, 500 };
    int[] bouncecount = new int[] { 0, 0, 0, 0 };

    float collx, collz;
    int i;

    void Start() {
        for (i = 0; i < 4; i++) {
            x[i] = sphere[i].transform.position.x;
            y[i] = sphere[i].transform.position.y;
            z[i] = sphere[i].transform.position.z;
        }
    }

    void Update() {
        for (i = 0; i < 4; i++) {
            if (i < 2) x[i] += xm[i];
            if (i > 1) z[i] += zm[i];

            sphere[i].transform.position = new Vector3(x[i], y[i], z[i]);

            if (!up[i]) {
                if ((y[i] > planey + 0.5) && !fall[i]) {
                    y[i] -= 0.01f;
                }
                if (y[i] < -7.5f && !fall[i]) {
                    up[i] = true;
                } 
                if (fall[i] && y[i] > -100) {
                    y[i] -= 0.01f;
                } 
            }

            collx = Mathf.Abs(x[i] - planex);
            collz = Mathf.Abs(z[i] - planez);

            if (up[i]) {
                y[i] += 0.01f * bounce[i] / 100; 
                bounce[i]--;

                if ((collx > 5f || collz > 5f) && bounce[i] < 0) { 
                    up[i] = false; 
                    fall[i] = true; 
                }
                if (bounce[i] < 0) { 
                    up[i] = false; 
                    bouncecount[i]++; 
                    bounce[i] = 500 - bouncecount[i] * 100; 
                }
            }
        }
    }
}