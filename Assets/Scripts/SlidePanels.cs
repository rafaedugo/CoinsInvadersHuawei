using UnityEngine;

public class SlidePanels : MonoBehaviour
{
    [SerializeField] [Range(0, 3)] private float animationTime;
    [SerializeField] private Transform[] horizontalSlideElements;
    [SerializeField] private GameObject rightBtn, leftBtn;
    private bool slideMove = true;
    private float timer;
    private float movePixels;
    private Vector3[] initialPositions;

    private void Awake() => movePixels = Mathf.Abs(horizontalSlideElements[0].position.x - horizontalSlideElements[1].position.x);

    private void Start()
    {
        initialPositions = new Vector3[horizontalSlideElements.Length];
        for (int i = 0; i < initialPositions.Length; i++)
            initialPositions[i] = horizontalSlideElements[i].localPosition;
    }

    public void ResetPositions()
    {
        for (int i = 0; i < initialPositions.Length; i++)
            horizontalSlideElements[i].localPosition = initialPositions[i];

        horizontalSlideElements[0].gameObject.SetActive(true);
        for (int i = 1; i < horizontalSlideElements.Length; i++)
            horizontalSlideElements[i].gameObject.SetActive(false);
    }

    public System.Collections.IEnumerator DeactivateOtherPanels()
    {
        yield return new WaitUntil(() => slideMove == true);
        foreach (Transform _transform in horizontalSlideElements)
            if (Mathf.RoundToInt(_transform.localPosition.x) != 0)
                _transform.gameObject.SetActive(false);
    }

    public void LeftSlide(UnityEngine.Video.VideoPlayer player)
    {
        for(int i = 0; i < horizontalSlideElements.Length;i++)
        {
            if (Mathf.RoundToInt(horizontalSlideElements[i].localPosition.x) == 0)
            {
                horizontalSlideElements[i + 1].gameObject.SetActive(true);
                break;
            }    
        }
            
        foreach (Transform _transform in horizontalSlideElements)
        {
            if (slideMove)
                _transform.LeanMoveX(_transform.position.x - movePixels, animationTime).setEaseInBack();
        }
        slideMove = false;
        StartCoroutine(DeactivateOtherPanels());
    }
    public void RightSlide()
    {
        for (int i = 0; i < horizontalSlideElements.Length; i++)
        {
            if (Mathf.RoundToInt(horizontalSlideElements[i].localPosition.x) == 0)
            {
                horizontalSlideElements[i - 1].gameObject.SetActive(true);
                break;
            }
        }
        foreach (Transform _transform in horizontalSlideElements)
        {
            if (slideMove)
                _transform.LeanMoveX(_transform.position.x + movePixels, animationTime).setEaseInBack();   
        }
        slideMove = false;
        StartCoroutine(DeactivateOtherPanels());
    }

    public void CheckRightOrLeft()
    {
        if (Mathf.RoundToInt(horizontalSlideElements[0].localPosition.x) == 0)
            leftBtn.SetActive(false);
        else leftBtn.SetActive(true);

        if (Mathf.RoundToInt(horizontalSlideElements[horizontalSlideElements.Length - 1].localPosition.x) == 0)
            rightBtn.SetActive(false);
        else rightBtn.SetActive(true);
    }

    private void Update()
    {
        if(slideMove == false)
        {
            timer += Time.deltaTime;
            if(timer >= .7f)
                CheckRightOrLeft();
            if (timer >= .9f)
            {
                timer = 0;
                slideMove = true;
            }
        }  
    }
}