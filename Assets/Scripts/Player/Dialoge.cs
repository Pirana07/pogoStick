using UnityEngine;
using TMPro;
using System.Collections;

public class DialogSystem : MonoBehaviour
{
    [SerializeField] private GameObject dialogBox;
    [SerializeField] private GameObject PlayUI;
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private string[] dialogLines;
    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private RectTransform playerImage;
    [SerializeField] private Sprite[] playerImages;
    [SerializeField] private float scaleMultiplier = 1.1f;
    [SerializeField] private float shakeSpeed = 2f;
    [SerializeField] private AudioClip typingSound; // Typing sound
    [SerializeField] private AudioClip skipSound; // Skip sound
    [SerializeField] private AudioSource audioSource; // AudioSource component
    [SerializeField] private Animator _animator; // Animator


    private int currentLineIndex;
    private bool isTyping;
    private string currentLine;
    private Coroutine typingCoroutine;
    private Coroutine animationCoroutine;
    private Vector3 originalScale;

    void Start()
    {
        _animator.enabled = false;
        dialogBox.SetActive(true);
        originalScale = playerImage.localScale;
    }

    public void StartDialog()
    {
        currentLineIndex = 0;
        dialogBox.SetActive(true);
        ShowNextLine();
    }

    public void ShowNextLine()
    {
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            dialogText.text = currentLine;
            isTyping = false;
            StopPlayerAnimation();

            // Play skip sound when skipping
            if (skipSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(skipSound);
            }
            return;
        }

        if (currentLineIndex < dialogLines.Length)
        {
            currentLine = dialogLines[currentLineIndex];
            typingCoroutine = StartCoroutine(TypeLine(currentLine));
            audioSource.PlayOneShot(typingSound); // Play the typing sound

            ChangePlayerImage(currentLineIndex);

            StartPlayerAnimation();
            currentLineIndex++;
        }
        else
        {
            EndDialog();
            PlayUI.SetActive(true);
            audioSource.Stop();
            _animator.enabled = true;
        }
    }

    public void EndDialog()
    {
        dialogBox.SetActive(false);
        StopPlayerAnimation();
        audioSource.Stop();
      
    }

    private IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogText.text = "";
        foreach (char letter in line.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
        StopPlayerAnimation();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && dialogBox.activeSelf)
        {
            ShowNextLine();
        }
    }

    private void StartPlayerAnimation()
    {
        if (animationCoroutine != null)
            StopCoroutine(animationCoroutine);
        animationCoroutine = StartCoroutine(AnimatePlayerImage());
    }

    private void StopPlayerAnimation()
    {
        if (animationCoroutine != null)
            StopCoroutine(animationCoroutine);
        ResetPlayerImageScale();
        audioSource.Stop();
    }

    private IEnumerator AnimatePlayerImage()
    {
        Vector3 targetScale = originalScale * scaleMultiplier;

        while (true)
        {
            playerImage.localScale = Vector3.Lerp(playerImage.localScale, targetScale, shakeSpeed * Time.deltaTime);
            yield return null;

            playerImage.localScale = Vector3.Lerp(playerImage.localScale, originalScale, shakeSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void ResetPlayerImageScale()
    {
        if (playerImage != null)
            playerImage.localScale = originalScale;
    }

    private void ChangePlayerImage(int lineIndex)
    {
        if (playerImages != null && lineIndex < playerImages.Length)
        {
            playerImage.GetComponent<UnityEngine.UI.Image>().sprite = playerImages[lineIndex];
        }
    }
}
