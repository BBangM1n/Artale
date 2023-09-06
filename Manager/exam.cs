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
        //Json ���������� Ŭ���� ����
        public string username;
        public string email;
        public User(string username, string email)
        {
            this.username = username;
            this.email = email;
        }
    }

    DatabaseReference reference; //������ ���̽� ���� ����
    int count = 1;

    // Start is called before the first frame update
    void Start()
    {
        //������ ������ �ν��Ͻ��� �ʿ�.
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void OnClickSave() //������ ����
    {
        writeNewUser("���̵�", "�г���", "�̸���@email.com");
        count++;
    }

    public void LoadBtn()
    {
        readUser("���̵�");
    }

    private void writeNewUser(string userId, string name, string email)
    {
        // user������ ����� �޾ƿ� name, email��ȯ
        User user = new User(name, email);

        //���� ��Ų user�� json���Ϸ� ��ȯ
        string json = JsonUtility.ToJson(user);

        //�����ͺ��̽����۷��� ������ userid�� �ڽ����� ��ȯ�� json ������ ���ε�
        reference.Child(userId).Child("num" + count.ToString()).SetRawJsonValueAsync(json);
    }

    private void readUser(string userId)
    {
        //���۷����� �ڽ��� task �ι޾� ��
        reference.Child(userId).GetValueAsync().ContinueWith(task =>
        {
            //���и� ����
            if(task.IsFaulted)
            {
                Debug.Log("error");
            }
            //����
            else if(task.IsCompleted)
            {
                //���������� task ������� ����
                DataSnapshot snapshot = task.Result;

                //�ڽ� ���� Ȯ��
                Debug.Log(snapshot.ChildrenCount);

                //IDictionary �̸��� �°� ���� �ʱ�ȭ
                foreach (DataSnapshot data in snapshot.Children)
                {
                    IDictionary personInfo = (IDictionary)data.Value;
                    Debug.Log("email : " + personInfo["email"] + ", username" + personInfo["username"] + ", num :" + personInfo["num"]);

                }
            }
        });
    }
}
