package com.aoop.backend.controller;

import com.aoop.backend.model.GameState;
import com.aoop.backend.model.Player;
import com.aoop.backend.repository.GameStateRepository;
import com.aoop.backend.repository.PlayerRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.security.core.Authentication;
import org.springframework.web.bind.annotation.*;

import java.util.Optional;

@RestController
@RequestMapping("/api/state")
public class GameStateController {

    @Autowired
    private GameStateRepository gameStateRepository;

    @Autowired
    private PlayerRepository playerRepository;

    @PostMapping("/save")
    public ResponseEntity<?> saveState(@RequestBody String stateData, @RequestParam(defaultValue = "default") String gameMode, Authentication authentication) {
        String username = authentication.getName();
        Player player = playerRepository.findByUsername(username).orElse(null);
        if (player == null) {
            return ResponseEntity.badRequest().body("Player not found");
        }

        Optional<GameState> existingState = gameStateRepository.findByPlayerAndGameMode(player, gameMode);
        if (existingState.isPresent()) {
            GameState state = existingState.get();
            state.setStateData(stateData);
            gameStateRepository.save(state);
        } else {
            GameState newState = new GameState(player, stateData, gameMode);
            gameStateRepository.save(newState);
        }

        return ResponseEntity.ok("Game state saved successfully");
    }

    @GetMapping("/load")
    public ResponseEntity<?> loadState(@RequestParam(defaultValue = "default") String gameMode, Authentication authentication) {
        String username = authentication.getName();
        Player player = playerRepository.findByUsername(username).orElse(null);
        if (player == null) {
            return ResponseEntity.badRequest().body("Player not found");
        }

        Optional<GameState> state = gameStateRepository.findByPlayerAndGameMode(player, gameMode);
        if (state.isPresent()) {
            return ResponseEntity.ok(state.get().getStateData());
        } else {
            return ResponseEntity.notFound().build();
        }
    }
}
