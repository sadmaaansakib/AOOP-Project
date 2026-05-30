package com.aoop.backend.config;

import org.springframework.web.socket.CloseStatus;
import org.springframework.web.socket.TextMessage;
import org.springframework.web.socket.WebSocketSession;
import org.springframework.web.socket.handler.TextWebSocketHandler;

import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.ObjectMapper;

import java.io.IOException;
import java.util.Collections;
import java.util.HashSet;
import java.util.Set;
import java.util.concurrent.ConcurrentHashMap;

public class GameWebSocketHandler extends TextWebSocketHandler {

    // Map of gameMode -> set of sessions
    private static final ConcurrentHashMap<String, Set<WebSocketSession>> rooms = new ConcurrentHashMap<>();
    
    // Map of sessionId -> gameMode (for quick lookup on disconnect)
    private static final ConcurrentHashMap<String, String> sessionRooms = new ConcurrentHashMap<>();

    private final ObjectMapper objectMapper = new ObjectMapper();

    @Override
    public void afterConnectionEstablished(WebSocketSession session) throws Exception {
        System.out.println("New player connected. Session ID: " + session.getId());
    }

    @Override
    protected void handleTextMessage(WebSocketSession session, TextMessage message) throws Exception {
        String payload = message.getPayload();
        try {
            JsonNode jsonNode = objectMapper.readTree(payload);
            
            // Check if it's a join request
            if (jsonNode.has("action") && "join".equals(jsonNode.get("action").asText())) {
                String mode = jsonNode.has("mode") ? jsonNode.get("mode").asText() : "default";
                
                rooms.computeIfAbsent(mode, k -> Collections.synchronizedSet(new HashSet<>())).add(session);
                sessionRooms.put(session.getId(), mode);
                System.out.println("Session " + session.getId() + " joined room: " + mode);
                return; // Don't broadcast the join message itself
            }
            
            // Otherwise, broadcast to the room the player is currently in
            String mode = sessionRooms.get(session.getId());
            if (mode != null) {
                Set<WebSocketSession> roomSessions = rooms.get(mode);
                if (roomSessions != null) {
                    for (WebSocketSession webSocketSession : roomSessions) {
                        if (webSocketSession.isOpen() && !session.getId().equals(webSocketSession.getId())) {
                            try {
                                webSocketSession.sendMessage(message);
                            } catch (IOException e) {
                                System.err.println("Error sending message to session " + webSocketSession.getId());
                            }
                        }
                    }
                }
            } else {
                System.out.println("Session " + session.getId() + " sent message without joining a room first.");
            }
        } catch (Exception e) {
            System.err.println("Error processing WebSocket message: " + e.getMessage());
        }
    }

    @Override
    public void afterConnectionClosed(WebSocketSession session, CloseStatus status) throws Exception {
        String mode = sessionRooms.remove(session.getId());
        if (mode != null) {
            Set<WebSocketSession> roomSessions = rooms.get(mode);
            if (roomSessions != null) {
                roomSessions.remove(session);
            }
        }
        System.out.println("Player disconnected. Session ID: " + session.getId());
    }
}
