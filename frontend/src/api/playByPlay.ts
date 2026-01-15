export type PlayByPlay = {
    home_score: string,
    visitor_score: string,
    period_number: string,
    clock: string,
    event_description: string,
}

const API_BASE = "http://localhost:5150/play-by-play";

export async function fetchPLayByPlay(params: {
    gameId: string,
}) {

    const res = await fetch(`${API_BASE}/${params.gameId}`);

    if (!res.ok) {
        throw new Error("Failed to fetch plays");
    }

    return res.json() as Promise<PlayByPlay[]>;
}