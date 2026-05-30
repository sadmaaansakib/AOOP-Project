package com.aoop.backend.controller;

import com.aoop.backend.model.Player;
import com.aoop.backend.repository.PlayerRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/api/auth")
public class AuthController {

    @Autowired
    private PlayerRepository playerRepository;

    @Autowired
    private PasswordEncoder passwordEncoder;

    @PostMapping("/register")
    public ResponseEntity<?> registerUser(@RequestParam String username, @RequestParam String password) {
        if (playerRepository.findByUsername(username).isPresent()) {
            return ResponseEntity.badRequest().body("Username is already taken.");
        }

        Player newPlayer = new Player();
        newPlayer.setUsername(username);
        newPlayer.setPassword(passwordEncoder.encode(password));
        playerRepository.save(newPlayer);

        return ResponseEntity.ok("User registered successfully.");
    }

    @GetMapping("/login")
    public ResponseEntity<?> loginUser() {
        // Because of Basic Auth in SecurityConfig, getting past the filter means credentials are correct.
        return ResponseEntity.ok("Login successful.");
    }
}
