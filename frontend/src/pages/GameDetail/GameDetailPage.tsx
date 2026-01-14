import { useEffect, useState } from "react";
import type { Game } from "../../api/games";
import { fetchGameDetail } from "../../api/gameDetail";
import { useParams } from "react-router-dom";

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

  return (
    <div>
    <h1>Game Detail</h1>
    <h2>{game?.gameId}</h2>
    </div>
  );
}
