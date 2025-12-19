export type Stat = {
    player_name: string;
    player_team: string;
    player_class: string;
    player_height: string;
    player_position: string;
    games_played: string;
    stat_total: string;
}

const API_BASE = "http://localhost:5150/api/stats";

export async function fetchStats(params?: {
    path?: string,
    statName?: string
}) {
    const query = new URLSearchParams();

    const res = await fetch(`${API_BASE}?${query.toString()}`);

    if (!res.ok) {
        throw new Error("Failed to fetch stats");
    }

    return res.json() as Promise<Stat[]>;
}
