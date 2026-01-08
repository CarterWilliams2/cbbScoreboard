export type Stat = {
    player_name: string;
    player_team: string;
    player_class: string;
    player_height: string;
    player_position: string;
    games_played: string;
    stat_total: string;
}

export type StatCategory = 
    | "ato-ratio"
    | "assists"
    | "assists-per-game"
    | "blocks"
    | "blocks-per-game"
    | "double-doubles"
    | "field-goal-attempts"
    | "field-goal-percentage"
    | "field-goals"
    | "free-throw-attempts"
    | "free-throw-percentage"
    | "free-throws"
    | "minutes-per-game"
    | "points"
    | "points-per-game"
    | "rebounds"
    | "def-rebounds-per-game"
    | "off-rebounds-per-game"
    | "rebounds-per-game"
    | "steals"
    | "steals-per-game"
    | "three-point-attempts"
    | "three-point-percentage"
    | "three-pointers-per-game"
    | "total-three-point-fgm"
    | "triple-doubles"

const API_BASE = "http://localhost:5150/api/stats";

export async function fetchStats(params: {
    stat: StatCategory,
}) {

    const res = await fetch(`${API_BASE}/${params.stat}`);

    if (!res.ok) {
        throw new Error("Failed to fetch stats");
    }

    return res.json() as Promise<Stat[]>;
}
