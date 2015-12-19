using UnityEngine;
using System.Collections;

public class KeyCardScript : MonoBehaviour
{
    public GameObject cop1key;
    public GameObject cop2key;
    public GameObject cop3key;
    public GameObject cop4key;
    public GameObject cop5key;
    public GameObject cop6key;

    private bool cop1_haskey;
    private bool cop2_haskey;
    private bool cop3_haskey;
    private bool cop4_haskey;
    private bool cop5_haskey;
    private bool cop6_haskey;
    
    void Start()
    {
        cop1_haskey = false; cop1key.SetActive(false);
        cop2_haskey = false; cop2key.SetActive(false);
        cop3_haskey = false; cop3key.SetActive(false);
        cop4_haskey = false; cop4key.SetActive(false);
        cop5_haskey = false; cop5key.SetActive(false);
        cop6_haskey = false; cop6key.SetActive(false);

        int x = Random.Range(1, 6);

        if (x == 1)
        {
            cop1_haskey = true; cop1key.SetActive(true);
            cop2_haskey = false; cop2key.SetActive(false);
            cop3_haskey = false; cop3key.SetActive(false);
            cop4_haskey = false; cop4key.SetActive(false);
            cop5_haskey = false; cop5key.SetActive(false);
            cop6_haskey = false; cop6key.SetActive(false);
        }
        else if (x == 2)
        {
            cop1_haskey = false; cop1key.SetActive(false);
            cop2_haskey = true; cop2key.SetActive(true);
            cop3_haskey = false; cop3key.SetActive(false);
            cop4_haskey = false; cop4key.SetActive(false);
            cop5_haskey = false; cop5key.SetActive(false);
            cop6_haskey = false; cop6key.SetActive(false);
        }
        else if (x == 3)
        {
            cop1_haskey = false; cop1key.SetActive(false);
            cop2_haskey = false; cop2key.SetActive(false);
            cop3_haskey = true; cop3key.SetActive(true);
            cop4_haskey = false; cop4key.SetActive(false);
            cop5_haskey = false; cop5key.SetActive(false);
            cop6_haskey = false; cop6key.SetActive(false);
        }
        else if (x == 4)
        {
            cop1_haskey = false; cop1key.SetActive(false);
            cop2_haskey = false; cop2key.SetActive(false);
            cop3_haskey = false; cop3key.SetActive(false);
            cop4_haskey = true; cop4key.SetActive(true);
            cop5_haskey = false; cop5key.SetActive(false);
            cop6_haskey = false; cop6key.SetActive(false);
        }
        else if (x == 5)
        {
            cop1_haskey = false; cop1key.SetActive(false);
            cop2_haskey = false; cop2key.SetActive(false);
            cop3_haskey = false; cop3key.SetActive(false);
            cop4_haskey = false; cop4key.SetActive(false);
            cop5_haskey = true; cop5key.SetActive(true);
            cop6_haskey = false; cop6key.SetActive(false);
        }
        else if (x == 6)
        {
            cop1_haskey = false; cop1key.SetActive(false);
            cop2_haskey = false; cop2key.SetActive(false);
            cop3_haskey = false; cop3key.SetActive(false);
            cop4_haskey = false; cop4key.SetActive(false);
            cop5_haskey = false; cop5key.SetActive(false);
            cop6_haskey = true; cop6key.SetActive(true);
        }


    }

}
