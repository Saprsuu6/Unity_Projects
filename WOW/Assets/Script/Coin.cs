using System.Timers;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Timer _timer;
    private Animator _animator;
    private GameObject player;
    private float spawnDistanceMin = 10;
    private float spawnDistanceMax = 20;
    private float coinOffsetY;   // высота появления монеты над землей
    private float respawnTime = 10;  // 10 second
    private float resTime;  // остаток времени до переноса

    [SerializeField]
    private Audio myObjAudio;
    private Audio myAudio;

    void Start()
    {
        setDissapearCoinTimer();
        myAudio = myObjAudio.GetComponent<Audio>();
        _animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
        coinOffsetY = transform.position.y -
            Terrain.activeTerrain.SampleHeight(transform.position);
        resTime = respawnTime;
    }

    private void setDissapearCoinTimer()
    {
        _timer = new Timer(5000);
        _timer.Elapsed += (sender, e) => { if (GameSettings.DisplayDisaparTimer) newRandomCoinPosition(); };
        _timer.AutoReset = true;
        _timer.Enabled = true;
        _timer.Start();
    }

    void LateUpdate()
    {
        float coinDistance = (transform.position - player.transform.position).magnitude;
        if (coinDistance < 5)  // Player is near Coin
        {
            _animator.SetBool(  // Установить логический параметр Аниматора (контроллера)
                "IsNear",       // с именем "IsNear"
                true);          // в значение true (после этого Аниматор сам выберет состояние)
        }
        else
        {
            _animator.SetBool("IsNear", false);
        }
        resTime -= Time.deltaTime;
        if (resTime <= 0)
        {
            // respawn
            resTime = respawnTime;  // новый отсчет
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")  // монета собрана
        {
            _animator.SetBool("IsPicked", true);
        }
        {
            Debug.Log(other.gameObject.name);
        }
    }

    public void Picked()  // событие анимации после исчезновения
    {
        if (!GameSettings.MuteAllSounds)
        {
            Debug.Log(myAudio.money.volume);
            myAudio.money.Play();
        }
        newRandomCoinPosition();
        Player.CoinCount++;
    }

    private void newRandomCoinPosition()
    {
        Vector3 newPosition;
        float distance;
        do
        {
            newPosition = new Vector3(
                transform.position.x + UnityEngine.Random.Range(-spawnDistanceMax, spawnDistanceMax),
                transform.position.y,
                transform.position.z + UnityEngine.Random.Range(-spawnDistanceMax, spawnDistanceMax));
            distance = Vector3.Distance(newPosition, transform.position);
        } while (distance < spawnDistanceMin
                 || distance > spawnDistanceMax
                 || newPosition.x < 11    // край Terrain  \____/
                 || newPosition.z < 11    //             0-11    1000
                 || newPosition.x > 1000 - 11
                 || newPosition.z > 1000 - 11);

        float y = Terrain.activeTerrain.SampleHeight(newPosition) + coinOffsetY;
        newPosition.y = y;


        transform.position = newPosition;
        _animator.SetBool("IsPicked", false);
    }
}