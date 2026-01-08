namespace cbbScoreboard.Constants
{
public static class StatCategories

{
    public static readonly Dictionary<string, (string categoryId, string stat)> Map =
    new()
    {
        ["assists"] = ("605", "AST"),
        ["ato-ratio"] = ("473", "Ratio"),
        ["assists-per-game"] = ("140", "APG"),
        ["blocks"] = ("608", "BLKS"),
        ["blocks-per-game"] = ("138", "BKPG"),
        ["double-doubles"] = ("556", "Dbl Dbl"),
        ["field-goal-attempts"] = ("618", "FGA"),
        ["field-goal-percentage"] = ("141", "FG%"),
        ["field-goals"] = ("611", "FGM"),
        ["free-throw-attempts"] = ("851", "FTA"),
        ["free-throw-percentage"] = ("142", "FT%"),
        ["free-throws"] = ("850", "FT"),
        ["minutes-per-game"] = ("628", "MPG"),
        ["points"] = ("600", "PTS"),
        ["points-per-game"] = ("136", "PPG"),
        ["rebounds"] = ("601", "REB"),
        ["def-rebounds-per-game"] = ("858", "RPG"),
        ["off-rebounds-per-game"] = ("856", "RPG"),
        ["rebounds-per-game"] = ("137", "RPG"),
        ["steals"] = ("615", "ST"),
        ["steals-per-game"] = ("139", "STPG"),
        ["three-point-attempts"] = ("624", "3FGA"),
        ["three-point-percentage"] = ("143", "3FG%"),
        ["three-pointers-per-game"] = ("144", "3PG"),
        ["total-3-point-FGM"] = ("621", "3FG"),
        ["triple-doubles"] = ("557", "Trpl Dbl")
    };
}
}