import { useEffect, useState } from "react";
import type { Game } from "../../api/games";
import { fetchGameDetail } from "../../api/gameDetail";
import { useParams } from "react-router-dom";
import { fetchPlayByPlay } from "../../api/playByPlay";
import type { PlayByPlay } from "../../api/playByPlay";
import "./GameDetailPage.css";

export default function GameDetailPage() {
  const { gameId } = useParams();
  const [game, setGame] = useState<Game>();
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");
  const [plays, setPlays] = useState<PlayByPlay[]>();

  useEffect(() => {
    if (!gameId) return;

    const loadData = async () => {
      try {
        setLoading(true);
        setError("");

        const [gameData, playsData] = await Promise.all([
          fetchGameDetail({ gameId }),
          fetchPlayByPlay({ gameId }),
        ]);

        setGame(gameData);
        setPlays(playsData);
      } catch (err) {
        setError(err instanceof Error ? err.message : "An error occurred");
      } finally {
        setLoading(false);
      }
    };

    loadData();
  }, [gameId]);

  if (loading) return <p>Loading games...</p>;
  if (error) return <p>Error: {error}</p>;
  if (!game) return <p>Game not found!</p>;

  console.log("Plays data:", plays);

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
        <div>
          Plays:
          {plays?.map((play, index) => (
            <div key={index}>{play.eventDescription}</div>
          ))}
        </div>
      </div>
    </div>
  );
}
