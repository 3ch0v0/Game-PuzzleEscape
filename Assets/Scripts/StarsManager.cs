using UnityEngine;

public class StarManager : MonoBehaviour
{
    public AudioClip collectSound;
    private AudioSource audioSource;
    private bool collected = false;

    void Start()
    {
        //audioSource = FindObjectOfType<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (collected) return;

        if (other.CompareTag("Player"))
        {
            collected = true;

            if (collectSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(collectSound);
            }

            gameObject.SetActive(false);

            // 通知关卡管理器
            FindObjectOfType<LevelManager>()?.CollectStar();
        }
    }
}