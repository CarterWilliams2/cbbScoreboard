import requests
import pandas as pd
from datetime import datetime, timedelta
import time

def fetch_game_ids_for_date(date_string):

    url = f"https://ncaa-api.henrygd.me/scoreboard/basketball-men/d1/{date_string}"
    game_ids = []

    try:
        response = requests.get(url)
        response.raise_for_status()
        data = response.json()
        
        games = data.get('games', [])
        print(f"Found {len(games)} games for {date_string}")
        
        for _g in games:
            g = _g.get('game', '')
            game_state = g.get('finalMessage', '')
            if game_state == 'FINAL':
                game_id = g.get('gameID')
                game_ids.append(game_id)
        
        print(f"{len(game_ids)} completed games")

        return game_ids
        

        
    except Exception as e:
        print(f"Error fetching games for {date_string}: {e}")
        return []

def fetch_plays_for_game_id(game_id):

    url = f"https://ncaa-api.henrygd.me/game/{game_id}/play-by-play"
    plays = []

    try:
        response = requests.get(url)
        response.raise_for_status()
        data = response.json()

        periods = data.get('periods', [])
        for period in periods:
            period_plays = period.get('playbyplayStats', [])
            for play in period_plays:
                plays.append(play)
        
        print(f"Found {len(plays)} plays for game: {game_id}")

        return plays
        
    except Exception as e:
        print(f"Error fetching plays for {game_id}: {e}")
        return []


if __name__ == "__main__":
    date_string = "2026/01/19"

    game_ids = fetch_game_ids_for_date(date_string)

    all_plays = []
    for game_id in game_ids:
        game_plays = fetch_plays_for_game_id(game_id)
        all_plays.extend(game_plays)
        time.sleep(0.5)
    


