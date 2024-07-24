using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
    public int Minimum;
    public int Maximum = 100;
    public int Current;
    public Image Mask;
    public Image Fill;
    public Color Color;

#if UNITY_EDITOR
    [MenuItem("GameObject/UI/Linear Progress Bar")]
    public static void AddLinearPrgressBar()
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("UI/LinearProgressBar"));
        obj.transform.SetParent(Selection.activeGameObject.transform, false);
    }

    [MenuItem("GameObject/UI/Radial Progress Bar")]
    public static void AddRadialPrgressBar()
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("UI/RadialProgressBar"));
        obj.transform.SetParent(Selection.activeGameObject.transform, false);
    }
#endif

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FillCurrent();
    }

    void FillCurrent()
    {
        float currentOffset = Current - Minimum;
        float maximumOffset = Maximum - Minimum;

        float fillAmount = currentOffset / maximumOffset;
        Mask.fillAmount = fillAmount;

        Fill.color = Color;
    }
}
