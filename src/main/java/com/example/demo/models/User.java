package com.example.demo.models;

public class User {
    private long id;
    private String username;
    private String password;
    private String firstName;
    private String lastName;
    private String role;

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public String getLastName() {
        return lastName;
    }

    public void setLastName(String lastName) {
        this.lastName = lastName;
    }

    public String getRole() {
        return role;
    }

    public void setRole(String role) {
        this.role = role;
    }

    public long getId() {
        return id;
    }

    public void setId(long id) {
        this.id = id;
    }

    public String getUsername() {
        return username;
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public String getFirstName() {
        return firstName;
    }

    public void setFirstName(String firstName) {
        this.firstName = firstName;
    }

    public User(long id, String username, String password, String firstName, String lastName, String role) {
        super();
        this.id = id;
        this.username = username;
        this.firstName = firstName;
        this.password = password;
        this.lastName = lastName;
        this.role = role;
    }

    public User() {
        super();
    }
}