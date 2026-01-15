class WinProbabilityModel:
    def __init__(self):
        print("Model initialized")
    
    def predict(self, game_state):
        return {
            "home_win_probability": .5,
            "away_win_probability": .5
        }