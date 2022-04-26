using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    public string username;
    public string email;
    public string inventory;

    public User() {
    }

    public User(string username, string email, string inventory) {
        this.username = username;
        this.email = email;
        this.inventory = inventory;
    }
}
