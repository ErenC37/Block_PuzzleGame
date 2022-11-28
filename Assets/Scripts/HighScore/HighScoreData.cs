using System;
using Firebase.Firestore;
using UnityEngine;

[FirestoreData]
public class HighScoreData
{
    [FirestoreProperty] public string NickName { get; set; }
    [FirestoreProperty] public int Score { get; set; }
    
}