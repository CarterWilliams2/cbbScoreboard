# cbbScoreboard

LIVE DEMO:
http://cbb-scoreboard-frontend.s3-website.us-east-2.amazonaws.com/

A full-stack web application that tracks live College Basketball scores and utilizes a Machine Learning model to predict win probabilities in real-time.

Architecture:
- Frontend: React hosted on AWS S3
- Backend: .NET 10 Web API hosted on Azure App Service
- ML Service: Python hosted on Azure Functions
- CI/CD: GitHub Actions with OIDC/Publish Profiles for automated deployments

Tech Stack:
- Languages: C#, TypeScript, Python
- Frameworks: ASP.NET Core, React.js
- Cloud: AWS (S3), Azure (App Service, Functions)
- DevOps: GitHub Actions, YAML
