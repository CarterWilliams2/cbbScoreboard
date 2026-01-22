import joblib
import pandas as pd
from pathlib import Path

class WinProbabilityModel:
    def __init__(self):
        
        model_path = Path(__file__).parent.parent / "training" / "home_win_logreg.joblib"
        
        if model_path.exists():
            self.model = joblib.load(model_path)
            print("Win probability model loaded")
        else:
            raise FileNotFoundError(f"Model not found at {model_path}")

    def predict(self, game_state):
        """
        game_state is a dict with keys:
        - home_score
        - away_score
        - time_remaining_seconds
        """
        score_diff = game_state['home_score'] - game_state['away_score']
        time_remaining = game_state['time_remaining_seconds']

        X = pd.DataFrame([{
            "score_differential": score_diff,
            "time_remaining_seconds": time_remaining
        }])

        home_prob = float(self.model.predict_proba(X)[0][1])

        return {
            "home_win_probability": home_prob,
            "away_win_probability": 1.0 - home_prob
        }