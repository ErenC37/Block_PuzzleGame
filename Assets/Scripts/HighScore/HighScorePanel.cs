using System;
using Firebase.Extensions;
using Firebase.Firestore;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class HighScorePanel : MonoBehaviour
{
    private FirebaseFirestore db;


    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private Text highScoreText;
    private int i = 1;

    [SerializeField] private string nickName;


    private void Start()
    {
        db = FirebaseFirestore.DefaultInstance;

        HighScoreData temp = new HighScoreData()
        {
            NickName = nickName,
            Score = Scores.currentScores_
        };


       

        db.Collection("HighScore").Document().SetAsync(temp).ContinueWith(task =>
            {
                if (task.IsCanceled || task.IsFaulted)
                {
                    Debug.Log("Send Message error" + task.Exception);
                }
            }
        );


        Query docRef = db.Collection("HighScore").OrderByDescending("Score").Limit(10);


        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.Log(task.Exception);
            }

            QuerySnapshot capitalQuerySnapshot = task.Result;
            foreach (DocumentSnapshot documentSnapshot in capitalQuerySnapshot.Documents)
            {
                var highScoreData = documentSnapshot.ConvertTo<HighScoreData>();
                textMeshProUGUI.text += $"{i}.{highScoreData.NickName} {highScoreData.Score} \n";
                i++;
            }
        });
    }

    public void OpenOrClosePanel()
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
    }
}