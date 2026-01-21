export type GameStatus = 0 | 1 | 2 | "upcoming" | "live" | "final";

export type Game = {
    gameId: string;
    homeTeam: string;
    awayTeam: string;
    homeScore: string;
    awayScore: string;
    status: GameStatus;
    startTimeUtc: string;
    clock: string;
    period: string;
};

export type PagedResult<T> = {
    items: T[];
    page: number;
    pageSize: number;
    totalCount: number;
};

const API_BASE = import.meta.env.VITE_API_BASE_URL;

export async function fetchGames(params?: {
    status?: GameStatus,
    page?: number,
    pageSize?: number
}) {
    const query = new URLSearchParams();

    if (params?.status) query.append("status", params.status.toString());
    if (params?.page) query.append("page", params.page.toString());
    if (params?.pageSize) query.append("pageSize", params.pageSize.toString());


    const res = await fetch(`${API_BASE}/api/games?${query.toString()}`);

    if (!res.ok) {
        throw new Error("Failed to fetch games");
    }

    return res.json() as Promise<PagedResult<Game>>;
}