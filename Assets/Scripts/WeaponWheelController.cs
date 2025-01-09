using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponWheelController : MonoBehaviour
{
    public WeaponsInventory inv;
    public Image[] slots;
    
    // Start is called before the first frame update
    void Start()
    {
        updateWheel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateWheel()
    {
        for (int i = 0; i < 8; i++)
        {
            slots[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < inv.weapons.Length; i++)
        {
            slots[i].sprite = inv.weapons[i].weaponImage;
            slots[i].gameObject.SetActive(true);
        }
        gameObject.transform.rotation = Quaternion.identity;
    }

    public IEnumerator RotateWheel(float targetDeg)
    {
        
        float startingDeg = gameObject.transform.rotation.eulerAngles.z;
        if(startingDeg > 1)
        {
            startingDeg -= 360;
        }
        if(targetDeg == 0)
        {
            
            targetDeg = 360;
        }
        for(int i = 0; i < 20; i++)
        {
            gameObject.transform.Rotate(0f, 0f, -(targetDeg+startingDeg)/20);
            yield return new WaitForSeconds(0.01f);
        }
        //Debug.Log(targetDeg);
        //Debug.Log(startingDeg);

    }
}
