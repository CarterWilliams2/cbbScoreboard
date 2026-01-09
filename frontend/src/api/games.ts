export type GameStatus = "live" | "final" | "upcoming";

export type Game = {
    gameId: string;
    homeTeam: string;
    awayTeam: string;
    homeScore: string;
    awayScore: string;
    status: GameStatus;
    startTimeUtc: string;
    clock: string;
};

export type PagedResult<T> = {
    items: T[];
    page: number;
    pageSize: number;
    totalCount: number;
};

const API_BASE = "http://localhost:5150/api/games"

export async function fetchGames(params?: {
    status?: GameStatus,
    page?: number,
    pageSize?: number
}) {
    const query = new URLSearchParams();

    if (params?.status) query.append("status", params.status);
    if (params?.page) query.append("page", params.page.toString());
    if (params?.pageSize) query.append("pageSize", params.pageSize.toString());


    const res = await fetch(`${API_BASE}?${query.toString()}`);

    if (!res.ok) {
        throw new Error("Failed to fetch games");
    }

    return res.json() as Promise<PagedResult<Game>>;
}