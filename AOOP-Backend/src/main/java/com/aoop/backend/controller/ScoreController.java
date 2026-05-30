package com.aoop.backend.controller;

import com.aoop.backend.model.Player;
import com.aoop.backend.model.Score;
import com.aoop.backend.repository.PlayerRepository;
import com.aoop.backend.repository.ScoreRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.security.core.Authentication;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.stream.Collectors;

@RestController
@RequestMapping("/api/scores")
public class ScoreController {

    @Autowired
    private ScoreRepository scoreRepository;

    @Autowired
    private PlayerRepository playerRepository;

    @PostMapping
    public ResponseEntity<?> submitScore(@RequestParam int score, @RequestParam(defaultValue = "default") String gameMode, Authentication authentication) {
        String username = authentication.getName();
        Player player = playerRepository.findByUsername(username).orElse(null);
        if (player == null) {
            return ResponseEntity.badRequest().body("Player not found");
        }

        Score newScore = new Score(player, score, gameMode);
        scoreRepository.save(newScore);
        return ResponseEntity.ok("Score submitted successfully");
    }

    @GetMapping("/leaderboard")
    public ResponseEntity<List<LeaderboardEntry>> getLeaderboard(@RequestParam(defaultValue = "default") String gameMode) {
        List<Score> topScores = scoreRepository.findTop10ByGameModeOrderByScoreDesc(gameMode);
        List<LeaderboardEntry> leaderboard = topScores.stream()
                .map(s -> new LeaderboardEntry(s.getPlayer().getUsername(), s.getScore()))
                .collect(Collectors.toList());
        
        return ResponseEntity.ok(leaderboard);
    }

    // DTO class to avoid exposing the whole Player entity in the response
    public static class LeaderboardEntry {
        public String username;
        public int score;

        public LeaderboardEntry(String username, int score) {
            this.username = username;
            this.score = score;
        }
    }
}
