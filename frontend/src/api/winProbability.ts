import type { PlayByPlay } from "./playByPlay";

export type WinProbability = {
    away_win_probability: number,
    home_win_probability: number
};

const API_BASE = "http://localhost:5150/api/win-probability";

export async function fetchWinProbability(params: {
    play: PlayByPlay,
}) {

    const res = await fetch(`${API_BASE}/`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(params.play),
    });
    if (!res.ok) {
        throw new Error("Failed to fetch stats");
    }

    return res.json() as Promise<WinProbability>;
}