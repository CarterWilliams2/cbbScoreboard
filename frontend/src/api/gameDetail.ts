import type { Game } from "./games";

const API_BASE = "http://localhost:5150/api/games";

export async function fetchGameDetail(params?: {
    gameId: string,
}) {
    const res = await fetch(`${API_BASE}?${params?.gameId}`);

    if (!res.ok) {
        throw new Error("Failed to fetch games");
    }

    return res.json() as Promise<Game>;
}
