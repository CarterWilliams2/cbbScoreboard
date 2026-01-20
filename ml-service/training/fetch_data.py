import requests
import pandas as pd
from datetime import datetime, timedelta
import time

def fetch_game_ids_for_date(date_string):

    # get all games for a certain date
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
        

        
    except Exception as e:
        print(f"Error fetching games for {date_string}: {e}")
        return []

    # grab all of the game ids

    #return the ids


    return None

if __name__ == "__main__":
   date_string = "2026/01/19"

   fetch_game_ids_for_date(date_string)
