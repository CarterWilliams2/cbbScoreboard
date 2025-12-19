import { useEffect, useState } from "react";
import { fetchGames, type Game, type GameStatus } from "../api/games";

export default function ScoresPage() {
  const [games, setGames] = useState<Game[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");
  const [status, setStatus] = useState<GameStatus | undefined>();

  useEffect(() => {
    fetchGames({ status })
      .then((data) => setGames(data.items))
      .catch((err) => setError(err.message))
      .finally(() => setLoading(false));
  }, [status]);

  if (loading) return <p>Loading games...</p>;
  if (error) return <p>Error: {error}</p>;

  return (
    <div style={{ padding: 20 }}>
      <h1>College Basketball Scoreboard</h1>

      <div style={{ marginBottom: 16 }}>
        <button onClick={() => setStatus(undefined)}>All</button>
        <button onClick={() => setStatus("upcoming")}>Upcoming</button>
        <button onClick={() => setStatus("live")}>Live</button>
        <button onClick={() => setStatus("final")}>Final</button>
      </div>

      {games.map((game) => (
        <div key={game.gameId} style={{ marginBottom: 12 }}>
          <strong>{game.awayTeam}</strong> @ <strong>{game.homeTeam}</strong>
          <div>
            {game.awayScore} - {game.homeScore} ({game.status})
          </div>
        </div>
      ))}
    </div>
  );
}
