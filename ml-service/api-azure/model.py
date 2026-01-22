import joblib
import pandas as pd
import os
from pathlib import Path

class WinProbabilityModel:
    def __init__(self):
        
        current_dir = Path(__file__).parent
        model_path = current_dir / "home_win_logreg.joblib"
        
        if model_path.exists():
            
            self.model = joblib.load(model_path)
        else:
            
            alternative_path = Path("/home/site/wwwroot/home_win_logreg.joblib")
            if alternative_path.exists():
                self.model = joblib.load(alternative_path)
            else:
                raise FileNotFoundError(f"Model file not found at {model_path}")

    def predict(self, home_score, away_score, time_remaining):
        score_diff = home_score - away_score
        
        
        X = pd.DataFrame([{
            "score_differential": score_diff,
            "time_remaining_seconds": time_remaining
        }])

        
        probs = self.model.predict_proba(X)[0]
        home_prob = float(probs[1])

        return {
            "home_win_probability": round(home_prob, 4),
            "away_win_probability": round(1.0 - home_prob, 4)
        }