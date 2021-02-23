using UnityEngine;
using UnityEngine.UI;

public class HpUIScript : MonoBehaviour
{
    public GameObject UIContainer;
    float current;
    Image imagefill;
    Text nameText;
    EntityStats stats;

    // Start is called before the first frame update
    void Start()
    {
        stats = gameObject.GetComponent<EntityStats>();
        imagefill = UIContainer.transform.Find("HealthContainer/Health").GetComponent<Image>();
        imagefill.fillAmount = 1;

        if (UIContainer.transform.Find("Name"))
        {
            nameText = UIContainer.transform.Find("Name").GetComponent<Text>();
            nameText.text = gameObject.name;
        }
    }

    // Update is called once per frame
    void Update()
    {
        imagefill.fillAmount = (float)stats.HP / stats.MAXHP;
    }
}