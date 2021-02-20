using UnityEngine;
using UnityEngine.UI;

public class HpUIScript : MonoBehaviour
{
    float current;
    Image imagefill;
    EntityStats stats;
    GameObject UIContainer;

    // Start is called before the first frame update
    void Start()
    {
        UIContainer = gameObject.transform.Find("EnemyUI").gameObject;
        stats = gameObject.GetComponent<EntityStats>();
        imagefill = UIContainer.transform.Find("HealthContainer/Health").GetComponent<Image>();
        imagefill.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        imagefill.fillAmount = (float)stats.HP / stats.MAXHP;
    }
}