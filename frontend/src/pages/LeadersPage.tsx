import { useEffect, useState } from "react";
import { fetchStats, type Stat } from "../api/stats";

export default function LeadersPage() {
  const [stats, setStats] = useState<Stat[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  useEffect(() => {
    fetchStats()
      .then((data) => setStats(data))
      .catch((err) => setError(err.message))
      .finally(() => setLoading(false));
  }, []);

  if (loading) return <p>Loading games...</p>;
  if (error) return <p>Error: {error}</p>;

  return (
    <div>
      <h1>Season Stat Leaders</h1>
      <p>Coming soon</p>

      {stats.map((stat) => (
        <div key={stat.player_name} style={{ marginBottom: 12 }}>
            <div>{stat.player_name}</div>
            <div>{stat.stat_total}</div>
        </div>
      ))}
    </div>
  );
}
