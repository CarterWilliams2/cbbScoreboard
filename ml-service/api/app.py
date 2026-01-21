from fastapi import FastAPI, HTTPException
from pydantic import BaseModel, Field
from fastapi.middleware.cors import CORSMiddleware
from model import WinProbabilityModel

app = FastAPI(
    title="College Basketball Win Probability API",
    description="Predicts win probability based on current game state",
    version="1.0.0"
)

app.add_middleware(
    CORSMiddleware,
    allow_origins=[
        "http://localhost:3000",
        "http://localhost:5173",
        "http://localhost:5150"
    ],
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)

model = WinProbabilityModel("../training/home_win_logreg.joblib")

class GameState(BaseModel):
    period: int = Field(..., ge=1, le=3, description="Period number")
    time_remaining_seconds: int = Field(..., ge=0, le=2400, description="Time remaining in seconds")
    home_score: int = Field(..., ge=0, description="Home team score")
    away_score: int = Field(..., ge=0, description="Away team score")
    
    class Config:
        json_schema_extra = {
            "example": {
                "period": 2,
                "time_remaining_seconds": 300,
                "home_score": 65,
                "away_score": 62
            }
        }

class PredictionResponse(BaseModel):
    home_win_probability: float = Field(..., ge=0.0, le=1.0)
    away_win_probability: float = Field(..., ge=0.0, le=1.0)
    
    class Config:
        json_schema_extra = {
            "example": {
                "home_win_probability": 0.72,
                "away_win_probability": 0.28
            }
        }


@app.get("/")
def root():
    """Root endpoint"""
    return {
        "message": "College Basketball Win Probability API",
        "status": "running",
        "version": "1.0.0"
    }

@app.get("/health")
def health_check():
    """Health check endpoint"""
    return {
        "status": "healthy",
        "model_loaded": True
    }

@app.post("/predict", response_model=PredictionResponse)
def predict_win_probability(game_state: GameState):
    try:
        return model.predict(game_state)
    except Exception as e:
        raise HTTPException(status_code=500, detail=f"Prediction failed: {str(e)}")

if __name__ == "__main__":
    import uvicorn
    uvicorn.run(app, host="0.0.0.0", port=8000)