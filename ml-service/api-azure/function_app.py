import azure.functions as func
import json
import logging
from model import WinProbabilityModel

app = func.FunctionApp()


predictor = WinProbabilityModel()

@app.route(route="predict", auth_level=func.AuthLevel.ANONYMOUS)
def predict(req: func.HttpRequest) -> func.HttpResponse:
    logging.info('CBB Scoreboard: Processing prediction request.')

    try:
        req_body = req.get_json()
        
       
        home_score = req_body.get('home_score', 0)
        away_score = req_body.get('away_score', 0)
        time_left = req_body.get('time_remaining_seconds', 1200) # Default to 20 mins

        
        result = predictor.predict(home_score, away_score, time_left)

        return func.HttpResponse(
            body=json.dumps(result),
            mimetype="application/json",
            status_code=200
        )

    except Exception as e:
        logging.error(f"Error in prediction: {str(e)}")
        return func.HttpResponse("Error processing request", status_code=500)