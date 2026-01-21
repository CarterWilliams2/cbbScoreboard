import type { PlayByPlay } from "./playByPlay";

export type WinProbability = {
    away_win_probability: number,
    home_win_probability: number
};

const API_BASE = import.meta.env.VITE_ML_API_BASE_URL;

export async function fetchWinProbability(params: {
    play: PlayByPlay,
}) {

    const res = await fetch(`${API_BASE}/api/win-probability`, {
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