package com.aoop.backend.repository;

import com.aoop.backend.model.Score;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.List;

public interface ScoreRepository extends JpaRepository<Score, Long> {
    
    List<Score> findTop10ByOrderByScoreDesc();
    List<Score> findTop10ByGameModeOrderByScoreDesc(String gameMode);
}
