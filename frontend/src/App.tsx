import { BrowserRouter, Routes, Route, Link } from "react-router-dom";
import ScoresPage from "./pages/Scores/ScoresPage";
import LeadersPage from "./pages/Leaders/LeadersPage";
import GameDetailPage from "./pages/GameDetail/GameDetailPage";


function App() {
  return (
    <BrowserRouter>
    <div style={{ padding: 20 }}>
      <nav style={{ marginBottom: 20 }}>
        <Link to="/" style={{ marginRight: 12}}>Scores</Link>
        <Link to="/leaders">Leaders</Link>
      </nav>

      <Routes>
        <Route path="/" element={<ScoresPage />} />
        <Route path="/leaders" element={<LeadersPage />} />
        <Route path="/game/:gameId" element={<GameDetailPage />} />
      </Routes>
    </div>
    </BrowserRouter>
  )
}

export default App;
