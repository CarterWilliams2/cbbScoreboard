import type { Game } from "./games";

const API_BASE = import.meta.env.VITE_API_BASE_URL;

export async function fetchGameDetail(params?: {
    gameId: string,
}) {
    const res = await fetch(`${API_BASE}/api/games/${params?.gameId}`);

    if (!res.ok) {
        throw new Error("Failed to fetch games");
    }

    return res.json() as Promise<Game>;
}
