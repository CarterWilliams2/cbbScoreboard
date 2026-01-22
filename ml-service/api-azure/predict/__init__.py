import logging
import json
import azure.functions as func
from ..model import WinProbabilityModel


model = WinProbabilityModel()

def main(req: func.HttpRequest) -> func.HttpResponse:
    logging.info('Predict function processed a request.')

    
    headers = {
        'Access-Control-Allow-Origin': '*',
        'Access-Control-Allow-Methods': 'POST, GET, OPTIONS',
        'Access-Control-Allow-Headers': 'Content-Type'
    }

    
    if req.method == 'OPTIONS':
        return func.HttpResponse(status_code=200, headers=headers)

    try:
        
        req_body = req.get_json()
        
        
        required_fields = ['period', 'time_remaining_seconds', 'home_score', 'away_score']
        if not all(field in req_body for field in required_fields):
            return func.HttpResponse(
                json.dumps({"error": "Missing required fields"}),
                status_code=400,
                headers=headers,
                mimetype="application/json"
            )
        
        
        game_state = {
            'period': req_body['period'],
            'time_remaining_seconds': req_body['time_remaining_seconds'],
            'home_score': req_body['home_score'],
            'away_score': req_body['away_score']
        }
        
        
        prediction = model.predict(game_state)
        
        return func.HttpResponse(
            json.dumps(prediction),
            status_code=200,
            headers=headers,
            mimetype="application/json"
        )
        
    except ValueError:
        return func.HttpResponse(
            json.dumps({"error": "Invalid request body"}),
            status_code=400,
            headers=headers,
            mimetype="application/json"
        )
    except Exception as e:
        logging.error(f"Error: {str(e)}")
        return func.HttpResponse(
            json.dumps({"error": str(e)}),
            status_code=500,
            headers=headers,
            mimetype="application/json"
        )