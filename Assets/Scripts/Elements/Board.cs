using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Board : MonoBehaviour {

    //! References
    [SerializeField] private TextMeshProUGUI messageText;


    //! MonoBehaviour
    void Update() {
        MoveUp();
    }


    //! Board - Public
    public void Initialize(string message, int fontSize) {
        messageText.text = message;
        messageText.fontSize = fontSize;
        gameObject.SetActive(true);
        StartCoroutine("RemoveFromGame");
    }

    //! Board - Private
    private void MoveUp() {
        transform.Translate(Vector3.up * Config.BOARD_SPEED * Time.deltaTime);
    }

    private IEnumerator RemoveFromGame() {
        yield return new WaitForSeconds(Config.BOARD_TTL);
        gameObject.SetActive(false);
    }
}
