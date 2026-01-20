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
                play['period'] = period.get('periodNumber')
                plays.append(play)
        
        home_winner = get_winner(plays[-1])
        play_rows = []
        for play in plays:
            play_rows.append(convert_play_to_row(play, home_winner, game_id))
        
        
        print(f"Found {len(plays)} plays for game: {game_id}")

        return play_rows
        
    except Exception as e:
        print(f"Error fetching plays for {game_id}: {e}")
        return []

def get_winner(play):
    home = play.get('homeScore', 0)
    away = play.get('visitorScore', 0)
    
    if home > away:
        return True

    return False

def convert_play_to_row(play, home_winner, game_id):
    
    home_score = play.get('homeScore', 0)
    away_score = play.get('visitorScore', 0)
    
    clock = play.get('clock', '0:00')
    period = play.get('period')
    minutes, seconds = clock.split(":")
    time_remaining = 0
    
    time_remaining += int(seconds)
    time_remaining += int(minutes) * 60
    if (period == 1):
        time_remaining += 1200
    
    row = {
        'game_id': game_id,
        'home_score': home_score,
        'away_score': away_score,
        'period': period,
        'time_remaining_seconds': time_remaining,
        'home_win': home_winner
    }
    
    return row


if __name__ == "__main__":
    date_string = "2026/01/19"
    safe_date_string = date_string.replace("/", "-")

    game_ids = fetch_game_ids_for_date(date_string)

    all_rows = []
    for game_id in game_ids:
        game_plays = fetch_plays_for_game_id(game_id)
        all_rows.extend(game_plays)
        time.sleep(0.5)
    
    df = pd.DataFrame(all_rows)
    
    output_file = f"training_data_{safe_date_string}.csv"
    df.to_csv(output_file, index=False)
    