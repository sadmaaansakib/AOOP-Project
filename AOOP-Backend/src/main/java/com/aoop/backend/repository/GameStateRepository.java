package com.aoop.backend.repository;

import com.aoop.backend.model.GameState;
import com.aoop.backend.model.Player;
import org.springframework.data.jpa.repository.JpaRepository;
import java.util.Optional;

public interface GameStateRepository extends JpaRepository<GameState, Long> {
    Optional<GameState> findByPlayerAndGameMode(Player player, String gameMode);
}
