import { useEffect, useState } from "react";
import { fetchGames, type Game } from "./api/games";


function App() {
  const [games, setGames] = useState<Game[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  useEffect(() => {
    fetchGames()
      .then(data => setGames(data.items))
      .catch(err => setError(err.message))
      .finally(() => setLoading(false));
  }, []);

  if (loading) return <p>Loading games...</p>;
  if (error) return <p>Error: {error}</p>;

  return (
    <div style={{ padding: 20}}>
      <h1>College Basketball Scoreboard</h1>

      {games.map(game => (
        <div key={game.gameId} style={{ marginBottom: 12}}>
          <strong>{game.awayTeam}</strong> @ {" "}
          <strong>{game.homeTeam}</strong>
          <div>
            {game.awayScore} - {game.homeScore} ({game.status})
          </div>
          </div>
      ))}
    </div>
  );
}

export default App
