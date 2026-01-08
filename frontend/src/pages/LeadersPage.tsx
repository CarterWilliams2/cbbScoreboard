import { useEffect, useState } from "react";
import { fetchStats, type Stat } from "../api/stats";
import type { StatCategory } from "../api/stats";

export default function LeadersPage() {
  const [stats, setStats] = useState<Stat[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");
  const [statName, setStatName] = useState<StatCategory>("points");

  useEffect(() => {
    fetchStats({ stat: statName })
      .then((data) => setStats(data))
      .catch((err) => setError(err.message))
      .finally(() => setLoading(false));
  }, [statName]);

  if (loading) return <p>Loading games...</p>;
  if (error) return <p>Error: {error}</p>;

  return (
    <div>
      <h1>Season Stat Leaders</h1>
      <p>Coming soon</p>

      <div style={{ marginBottom: 16 }}>
        <label htmlFor="stat-select">Select Stat: </label>
        <select
          id="stat-select"
          value={statName}
          onChange={(e) => setStatName(e.target.value as StatCategory)}
        >
          <option value="ato-ratio">Assist-Turnover Ratio</option>
          <option value="assists">Assists</option>
          <option value="assists-per-game">Assists per Game</option>
          <option value="blocks">Blocks</option>
          <option value="blocks-per-game">Blocks per Game</option>
          <option value="double-doubles">Double Doubles</option>
          <option value="field-goal-attempts">Field Goal Attempts</option>
          <option value="field-goal-percentage">Field Goal Percentage</option>
          <option value="field-goals">Field Goals</option>
          <option value="free-throw-attempts">Free Throw Attempts</option>
          <option value="free-throw-percentage">Free Throw Percentage</option>
          <option value="free-throws">Free Throws Made</option>
          <option value="minutes-per-game">Minutes per Game</option>
          <option value="points">Points</option>
          <option value="points-per-game">Points per Game</option>
          <option value="rebounds">Rebounds</option>
          <option value="def-rebounds-per-game">
            Rebounds [Defensive] per Game
          </option>
          <option value="off-rebounds-per-game">
            Rebounds [Offensive] per Game
          </option>
          <option value="rebounds-per-game">Rebounds per Game</option>
          <option value="steals">Steals</option>
          <option value="steals-per-game">Steals per Game</option>
          <option value="three-point-attempts">3-Point Attempts</option>
          <option value="three-point-percentage">3-Point Percentage</option>
          <option value="three-pointers-per-game">3-Pointers per Game</option>
          <option value="total-three-point-fgm">Total 3-Pointers Made</option>
          <option value="triple-doubles">Triple Doubles</option>
        </select>
      </div>

      {stats.map((stat) => (
        <div key={stat.player_name} style={{ marginBottom: 12 }}>
          <div>{stat.player_name}</div>
          <div>{stat.stat_total}</div>
        </div>
      ))}
    </div>
  );
}
