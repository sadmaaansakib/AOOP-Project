package com.aoop.backend.model;

import jakarta.persistence.*;

@Entity
@Table(name = "game_states")
public class GameState {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @ManyToOne
    @JoinColumn(name = "player_id", nullable = false)
    private Player player;

    @Column(columnDefinition = "TEXT", nullable = false)
    private String stateData; // JSON string containing the state

    @Column(nullable = false)
    private String gameMode = "default";

    // Constructors
    public GameState() {}

    public GameState(Player player, String stateData, String gameMode) {
        this.player = player;
        this.stateData = stateData;
        this.gameMode = gameMode;
    }

    // Getters and Setters
    public Long getId() { return id; }
    public void setId(Long id) { this.id = id; }

    public Player getPlayer() { return player; }
    public void setPlayer(Player player) { this.player = player; }

    public String getStateData() { return stateData; }
    public void setStateData(String stateData) { this.stateData = stateData; }

    public String getGameMode() { return gameMode; }
    public void setGameMode(String gameMode) { this.gameMode = gameMode; }
}
