export type Game = {
    gameId: string;
    homeTeam: string;
    awayTeam: string;
    homeScore: string;
    awayScore: string;
    status: "live" | "final" | "upcoming";
    startTimeUtc: string;
};

export type PagedResult<T> = {
    items: T[];
    page: number;
    pageSize: number;
    totalCount: number;
};

export async function fetchGames() {
    const res = await fetch("http://localhost:5150/api/games");

    if (!res.ok) {
        throw new Error("Failed to fetch games");
    }

    return res.json() as Promise<PagedResult<Game>>;
}