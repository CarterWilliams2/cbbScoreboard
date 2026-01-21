export type PlayByPlay = {
    homeScore: string,
    visitorScore: string,
    periodNumber: string,
    clock: string,
    eventDescription: string,
}

const API_BASE = import.meta.env.VITE_API_BASE_URL;

export async function fetchPlayByPlay(params: {
    gameId: string,
}) {

    const res = await fetch(`${API_BASE}/api/play-by-play${params.gameId}`);

    if (!res.ok) {
        throw new Error("Failed to fetch plays");
    }

    return res.json() as Promise<PlayByPlay[]>;
}