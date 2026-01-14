import { useEffect, useState } from "react";
import type { Game } from "../../api/games";
import { fetchGameDetail } from "../../api/gameDetail";
import { useParams } from "react-router-dom";
import "./GameDetailPage.css";

export default function GameDetailPage() {
  const { gameId } = useParams();
  const [game, setGame] = useState<Game>();
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  useEffect(() => {
    if (!gameId) return;

    fetchGameDetail({ gameId })
      .then((data) => setGame(data))
      .catch((err) => setError(err.message))
      .finally(() => setLoading(false));
  }, [gameId]);

  if (loading) return <p>Loading games...</p>;
  if (error) return <p>Error: {error}</p>;
  if (!game) return <p>Game not found!</p>;

 return (
    <div className="scoreboard-container">
      <div className="scoreboard">
        <div className="game-status">
          <div className="period">{game.period}</div>
          <div className="clock">{game.clock}</div>
        </div>

        <div className="teams-grid">
          <div className="team">
            <div className="team-name">{game.awayTeam}</div>
            <div className="team-score">{game.awayScore}</div>
          </div>

          <div className="vs-separator">VS</div>

          <div className="team">
            <div className="team-name">{game.homeTeam}</div>
            <div className="team-score">{game.homeScore}</div>
          </div>
        </div>

        <div className="game-id">Game ID: {game.gameId}</div>
      </div>
    </div>
  );
}
