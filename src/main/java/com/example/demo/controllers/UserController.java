package com.example.demo.controllers;

import java.util.ArrayList;
import java.util.List;

import org.springframework.web.bind.annotation.*;

import com.example.demo.models.User;

@RestController
public class UserController {
    static List<User> users = new ArrayList<User>();

    // POST - Creating
    @PostMapping("/api/001642349/users")
    public List<User> createUser(@RequestBody User user) {
        boolean free = false;
        while (!free) {
            for (int i = 0; i < users.size(); i++) {
                if (users.get(i).getId() == user.getId()) {
                    user.setId((long)(Math.random() * 1000000));
                    i = 0;
                }
            }
            free = true;
        }
        users.add(user);
        return users;
    }

    // GET ALL
    @GetMapping("/api/001642349/users")
    public List<User> findAllUsers() {
        for (User user:
             users) {
            System.out.println(user.getFirstName());
        }
        return users;
    }

    // GET
    @GetMapping("/api/001642349/users/{id}")
    public User findUser(@PathVariable("id") String id) {
        for (User user: users) {
            if (user.getId() == Long.parseLong(id)) {
                return user;
            }
        }
        return null;
    }

    // EDIT
    @PutMapping("/api/001642349/users/{id}")
    public List<User> editUser(@PathVariable("id") String id, @RequestBody User user) {
        for (int i = 0; i < users.size(); i++) {
            if (users.get(i).getId() == Long.parseLong(id)) {
                users.set(i, user);
            }
        }
        return users;
    }

    // DELETE - Deleting
    @DeleteMapping("/api/001642349/users/{id}")
    public List<User> deleteUser(@PathVariable("id") String id) {
        int idx = -1;
        for (int i = 0; i < users.size(); i++) {
            if (users.get(i).getId() == Long.parseLong(id)) {
                idx = i;
                break;
            }
        }
        if (idx >= 0) {
            users.remove(idx);
        }
        return users;
    }
}