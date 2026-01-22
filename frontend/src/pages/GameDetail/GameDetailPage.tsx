import { useEffect, useState } from "react";
import type { Game } from "../../api/games";
import { fetchGameDetail } from "../../api/gameDetail";
import { useParams } from "react-router-dom";
import { fetchPlayByPlay } from "../../api/playByPlay";
import type { PlayByPlay } from "../../api/playByPlay";
import "./GameDetailPage.css";
import { fetchWinProbability } from "../../api/winProbability";
import type { WinProbability } from "../../api/winProbability";
import { formatWinPercent } from "./GameDetailService";

export default function GameDetailPage() {
  const { gameId } = useParams();
  const [game, setGame] = useState<Game>();
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");
  const [plays, setPlays] = useState<PlayByPlay[]>();
  const [winProbability, setWinProbability] = useState<WinProbability>();

 useEffect(() => {
    if (!gameId) return;

    const loadData = async () => {
      try {
        setError("");

        const gameData = await fetchGameDetail({ gameId });
        setGame(gameData);

        if (gameData.status != 0) {
          const playsData = await fetchPlayByPlay({ gameId });
          setPlays(playsData);
          
          if (playsData && playsData.length > 0) {
            const mostRecentPlay = playsData[playsData.length - 1];
            const winProb = await fetchWinProbability({ play: mostRecentPlay });
            setWinProbability(winProb);
          }
        }
      } catch (err) {
        setError(err instanceof Error ? err.message : "An error occurred");
      } finally {
        setLoading(false);
      }
    };

    loadData();
    const interval = setInterval(loadData, 10000);

    return () => clearInterval(interval);
  }, [gameId]);

  if (loading) return <p>Loading game details...</p>;
  if (error) return <p>Error: {error}</p>;
  if (!game) return <p>Game not found!</p>;

  const selectPlays = plays?.slice().reverse().slice(0, 10);

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
            <div>{formatWinPercent(winProbability?.away_win_probability)}</div>
          </div>

          <div className="vs-separator">VS</div>

          <div className="team">
            <div className="team-name">{game.homeTeam}</div>
            <div className="team-score">{game.homeScore}</div>
            <div>{formatWinPercent(winProbability?.home_win_probability)}</div>
          </div>
        </div>

        <div className="game-id">Game ID: {game.gameId}</div>
        <div>
          Plays:
          {selectPlays?.map((play, index) => (
            <div key={index}>{play.eventDescription}</div>
          ))}
        </div>
      </div>
    </div>
  );
}
