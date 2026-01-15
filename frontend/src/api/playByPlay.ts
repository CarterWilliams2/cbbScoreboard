export type PlayByPlay = {
    homeScore: string,
    visitorScore: string,
    periodNumber: string,
    clock: string,
    eventDescription: string,
}

const API_BASE = "http://localhost:5150/api/play-by-play";

export async function fetchPlayByPlay(params: {
    gameId: string,
}) {

    const res = await fetch(`${API_BASE}/${params.gameId}`);

    if (!res.ok) {
        throw new Error("Failed to fetch plays");
    }

    return res.json() as Promise<PlayByPlay[]>;
}