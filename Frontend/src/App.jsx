
import React, { useEffect, useState } from "react";
import * as signalR from "@microsoft/signalr";

const KafkaMessages = () => {
  const [messages, setMessages] = useState([]);

  useEffect(() => {
    const connection = new signalR.HubConnectionBuilder()
      .withUrl("http://localhost:5000/kafkaHub")
      .withAutomaticReconnect()
      .build();

    connection.start().catch(err => console.error(err));

    connection.on("ReceiveMessage", (topic, message) => {
      setMessages(prev => [`[${topic}] ${message}`, ...prev]);
    });

    return () => connection.stop();
  }, []);

  return (
    <div>
      <h2>Сообщения из Kafka</h2>
      <ul>
        {messages.map((msg, index) => (
          <li key={index}>{msg}</li>
        ))}
      </ul>
    </div>
  );
};

export default KafkaMessages;
