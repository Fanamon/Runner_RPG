using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(VideoPlayer))]
public class BackgroundMenu : MonoBehaviour
{
    private Image _image;
    private VideoPlayer _player;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _player = GetComponent<VideoPlayer>();

        StartCoroutine(MenuPlayer());
    }

    private IEnumerator MenuPlayer()
    {
        int frameCount = 2;

        _player.Play();

        for (int i = 0; i < frameCount; i++)
        {
            yield return new WaitForEndOfFrame();
        }

        _image.enabled = false;
    }
}
