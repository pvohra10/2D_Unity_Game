using UnityEngine;

public class playMusic : MonoBehaviour
{
    private AudioSource self;
    private AudioClip music;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        self = gameObject.GetComponent<AudioSource>();
        music = Resources.Load<AudioClip>("Music/gameOst");
        self.clip = music;
        self.loop = true;
        self.Play();
    }

    // Update is called once per frame
    void Update()
    {
        



    }
}
