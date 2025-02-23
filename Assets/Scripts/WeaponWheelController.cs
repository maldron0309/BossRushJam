using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponWheelController : MonoBehaviour
{
    public static WeaponWheelController instance;
    public WeaponsInventory inv;
    public Image[] slots;

    private void Awake()
    {
        instance = this;
    }
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
        for (int i = 0; i < inv.slots.Length; i++)
        {
            slots[i].sprite = inv.slots[i].weapon.weaponImage;
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
        yield return new WaitForSeconds(0.01f);

        for (int i = 0; i < 20; i++)
        {
            gameObject.transform.Rotate(0f, 0f, -(targetDeg+startingDeg)/20);
            yield return new WaitForSeconds(0.01f);
            foreach (var item in slots)
            {
                item.transform.rotation = Quaternion.identity;
            }
            //Debug.Log(gameObject.transform.rotation.eulerAngles.z);
        }
        //Debug.Log(targetDeg);
        //Debug.Log(startingDeg);


    }
}
