using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Firebase;
using Firebase.Database;
using Firebase.Unity;

public class exam : MonoBehaviour
{
    public class User
    {
        //Json 파일을위한 클래스 정의
        public string username;
        public string email;
        public User(string username, string email)
        {
            this.username = username;
            this.email = email;
        }
    }

    DatabaseReference reference; //데이터 베이스 변수 선언
    int count = 1;

    // Start is called before the first frame update
    void Start()
    {
        //데이터 쓰려면 인스턴스가 필요.
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void OnClickSave() //데이터 저장
    {
        writeNewUser("아이디", "닉네임", "이메일@email.com");
        count++;
    }

    public void LoadBtn()
    {
        readUser("아이디");
    }

    private void writeNewUser(string userId, string name, string email)
    {
        // user변수를 만들고 받아온 name, email반환
        User user = new User(name, email);

        //대입 시킨 user를 json파일로 변환
        string json = JsonUtility.ToJson(user);

        //데이터베이스레퍼런스 변수에 userid를 자식으로 변환된 json 파일을 업로드
        reference.Child(userId).Child("num" + count.ToString()).SetRawJsonValueAsync(json);
    }

    private void readUser(string userId)
    {
        //레퍼런스의 자식을 task 로받아 냄
        reference.Child(userId).GetValueAsync().ContinueWith(task =>
        {
            //실패면 에러
            if(task.IsFaulted)
            {
                Debug.Log("error");
            }
            //성공
            else if(task.IsCompleted)
            {
                //변수선언해 task 결과값을 받음
                DataSnapshot snapshot = task.Result;

                //자식 갯수 확인
                Debug.Log(snapshot.ChildrenCount);

                //IDictionary 이름에 맞게 변수 초기화
                foreach (DataSnapshot data in snapshot.Children)
                {
                    IDictionary personInfo = (IDictionary)data.Value;
                    Debug.Log("email : " + personInfo["email"] + ", username" + personInfo["username"] + ", num :" + personInfo["num"]);

                }
            }
        });
    }
}
