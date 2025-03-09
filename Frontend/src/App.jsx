import React, { useEffect, useState } from "react";
import * as signalR from "@microsoft/signalr";
import { useNavigate } from "react-router-dom";

const TOPIC_NAMES = {
  "mixing_components_producer": "Подготовка и смешивание компонентов",
  "molding_and_initial_exposure_producer": "Формование и первичная выдержка",
  "cutting_array_producer": "Резка массива",
  "autoclaving_producer": "Автоклавирование и окончательная обработка",
};

const KafkaMessages = () => {
  const [messages, setMessages] = useState(
    Object.keys(TOPIC_NAMES).reduce((acc, topic) => ({ ...acc, [topic]: [] }), {})
  );

  const navigate = useNavigate();

  useEffect(() => {
    const connection = new signalR.HubConnectionBuilder()
      .withUrl("http://localhost:5000/kafkaHub")
      .withAutomaticReconnect()
      .build();

    console.log("🔗 SignalR: Подключение к серверу...");

    connection
      .start()
      .then(() => console.log("✅ SignalR: Подключение установлено"))
      .catch(err => console.error("❌ SignalR: Ошибка подключения", err));

    connection.on("ReceiveMessage", (topic, message) => {
      console.log(`📩 Получено сообщение: [${topic}] ${message}`);

      setMessages(prev => ({
        ...prev,
        [topic]: [message, ...(prev[topic] || [])],
      }));
    });

    connection.onclose(() => console.warn("⚠️ SignalR: Подключение закрыто"));

    return () => {
      console.log("🔌 SignalR: Остановка подключения");
      connection.stop();
    };
  }, []);

  return (
    <div className="container mt-4">
      <h2 className="text-center text-primary mb-4">
        Текущие параметры этапов технологического процесса производства газобетонных блоков
      </h2>
      <div className="text-left mb-3">
        <button className="btn btn-success" onClick={() => window.location.href = "http://localhost:8080"}>
          Перейти в отчёты
        </button>
      </div>
      
      <div className="row justify-content-center mb-4">
        {Object.entries(TOPIC_NAMES)
          .slice(0, 2)
          .map(([topic, displayName]) => (
            <div key={topic} className="col-lg-6 col-md-6 col-sm-12 d-flex align-items-stretch">
              <div className="card shadow w-100">
                <div className="card-header text-center bg-primary text-white">
                  <h5 className="m-0">{displayName}</h5>
                </div>
                <div className="card-body" style={{ height: "300px", overflowY: "auto" }}>
                  <ul className="list-group list-group-flush">
                    {messages[topic].map((msg, index) => (
                      <li
                        key={index}
                        className={`list-group-item ${index === 0 ? "bg-warning text-dark" : ""}`}
                      >
                        {msg}
                      </li>
                    ))}
                  </ul>
                </div>
              </div>
            </div>
          ))}
      </div>
      <div className="row justify-content-center">
        {Object.entries(TOPIC_NAMES)
          .slice(2, 4)
          .map(([topic, displayName]) => (
            <div key={topic} className="col-lg-6 col-md-6 col-sm-12 d-flex align-items-stretch">
              <div className="card shadow w-100">
                <div className="card-header text-center bg-primary text-white">
                  <h5 className="m-0">{displayName}</h5>
                </div>
                <div className="card-body" style={{ height: "300px", overflowY: "auto" }}>
                  <ul className="list-group list-group-flush">
                    {messages[topic].map((msg, index) => (
                      <li
                        key={index}
                        className={`list-group-item ${index === 0 ? "bg-warning text-dark" : ""}`}
                      >
                        {msg}
                      </li>
                    ))}
                  </ul>
                </div>
              </div>
            </div>
          ))}
      </div>
    </div>
  );
};

export default KafkaMessages;
